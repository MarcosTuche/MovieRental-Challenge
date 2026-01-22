namespace MovieRental.PaymentProviders
{
    public interface IPaymentResolver
    {
        IPaymentStrategy Resolve(PaymentMethod paymentMethod);
    }
}
