using Microsoft.EntityFrameworkCore;
using DotNet.BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using DotNet.BookStore.Data;

namespace DotNet.BookStore.Services
{
    public interface ICartItemService
    {
        void AddToCart(int LaptopId, User user);
        void RemoveFromCart(int cartItemId, User user);
        List<CartItem> GetAllCartItemsByCartId(int cartId);
        CartItem SaveCartItem(CartItem cartItem);
        void DeleteCartItem(int id);
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

        public void AddToCart(int LaptopId, User user)
        {
            var cart = _dataContext.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.UserId == user.Id);
            if (cart == null)
            {
                cart = new Cart { UserId = user.Id };
                _dataContext.Carts.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.LaptopId == LaptopId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    LaptopId = LaptopId,
                    CartId = cart.Id,
                    Quantity = 1
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += 1;
            }

            _dataContext.SaveChanges();
        }

        public void RemoveFromCart(int cartItemId, User user)
        {
            var cartItem = _dataContext.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefault(ci => ci.Id == cartItemId);

            if (cartItem == null || cartItem.Cart?.UserId != user.Id)
            {
                return;
            }
            _dataContext.CartItems.Remove(cartItem);
            _dataContext.SaveChanges();
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

        public void DeleteCartItem(int id)
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
