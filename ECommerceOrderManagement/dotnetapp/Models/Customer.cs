using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }  // Primary key
        public string Name { get; set; }  // Customer's name
        public string Email { get; set; }  // Customer's email
        public string Address { get; set; }  // Customer's address

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}
