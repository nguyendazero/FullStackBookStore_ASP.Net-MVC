using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.LaptopStore.Models;
using DotNet.LaptopStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.LaptopStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ILaptopService _laptopService;
        private readonly IUserService _userService;
        private readonly ICouponService _couponService;

        public CartController(ICartService cartService, ILaptopService laptopService, IUserService userService, ICouponService couponService)
        {
            _cartService = cartService;
            _laptopService = laptopService;
            _userService = userService;
            _couponService = couponService;
        }
        public IActionResult Index(int id)
        {
            var cart = _cartService.GetCartByIdUser(id);
            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItem>();
            }
            double? total = cart.CartItems
            .Where(item => item.Laptop != null)
            .Sum(item => item.Laptop?.Price * item.Quantity);

            ViewBag.CartTotal = total;
            return View(cart);
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

        public IActionResult Save(Cart cart)
        {
            return View();
        }
    }
}