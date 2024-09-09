using System.ComponentModel.DataAnnotations;

namespace DotNet.LaptopStore.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }

        public virtual User? User { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
