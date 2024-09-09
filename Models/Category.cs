using System.ComponentModel.DataAnnotations;
namespace DotNet.LaptopStore.Models;

public class Category
{
    [Key]

    public int Id { get; set; }

    [Required(ErrorMessage = "Catrgory Name is Required")]
    [StringLength(100, ErrorMessage = "category name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();
}