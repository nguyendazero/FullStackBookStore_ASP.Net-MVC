using Microsoft.EntityFrameworkCore;
using DotNet.LaptopStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.LaptopStore.Data;

namespace DotNet.LaptopStore.Services
{
    public interface ICouponService
    {
        List<Coupon> GetAllCoupons();
        Coupon? GetCouponById(int id);
        void CreateCoupon(Coupon coupon);
        void UpdateCoupon(Coupon coupon);
        void DeleteCoupon(int id);
        Coupon? GetCouponByCode(string couponCode);
    }

    public class CouponService : ICouponService
    {
        private readonly DataContext _dataContext;

        public CouponService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Coupon? GetCouponByCode(string couponCode)
        {
            // Lấy thông tin mã giảm giá từ bảng Coupons, có thể trả về null
            var coupon = _dataContext.Coupons
                .FirstOrDefault(c => c.Code == couponCode && c.Expiry >= DateOnly.FromDateTime(DateTime.Now));

            return coupon;
        }
        public List<Coupon> GetAllCoupons()
        {
            return _dataContext.Coupons.ToList();
        }

        public Coupon? GetCouponById(int id)
        {
            return _dataContext.Coupons.FirstOrDefault(c => c.Id == id);
        }

        public void CreateCoupon(Coupon coupon)
        {
            _dataContext.Coupons.Add(coupon);
            _dataContext.SaveChanges();
        }

        public void UpdateCoupon(Coupon coupon)
        {
            var existingCoupon = _dataContext.Coupons.FirstOrDefault(c => c.Id == coupon.Id);
            if (existingCoupon == null) return;

            existingCoupon.Code = coupon.Code;
            existingCoupon.Discount = coupon.Discount;
            existingCoupon.Expiry = coupon.Expiry;
            _dataContext.Coupons.Update(existingCoupon);
            _dataContext.SaveChanges();
        }

        public void DeleteCoupon(int id)
        {
            var coupon = _dataContext.Coupons.FirstOrDefault(c => c.Id == id);
            if (coupon == null) return;

            _dataContext.Coupons.Remove(coupon);
            _dataContext.SaveChanges();
        }
    }
}
