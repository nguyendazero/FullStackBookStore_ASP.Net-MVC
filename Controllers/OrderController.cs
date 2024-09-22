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
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly ILaptopService _laptopService;
        private readonly ICouponService _couponService;
        private readonly IUserService _userService;
        private readonly ICartItemService _cartItemsService;
        public OrderController(IOrderService orderService, ICartService cartService, ILaptopService laptopService, ICouponService couponService, IUserService userService, ICartItemService cartItemService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _laptopService = laptopService;
            _couponService = couponService;
            _userService = userService;
            _cartItemsService = cartItemService;
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

        public IActionResult Save(Order order)
        {
            return View();
        }

        public IActionResult ProceedtoCheckout()
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

            Cart cart = _cartService.GetCartByIdUser(user.Id);

            List<CartItem> cartItems = cart.CartItems.ToList();
            string paymentMethod = Request.Form["paymentMethod"].FirstOrDefault() ?? string.Empty;
            int? couponId = HttpContext.Session.GetInt32("couponId");

            _orderService.CreateOrder(user, cartItems.ToList(), paymentMethod, couponId);


            _cartItemsService.RemoveAllCartItemsByCartId(cart.Id);

            HttpContext.Session.Remove("couponId");

            return View("ThankYou");
        }
    }
}