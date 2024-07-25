using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Playlist
    {
        public int playlistId { get; set; }

        [Required(ErrorMessage = "Playlist name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Playlist name must be between 1 and 100 characters.")]
        public string playlistName { get; set; }

        [Required(ErrorMessage = "Song Name is required.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Song Name must be between 1 and 500 characters.")]
        public string songName { get; set; }

        [Required(ErrorMessage = "Year of release is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Year of release must be in the format YYYY-MM-DD.")]
        public string yearOfRelease { get; set; }

        [Required(ErrorMessage = "Artist Name is required.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Artist Name must be between 1 and 50 characters.")]
        public string artistName { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Genre must be between 1 and 200 characters.")]
        public string genre { get; set; }

        [Required(ErrorMessage = "Movie name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Movie name must be between 1 and 100 characters.")]
        public string MovieName { get; set; }
    }
}
