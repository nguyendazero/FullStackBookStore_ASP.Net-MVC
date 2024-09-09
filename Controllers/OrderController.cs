using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.BookStore.Models;
using DotNet.BookStore.Services;
using DotNet.LaptopStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly ILaptopService _laptopService;
        private readonly ICouponService _couponService;

        public OrderController(IOrderService orderService, ICartService cartService, ILaptopService laptopService, ICouponService couponService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _laptopService = laptopService;
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

        public IActionResult Save(Order order)
        {
            return View();
        }
    }
}