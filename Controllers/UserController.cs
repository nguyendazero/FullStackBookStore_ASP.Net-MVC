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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRatingService _ratingService;
        private readonly ILaptopService _laptopService;

        public UserController(IUserService userService, IRatingService ratingService, ILaptopService laptopService)
        {
            _userService = userService;
            _ratingService = ratingService;
            _laptopService = laptopService;
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

        public IActionResult Save(User user)
        {
            return View();
        }
    }
}