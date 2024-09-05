using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.BookStore.Data;

namespace DotNet.BookStore.Services
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
                .Include(od => od.Book)  // Nếu cần, thêm các liên kết khác
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
