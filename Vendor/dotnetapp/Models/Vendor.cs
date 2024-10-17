using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Vendor
    {
        public int vendorId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Product Offerings are required.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Product Offerings must be between 1 and 500 characters.")]
        public string productOfferings { get; set; }

        [Required(ErrorMessage = "Store Location is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Store Location must be between 1 and 100 characters.")]
        public string storeLocation { get; set; }

        [Required(ErrorMessage = "Operating Hours are required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Operating Hours must be between 1 and 50 characters.")]
        public string operatingHours { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone Number must be between 10 and 15 characters.")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Experience is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Experience must be a non-negative value.")]
        public int experience { get; set; }
    }
}