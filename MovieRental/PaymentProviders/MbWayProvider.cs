using Microsoft.EntityFrameworkCore.Storage.Json;

namespace MovieRental.PaymentProviders
{
    public class MbWayProvider : IPaymentStrategy
    {
        public PaymentMethod PaymentMethod => PaymentMethod.MbWay;

        public Task<bool> Pay(double price)
        {
            throw new Exception("asdasdasd");
            //ignore this implementation
            return Task.FromResult<bool>(true);
        }
    }
}
