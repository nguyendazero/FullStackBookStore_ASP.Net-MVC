using System.ComponentModel.DataAnnotations;
namespace DotNet.BookStore.Models;

public class Category
{
    [Key]

    public int Id { get; set; }

    [Required(ErrorMessage = "Catrgory Name is Required")]
    [StringLength(100, ErrorMessage = "category name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}