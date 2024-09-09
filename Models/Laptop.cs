using System.ComponentModel.DataAnnotations;
using DotNet.BookStore.Models;
namespace DotNet.BookStore.Models
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Laptop name is required")]
        [StringLength(100, ErrorMessage = "Laptop name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;  // Tên laptop

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;  // Mô tả laptop

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000")]
        public double Price { get; set; }  // Giá laptop

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image URL is required")]
        public string Image { get; set; } = string.Empty; // Đường dẫn hình ảnh laptop

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10000, ErrorMessage = "Quantity must be at least 1 and at most 10000")]
        public int Quantity { get; set; }  // Số lượng laptop còn trong kho

        [Required]
        public int CategoryId { get; set; }  // Id danh mục
        public virtual Category? Category { get; set; }

        [Required]
        public int ColorId { get; set; }  // Id màu của laptop
        public virtual Color? Color { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public double AverageRating { get; set; }  // Đánh giá trung bình

        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

}
