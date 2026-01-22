namespace MovieRental.PaymentProviders
{
    public interface IPaymentStrategy
    {
        PaymentMethod PaymentMethod { get; }
        Task<bool> Pay(double price);
    }
}
