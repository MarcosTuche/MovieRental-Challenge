namespace MovieRental.PaymentProviders
{
    public class PayPalProvider : IPaymentStrategy
    {
        public PaymentMethod PaymentMethod => PaymentMethod.PayPal;

        public Task<bool> Pay(double price)
        {
            //ignore this implementation
            return Task.FromResult<bool>(true);
        }
    }
}
