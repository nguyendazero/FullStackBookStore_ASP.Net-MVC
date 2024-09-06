using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IBookService _bookService;
        private readonly ICouponService _couponService;

        public OrderController(IOrderService orderService, ICartService cartService, IBookService bookService, ICouponService couponService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _bookService = bookService;
            _couponService = couponService;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}