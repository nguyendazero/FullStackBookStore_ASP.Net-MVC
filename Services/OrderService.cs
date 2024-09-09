using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.BookStore.Data;

namespace DotNet.BookStore.Services
{
    public interface IOrderService
    {
        List<Order> GetAllOrdersByUserId(int idUser);
        List<Order> GetAllOrders();
        Order SaveOrder(Order order);
        Order? GetOrderById(int id);
        void DeleteOrder(int id);
        void SaveOrderDetails(List<OrderDetail> orderDetails);
        Order CreateOrder(User user, List<CartItem> cartItems, string paymentMethod, int? couponId);
        bool HasUserPurchasedLaptop(User user, int LaptopId);
    }

    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;

        public OrderService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Order> GetAllOrdersByUserId(int idUser)
        {
            return _dataContext.Orders
                .Where(o => o.UserId == idUser)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Laptop)
                .ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _dataContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Laptop)
                .ToList();
        }

        public Order SaveOrder(Order order)
        {
            _dataContext.Orders.Add(order);
            _dataContext.SaveChanges();
            return order;
        }

        public Order? GetOrderById(int id)
        {
            return _dataContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Laptop)
                .FirstOrDefault(o => o.Id == id);
        }

        public void DeleteOrder(int id)
        {
            var order = _dataContext.Orders.Find(id);
            if (order != null)
            {
                _dataContext.Orders.Remove(order);
                _dataContext.SaveChanges();
            }
        }

        public void SaveOrderDetails(List<OrderDetail> orderDetails)
        {
            foreach (var detail in orderDetails)
            {
                _dataContext.OrderDetails.Add(detail);
            }
            _dataContext.SaveChanges();
        }

        public Order CreateOrder(User user, List<CartItem> cartItems, string paymentMethod, int? couponId)
        {
            // Tính tổng số tiền và xử lý Laptop có thể là null
            decimal total = (decimal)cartItems
                .Where(ci => ci.Laptop != null) // Loại bỏ các CartItem có Laptop là null
                .Sum(ci => ci.Quantity * (double)ci.Laptop!.Price);

            var order = new Order
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Total = total,
                Status = "Pending", // Default status
                PaymentMethod = paymentMethod,
                UserId = user.Id,
                CouponId = couponId
            };

            _dataContext.Orders.Add(order);
            _dataContext.SaveChanges();

            var orderDetails = cartItems
                .Where(ci => ci.Laptop != null) // Loại bỏ các CartItem có Laptop là null
                .Select(ci => new OrderDetail
                {
                    OrderId = order.Id,
                    LaptopId = ci.LaptopId,
                    Price = (decimal)ci.Laptop!.Price, // Chuyển đổi kiểu từ double sang decimal
                                                       // Add other properties if needed
                }).ToList();

            SaveOrderDetails(orderDetails);

            return order;
        }


        public bool HasUserPurchasedLaptop(User user, int LaptopId)
        {
            return _dataContext.Orders
                .Any(o => o.UserId == user.Id &&
                          o.OrderDetails.Any(od => od.LaptopId == LaptopId));
        }
    }
}
