using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly ICouponService _couponService;

        public CartController(ICartService cartService, IBookService bookService, IUserService userService, ICouponService couponService)
        {
            _cartService = cartService;
            _bookService = bookService;
            _userService = userService;
            _couponService = couponService;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}