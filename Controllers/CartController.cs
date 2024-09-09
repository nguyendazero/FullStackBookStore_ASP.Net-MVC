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

        public IActionResult Save(Cart cart)
        {
            return View();
        }
    }
}