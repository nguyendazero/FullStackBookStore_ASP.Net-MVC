using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.LaptopStore.Models;
using DotNet.LaptopStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.LaptopStore.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        private readonly IOrderService _orderService;

        public CouponController(ICouponService couponService, IOrderService orderService)
        {
            _couponService = couponService;
            _orderService = orderService;
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

        public IActionResult Save(Coupon coupon)
        {
            return View();
        }
    }
}