using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class MusicRecord
    {
        [Key]
        public int MusicRecordId { get; set; }
        
        [Required]
        public string Artist { get; set; }
        
        [Required]
        public string Album { get; set; }
        
        public string Genre { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public int StockQuantity { get; set; }

        // Foreign key to the Order
        public int? OrderId { get; set; }
        
        // Navigation property to the Order
        public Order? Order { get; set; }
    }
}
