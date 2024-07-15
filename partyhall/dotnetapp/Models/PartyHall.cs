using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class PartyHall
    {
        [Key]
        public long PartyHallId { get; set; }

        public string HallName { get; set; }

        public string HallImageUrl { get; set; }

        public string HallLocation { get; set; }

        public string HallAvailableStatus { get; set; }

        public long Price { get; set; }

        public int Capacity { get; set; }

        public string Description { get; set; }

        // [JsonIgnore]
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}