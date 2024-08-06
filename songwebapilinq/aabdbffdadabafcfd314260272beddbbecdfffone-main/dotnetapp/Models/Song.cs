using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Artist { get; set; }

        [Required]
        [Range(1900, 2024)]
        public int ReleaseYear { get; set; }

        public ICollection<Playlist> Playlists { get; set; } // Navigation property
    }
}
