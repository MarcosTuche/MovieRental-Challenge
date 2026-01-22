namespace MovieRental.PaymentProviders
{
    public class PaymentResolver : IPaymentResolver
    {
        private readonly IDictionary<PaymentMethod, IPaymentStrategy> map;

        public PaymentResolver()
        {
            map = new Dictionary<PaymentMethod, IPaymentStrategy>
            {
                [PaymentMethod.PayPal] = new PayPalProvider(),
                [PaymentMethod.MbWay] = new MbWayProvider(),
            };
        }

        public IPaymentStrategy Resolve(PaymentMethod paymentMethod)
            => map.TryGetValue(paymentMethod, out var strategy) ? strategy : throw new Exception("PaymentMethod not supported");
    }
}
