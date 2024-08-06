using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models{
public class MenuItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [MaxLength(200)]
    public string Description { get; set; }

    public int? RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}
}
