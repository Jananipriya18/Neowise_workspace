using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Orphanage
    {
        [Key]
        public int OrphanageId { get; set; } // Primary key: Unique identifier for the orphanage.

        [Required(ErrorMessage = "Orphanage name is required")]
        public string OrphanageName { get; set; } // Name of the orphanage.

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } // Description of the orphanage.

        [Required(ErrorMessage = "Founder is required")]
        public string Founder { get; set; } // Founder or organizer of the orphanage.

        [Required(ErrorMessage = "Establishment date is required")]
        public DateTime EstablishmentDate { get; set; } // Date when the orphanage was established.

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } // Status of the orphanage (e.g., active, closed).
    }
}
