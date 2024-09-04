using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        
        [Required]
        public string CustomerName { get; set; }
        
        [Required]
        public string OrderDate { get; set; }

        public ICollection<MusicRecord> MusicRecords { get; set; } = new List<MusicRecord>();
    }
}
