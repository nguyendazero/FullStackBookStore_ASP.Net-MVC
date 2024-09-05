using System.ComponentModel.DataAnnotations;

namespace DotNet.BookStore.Models
{
    public class LikeRating
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Book ID is required")]
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        [Required(ErrorMessage = "Rating ID is required")]
        public int RatingId { get; set; }
        public virtual Rating? Rating { get; set; }
    }
}
