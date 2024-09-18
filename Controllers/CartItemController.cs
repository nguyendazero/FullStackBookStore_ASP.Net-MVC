using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.LaptopStore.Models;
using DotNet.LaptopStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DotNet.LaptopStore.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ILaptopService _laptopService;
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartService cartService, ILaptopService laptopService, ICartItemService cartItemService)
        {
            _cartService = cartService;
            _laptopService = laptopService;
            _cartItemService = cartItemService;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

        public IActionResult Save(CartItem cartItem)
        {
            return View();
        }

        public IActionResult RemoveToCart(int id)
        {
            var userJson = HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToAction("Login", "User");
            }

            var user = JsonSerializer.Deserialize<User>(userJson);

            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            _cartItemService.DeleteCartItemById(id);
            return RedirectToAction("Index", "Cart", new { id = user.Id });
        }

        public IActionResult Plus(int id)
        {
            var userJson = HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToAction("Login", "User");
            }

            var user = JsonSerializer.Deserialize<User>(userJson);

            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            _cartItemService.IncreaseQuantity(id);
            return RedirectToAction("Index", "Cart", new { id = user.Id });

        }

        public IActionResult Minus(int id)
        {
            var userJson = HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToAction("Login", "User");
            }

            var user = JsonSerializer.Deserialize<User>(userJson);

            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            _cartItemService.DecreaseQuantity(id);
            return RedirectToAction("Index", "Cart", new { id = user.Id });

        }

        [HttpPost]
        public IActionResult Checkout(IEnumerable<CartItem> CartItems, double CartTotal, double DiscountedTotal)
        {
            // Kiểm tra thông tin người dùng từ session
            var userJson = HttpContext.Session.GetString("User");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToAction("Login", "User");
            }

            var user = JsonSerializer.Deserialize<User>(userJson);
            if (user?.Cart == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Tạo danh sách CartItems từ thông tin gửi lên
            var cartItems = CartItems.ToList();

            // Đặt dữ liệu vào TempData
            TempData["CartTotal"] = CartTotal.ToString("F2");
            TempData["DiscountedTotal"] = DiscountedTotal.ToString("F2");

            // Trả về view với CartItems nếu cần
            return View();
        }


    }
}