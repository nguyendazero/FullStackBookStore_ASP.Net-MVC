using System.ComponentModel.DataAnnotations;

namespace DotNet.LaptopStore.Models
{
    public class LikeRating
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Book ID is required")]
        public int LaptopId { get; set; }
        public virtual Laptop? Laptop { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        [Required(ErrorMessage = "Rating ID is required")]
        public int RatingId { get; set; }
        public virtual Rating? Rating { get; set; }
    }
}
