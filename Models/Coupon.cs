using System.ComponentModel.DataAnnotations;

namespace DotNet.LaptopStore.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Coupon code is required")]
        [StringLength(10, ErrorMessage = "Coupon code cannot exceed 10 characters")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Discount is required")]
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
        public int Discount { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        public DateOnly Expiry { get; set; }
    }
}
