using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.BookStore.Models;
using DotNet.BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.BookStore.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IBookService _bookService;
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartService cartService, IBookService bookService, ICartItemService cartItemService)
        {
            _cartService = cartService;
            _bookService = bookService;
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
    }
}