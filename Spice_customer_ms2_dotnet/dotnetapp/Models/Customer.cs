using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }  // Primary key
        public string FullName { get; set; }  // Doctor's name
        public string ContactNumber { get; set; }  // Doctor's specialty
        public string Address { get; set; }  // Doctor's contact information
        
         [JsonIgnore]
        public ICollection<Spice>? Spices { get; set; }
    }
}
