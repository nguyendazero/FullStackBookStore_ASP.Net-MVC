using Microsoft.EntityFrameworkCore;
using DotNet.LaptopStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.LaptopStore.Data;

namespace DotNet.LaptopStore.Services
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetAllOrderDetailByIdOrder(int id);

        OrderDetail SaveOrderDetail(OrderDetail orderDetail);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly DataContext _dataContext;

        public OrderDetailService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<OrderDetail> GetAllOrderDetailByIdOrder(int id)
        {
            return _dataContext.OrderDetails
                .Where(od => od.OrderId == id)
                .Include(od => od.Laptop)  // Nếu cần, thêm các liên kết khác
                .ToList();
        }

        public OrderDetail SaveOrderDetail(OrderDetail orderDetail)
        {
            _dataContext.Entry(orderDetail).State = orderDetail.Id == 0 ? EntityState.Added : EntityState.Modified;
            _dataContext.SaveChanges();
            return orderDetail;
        }
    }
}
