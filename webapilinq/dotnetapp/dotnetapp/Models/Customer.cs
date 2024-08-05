using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models
{
public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Phone number must be in the format ###-###-####")]
    public string PhoneNumber { get; set; }

    public ICollection<Movie>? Movies { get; set; }
}
}
