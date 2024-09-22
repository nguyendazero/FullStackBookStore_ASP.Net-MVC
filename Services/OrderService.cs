using Microsoft.EntityFrameworkCore;
using DotNet.LaptopStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.LaptopStore.Data;

namespace DotNet.LaptopStore.Services
{
    public interface IOrderService
    {
        List<Order> GetAllOrdersByUserId(int idUser);
        List<Order> GetAllOrders();
        Order SaveOrder(Order order);
        Order? GetOrderById(int orderId);
        void DeleteOrder(int orderId);
        void SaveOrderDetails(List<OrderDetail> orderDetails);
        Order CreateOrder(User user, List<CartItem> cartItems, string paymentMethod, int? couponId);
        bool HasUserPurchasedLaptop(User user, int LaptopId);
    }

    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private readonly ICouponService _couponService;

        private readonly ILaptopService _laptopService;



        public OrderService(DataContext dataContext, ICouponService couponService, ILaptopService laptopService)
        {
            _dataContext = dataContext;
            _couponService = couponService;
            _laptopService = laptopService;

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
                .Sum(ci => ci.Quantity * (double)ci.Laptop!.Price);

            if (couponId.HasValue)
            {
                Coupon? coupon = _couponService.GetCouponById(couponId.Value);
                if (coupon != null)
                {
                    decimal discountedTotal = total - (total * coupon.Discount) / 100;
                    total = discountedTotal;
                }
            }
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
                .Where(ci => ci.Laptop != null)
                .Select(ci => new OrderDetail
                {
                    Quantity = ci.Quantity,
                    OrderId = order.Id,
                    LaptopId = ci.LaptopId,
                    Price = (decimal)ci.Laptop!.Price,

                }).ToList();

            SaveOrderDetails(orderDetails);

            foreach (var orderDetail in orderDetails)
            {
                var laptop = _laptopService.GetLaptopById(orderDetail.LaptopId);
                if (laptop != null)
                {
                    laptop.Quantity -= orderDetail.Quantity;
                    laptop.Status = laptop.Quantity == 0 ? "runout" : laptop.Status;
                    _dataContext.Laptops.Update(laptop);
                    _dataContext.SaveChanges();
                }

            }
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
