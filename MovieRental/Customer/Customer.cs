using System.ComponentModel.DataAnnotations;

namespace MovieRental.Customer
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
    }
}
