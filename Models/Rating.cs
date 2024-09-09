using System.ComponentModel.DataAnnotations;

namespace DotNet.BookStore.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Stars are required")]
        [Range(1, 5, ErrorMessage = "Stars must be between 1 and 5")]
        public int Stars { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(1000, ErrorMessage = "Content cannot exceed 1000 characters")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date is required")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Book ID is required")]
        public int LaptopId { get; set; }
        public virtual Laptop? Laptop { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<LikeRating> LikeRatings { get; set; } = new List<LikeRating>();
    }
}
