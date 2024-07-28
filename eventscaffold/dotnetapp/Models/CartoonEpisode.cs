using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class CartoonEpisode
    {
        [Key]
        public int EpisodeId { get; set; }

        [Required(ErrorMessage = "Cartoon series name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Cartoon series name must be between 1 and 100 characters.")]
        public string CartoonSeriesName { get; set; }

        [Required(ErrorMessage = "Episode title is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Episode title must be between 1 and 200 characters.")]
        public string EpisodeTitle { get; set; }

        [Required(ErrorMessage = "Release date is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Release date must be in the format YYYY-MM-DD.")]
        public string ReleaseDate { get; set; }

        [Required(ErrorMessage = "Director name is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Director name must be between 1 and 50 characters.")]
        public string DirectorName { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive number.")]
        public int Duration { get; set; } // Duration in minutes

        [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters.")]
        public string Description { get; set; }
    }
}
