using MovieRental.Api.Middleware;
using MovieRental.Data;
using MovieRental.PaymentProviders;
using MovieRental.Rental;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

// scoped
builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();
builder.Services.AddScoped<IPaymentResolver, PaymentResolver>();
builder.Services.AddScoped<IPaymentStrategy, MbWayProvider>();
builder.Services.AddScoped<IPaymentStrategy, PayPalProvider>();

builder.Services.AddTransient<GlobalExceptionMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

using (var client = new MovieRentalDbContext())
{
	client.Database.EnsureCreated();
}

app.Run();
