using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        [Required]
        [RegularExpression(@"\(\d{3}\) \d{3}-\d{4}")]
        public string PhoneNumber { get; set; }

       public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();  
    }
}
