using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;

namespace DotNet.BookStore.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    // Khai báo DbSet cho các lớp model của bạn
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
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
        // Thiết lập mối quan hệ giữa Book và Author
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)           // Một Book có một Author
            .WithMany(a => a.Books)          // Một Author có nhiều Books
            .HasForeignKey(b => b.AuthorId); // Khóa ngoại trên Book trỏ tới AuthorId

        // Thiết lập mối quan hệ giữa Book và Category
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)        // Một Book có một Category
            .WithMany(c => c.Books)         // Một Category có nhiều Books
            .HasForeignKey(b => b.CategoryId); // Khóa ngoại trên Book trỏ tới CategoryId

        // Thiết lập mối quan hệ giữa Cart và User
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)           // Một Cart có một User
            .WithOne(u => u.Cart)          // Một User có một Cart
            .HasForeignKey<Cart>(c => c.UserId); // Khóa ngoại trên Cart trỏ tới UserId

        // Thiết lập mối quan hệ giữa CartItem và Book
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Book)         // Một CartItem có một Book
            .WithMany(b => b.CartItems)   // Một Book có nhiều CartItems
            .HasForeignKey(ci => ci.BookId); // Khóa ngoại trên CartItem trỏ tới BookId

        // Thiết lập mối quan hệ giữa CartItem và Cart
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)         // Một CartItem có một Cart
            .WithMany(c => c.CartItems)   // Một Cart có nhiều CartItems
            .HasForeignKey(ci => ci.CartId); // Khóa ngoại trên CartItem trỏ tới CartId

        // Thiết lập mối quan hệ giữa Order và User
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)          // Một Order có một User
            .WithMany(u => u.Orders)      // Một User có nhiều Orders
            .HasForeignKey(o => o.UserId); // Khóa ngoại trên Order trỏ tới UserId

        // Thiết lập mối quan hệ giữa Order và Coupon
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Coupon)        // Một Order có một Coupon (không bắt buộc)
            .WithMany()                   // Coupon không cần có mối quan hệ ngược với Order
            .HasForeignKey(o => o.CouponId); // Khóa ngoại trên Order trỏ tới CouponId

        // Thiết lập mối quan hệ giữa OrderDetail và Book
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Book)        // Một OrderDetail có một Book
            .WithMany(b => b.OrderDetails) // Một Book có nhiều OrderDetails
            .HasForeignKey(od => od.BookId); // Khóa ngoại trên OrderDetail trỏ tới BookId

        // Thiết lập mối quan hệ giữa OrderDetail và Order
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)       // Một OrderDetail có một Order
            .WithMany(o => o.OrderDetails) // Một Order có nhiều OrderDetails
            .HasForeignKey(od => od.OrderId); // Khóa ngoại trên OrderDetail trỏ tới OrderId

        modelBuilder.Entity<LikeRating>()
        .HasOne(lr => lr.Book)
        .WithMany(b => b.LikeRatings)
        .HasForeignKey(lr => lr.BookId)
        .OnDelete(DeleteBehavior.Cascade); // Giữ lại hành động này nếu muốn

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


        // Thiết lập mối quan hệ giữa Rating và Book
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Book)          // Một Rating có một Book
            .WithMany(b => b.Ratings)     // Một Book có nhiều Ratings
            .HasForeignKey(r => r.BookId); // Khóa ngoại trên Rating trỏ tới BookId

        // Thiết lập mối quan hệ giữa Rating và User
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.User)          // Một Rating có một User
            .WithMany(u => u.Ratings)     // Một User có nhiều Ratings
            .HasForeignKey(r => r.UserId); // Khóa ngoại trên Rating trỏ tới UserId

    }

}
