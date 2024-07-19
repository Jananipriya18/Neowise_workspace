using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class College
    {
        [Key]
        public int CollegeId { get; set; }

        [Required(ErrorMessage = "College Name is required")]
        public string CollegeName { get; set; }


        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        public string Description { get; set; }
        public string Website { get; set; }
    }
}
