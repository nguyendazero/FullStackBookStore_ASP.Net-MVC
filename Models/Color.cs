using System.ComponentModel.DataAnnotations;
namespace DotNet.LaptopStore.Models;

public class Color
{
    [Key]

    public int Id { get; set; }

    [Required(ErrorMessage = "Color Name is Required")]
    [StringLength(100, ErrorMessage = "color name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();
}