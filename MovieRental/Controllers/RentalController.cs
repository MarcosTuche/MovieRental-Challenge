using Microsoft.AspNetCore.Mvc;
using MovieRental.PaymentProviders;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;
        private readonly IPaymentResolver _paymentResolver;

        public RentalController(IRentalFeatures features, IPaymentResolver paymentResolver)
        {
            _features = features;
            _paymentResolver = paymentResolver;
        }


        [HttpPost]
        public IActionResult Post([FromBody] Rental.Rental rental)
        {
	        return Ok(_features.Save(rental));
        }

        [HttpGet]
        public IActionResult Get(string customerName)
        {
            return Ok(_features.GetRentalsByCustomerName(customerName));
        }

        //EX: rental/payment?price=10
        [HttpPatch("payment")]
        public async Task<IActionResult> PaymentProcess([FromQuery]double price, [FromBody] Rental.Rental rental)
        {
            if (string.IsNullOrEmpty(rental.PaymentMethod))
                throw new ArgumentException("Payment Process not valid.");

            if (!Enum.TryParse<PaymentMethod>(rental.PaymentMethod, true, out var payment))
                throw new ArgumentException($"Invalid Payment Method {rental.PaymentMethod}");

            var strategy = _paymentResolver.Resolve(payment);
            var paid = await strategy.Pay(price);

            return Ok(new { paid, method = payment.ToString(), price });
        }
    }
}
