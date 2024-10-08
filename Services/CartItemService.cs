using Microsoft.EntityFrameworkCore;
using DotNet.LaptopStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.LaptopStore.Data;

namespace DotNet.LaptopStore.Services
{
    public interface ICartItemService
    {

        List<CartItem> GetAllCartItemsByCartId(int cartId);
        CartItem SaveCartItem(CartItem cartItem);
        void DeleteCartItemById(int id);
        CartItem? GetCartItemById(int itemId);
        void RemoveAllCartItemsByCartId(int cartId);
        string IncreaseQuantity(int cartItemId);
        void DecreaseQuantity(int cartItemId);
    }

    public class CartItemService : ICartItemService
    {
        private readonly DataContext _dataContext;

        public CartItemService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<CartItem> GetAllCartItemsByCartId(int cartId)
        {
            return _dataContext.CartItems
                .Include(ci => ci.Laptop)
                .Include(ci => ci.Cart)
                .Where(ci => ci.CartId == cartId)
                .ToList();
        }

        public CartItem SaveCartItem(CartItem cartItem)
        {
            _dataContext.CartItems.Add(cartItem);
            _dataContext.SaveChanges();
            return cartItem;
        }

        public void DeleteCartItemById(int id)
        {
            var cartItem = _dataContext.CartItems.Find(id);
            if (cartItem == null) return;

            _dataContext.CartItems.Remove(cartItem);
            _dataContext.SaveChanges();
        }

        public CartItem? GetCartItemById(int itemId)
        {
            return _dataContext.CartItems
                .Include(ci => ci.Laptop)
                .Include(ci => ci.Cart)
                .FirstOrDefault(ci => ci.Id == itemId);
        }

        public void RemoveAllCartItemsByCartId(int cartId)
        {
            var cartItems = _dataContext.CartItems.Where(ci => ci.CartId == cartId).ToList();
            _dataContext.CartItems.RemoveRange(cartItems);
            _dataContext.SaveChanges();
        }

        public string IncreaseQuantity(int cartItemId)
        {
            var cartItem = _dataContext.CartItems.Find(cartItemId);
            if (cartItem == null) return "Cart item not found.";

            cartItem.Quantity += 1;
            _dataContext.SaveChanges();
            return "Quantity increased.";
        }

        public void DecreaseQuantity(int cartItemId)
        {
            var cartItem = _dataContext.CartItems.Find(cartItemId);
            if (cartItem == null) return;

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
            }
            else
            {
                _dataContext.CartItems.Remove(cartItem);
            }

            _dataContext.SaveChanges();
        }
    }
}
