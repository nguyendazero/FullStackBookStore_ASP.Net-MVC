using Microsoft.EntityFrameworkCore;
using DotNet.LaptopStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.LaptopStore.Data;

namespace DotNet.LaptopStore.Services
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetAllOrderDetailByIdOrder(int orderid);

        OrderDetail SaveOrderDetail(OrderDetail orderDetail);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly DataContext _dataContext;

        public OrderDetailService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<OrderDetail> GetAllOrderDetailByIdOrder(int orderid)
        {
            return _dataContext.OrderDetails
                .Where(od => od.OrderId == orderid)
                .Include(od => od.Laptop)
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
