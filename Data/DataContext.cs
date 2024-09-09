using Microsoft.EntityFrameworkCore;
using DotNet.LaptopStore.Models;

namespace DotNet.LaptopStore.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    // Khai báo DbSet cho các lớp model của bạn
    public DbSet<Color> Colors { get; set; }
    public DbSet<Laptop> Laptops { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<LikeRating> LikeRatings { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ví dụ cấu hình mối quan hệ

        // Thiết lập mối quan hệ giữa Laptop và Color
        modelBuilder.Entity<Laptop>()
            .HasOne(l => l.Color)            // Một Laptop có một Color
            .WithMany(c => c.Laptops)        // Một Color có nhiều Laptops
            .HasForeignKey(l => l.ColorId);  // Khóa ngoại trên Laptop trỏ tới ColorId

        // Thiết lập mối quan hệ giữa Laptop và Category
        modelBuilder.Entity<Laptop>()
            .HasOne(l => l.Category)
            .WithMany(c => c.Laptops)
            .HasForeignKey(l => l.CategoryId); //

        // Thiết lập mối quan hệ giữa Cart và User
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithOne(u => u.Cart)
            .HasForeignKey<Cart>(c => c.UserId);

        // Thiết lập mối quan hệ giữa CartItem và Laptop
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Laptop)
            .WithMany(l => l.CartItems)
            .HasForeignKey(ci => ci.LaptopId);

        // Thiết lập mối quan hệ giữa CartItem và Cart
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId);

        // Thiết lập mối quan hệ giữa Order và User
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        // Thiết lập mối quan hệ giữa Order và Coupon
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Coupon)
            .WithMany()
            .HasForeignKey(o => o.CouponId);

        // Thiết lập mối quan hệ giữa OrderDetail và Laptop
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Laptop)
            .WithMany(l => l.OrderDetails)
            .HasForeignKey(od => od.LaptopId);

        // Thiết lập mối quan hệ giữa OrderDetail và Order
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

        modelBuilder.Entity<LikeRating>()
            .HasOne(lr => lr.User)
            .WithMany(u => u.LikeRatings)
            .HasForeignKey(lr => lr.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Thay đổi hành động xóa thành Restrict

        modelBuilder.Entity<LikeRating>()
            .HasOne(lr => lr.Rating)
            .WithMany(r => r.LikeRatings)
            .HasForeignKey(lr => lr.RatingId)
            .OnDelete(DeleteBehavior.Restrict); // Thay đổi hành động xóa thành Restrict


        // Thiết lập mối quan hệ giữa Rating và Laptop
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Laptop)
            .WithMany(l => l.Ratings)
            .HasForeignKey(r => r.LaptopId);

        // Thiết lập mối quan hệ giữa Rating và User
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId);

    }

}
