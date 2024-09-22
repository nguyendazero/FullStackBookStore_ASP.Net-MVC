using System.ComponentModel.DataAnnotations;

namespace DotNet.LaptopStore.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }  // Số lượng laptop còn trong kho

        [Required(ErrorMessage = "Book ID is required")]
        public int LaptopId { get; set; }
        public virtual Laptop? Laptop { get; set; }

        [Required(ErrorMessage = "Order ID is required")]
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
