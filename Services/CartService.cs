using Microsoft.EntityFrameworkCore;
using DotNet.LaptopStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.LaptopStore.Data;

namespace DotNet.LaptopStore.Services
{
    public interface ICartService
    {
        Cart SaveCart(Cart cart);
        Cart GetCartByIdUser(int userId);
        void UpdateCart(Cart cart);
        void DeleteCart(int id);
        Cart ViewCart(User user);
        double ApplyCoupon(User user, string couponCode);
    }

    public class CartService : ICartService
    {
        private readonly DataContext _dataContext;

        public CartService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Cart SaveCart(Cart cart)
        {
            _dataContext.Carts.Add(cart);
            _dataContext.SaveChanges();
            return cart;
        }

        public Cart GetCartByIdUser(int userId)
        {
            return _dataContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Laptop)
                .FirstOrDefault(c => c.UserId == userId) ?? new Cart(); // Return a new Cart if not found
        }

        public void UpdateCart(Cart cart)
        {
            var existingCart = _dataContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Laptop)
                .FirstOrDefault(c => c.Id == cart.Id);

            if (existingCart == null) return;

            existingCart.CartItems = cart.CartItems;
            _dataContext.Carts.Update(existingCart);
            _dataContext.SaveChanges();
        }

        public void DeleteCart(int id)
        {
            var cart = _dataContext.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.Id == id);

            if (cart == null) return;

            _dataContext.CartItems.RemoveRange(cart.CartItems);
            _dataContext.Carts.Remove(cart);
            _dataContext.SaveChanges();
        }

        public Cart ViewCart(User user)
        {
            return GetCartByIdUser(user.Id);
        }

        public double ApplyCoupon(User user, string couponCode)
        {
            var cart = GetCartByIdUser(user.Id);
            if (cart == null) return 0;

            var coupon = _dataContext.Coupons
                .FirstOrDefault(c => c.Code == couponCode && c.Expiry >= DateOnly.FromDateTime(DateTime.Now));

            if (coupon == null) return 0;

            double discount = coupon.Discount;

            // Calculate total price ensuring that Laptop is not null
            double total = cart.CartItems
                .Sum(ci => (ci.Laptop != null ? ci.Quantity * ci.Laptop.Price : 0));

            double discountedTotal = total - (total * discount / 100);

            _dataContext.SaveChanges();
            return discountedTotal;
        }
    }
}
