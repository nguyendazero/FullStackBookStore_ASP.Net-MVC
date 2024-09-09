using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet.LaptopStore.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50, ErrorMessage = "Status length can't be more than 50.")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "Payment Method is required")]
        [StringLength(50, ErrorMessage = "Payment method length can't be more than 50.")]
        public string PaymentMethod { get; set; } = string.Empty;

        public int? CouponId { get; set; }
        public virtual Coupon? Coupon { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
