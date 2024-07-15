using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
 
using System.ComponentModel.DataAnnotations.Schema;
 
namespace dotnetapp.Models
{
    public class Booking
    {
        [Key]
        public long? BookingId { get; set; }
 
        public int NoOfPersons { get; set; }
 
        public DateTime FromDate { get; set; }
 
        public DateTime ToDate { get; set; }
 
        public string Status { get; set; }
 
        public double TotalPrice { get; set; }
 
        public string Address { get; set; }
 
        public long UserId { get; set; } 

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; } 
 
        public long PartyHallId  { get; set; } 

        [ForeignKey(nameof(PartyHallId))]
        public virtual PartyHall? PartyHall { get; set; } 
        
    }
}

