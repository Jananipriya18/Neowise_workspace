using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class CheeseShop
    {
        [Key]
        public int shopId { get; set; }

        [Required(ErrorMessage = "Owner name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Owner name must be between 1 and 100 characters.")]
        public string ownerName { get; set; }

        [Required(ErrorMessage = "Cheese specialties are required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Cheese specialties must be between 1 and 200 characters.")]
        public string cheeseSpecialties { get; set; }

        [Required(ErrorMessage = "Experience years is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Experience years must be a non-negative number.")]
        public int experienceYears { get; set; }

        [Required(ErrorMessage = "Store location is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Store location must be between 1 and 100 characters.")]
        public string storeLocation { get; set; }

        [Required(ErrorMessage = "Imported country is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Imported country must be between 1 and 50 characters.")]
        public string importedCountry { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Phone number must be between 5 and 20 characters.")]
        public string phoneNumber { get; set; }
    }
}
