using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.LaptopStore.Models;
using DotNet.LaptopStore.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}