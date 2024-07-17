using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Orphanage ID is required")]
        public int OrphanageId { get; set; }
        public Orphanage? Orphanage { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Donation date is required")]
        public DateTime DonationDate { get; set; }
    }
}
