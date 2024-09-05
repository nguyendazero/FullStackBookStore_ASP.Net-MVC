using System.ComponentModel.DataAnnotations;

namespace DotNet.BookStore.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Home town is required")]
        [StringLength(100, ErrorMessage = "Home town cannot exceed 100 characters")]
        public string HomeTown { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Story cannot exceed 500 characters")]
        public string Story { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year of birth is required")]
        [Range(0, 2024, ErrorMessage = "Year of birth must be between 0 and 2024")]
        public int YearOfBirth { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
