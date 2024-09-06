using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.BookStore.Controllers
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
    }
}