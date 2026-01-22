using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MovieRental.Api.Middleware;

public sealed class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static async Task HandleException(HttpContext context, Exception ex)
    {
        var (status, title, detail) = MapException(ex);
        context.Response.StatusCode = status;

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }

    private static (int status, string title, string detail) MapException(Exception ex)
    {
        return ex switch
        {
            ArgumentException or FormatException
                => (StatusCodes.Status400BadRequest, "Bad Request", ex.Message),

            KeyNotFoundException
                => (StatusCodes.Status404NotFound, "Not Found", ex.Message),

            DbUpdateConcurrencyException
                => (StatusCodes.Status409Conflict, "Concurrency Conflict",
                    "Concurrency Conflict"),

            DbUpdateException
                => (StatusCodes.Status409Conflict, "Database Conflict",
                    "Database Conflict"),

            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error",
                  "Unexpected Error")
        };
    }
}
