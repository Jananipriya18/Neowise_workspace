using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
public class Movie{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(50)]
    public string Director { get; set; }

    [Range(1900, 2024)]
    public int ReleaseYear { get; set; }

    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
}
