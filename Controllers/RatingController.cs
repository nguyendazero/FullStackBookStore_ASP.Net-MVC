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
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;
        private readonly ILaptopService _laptopService;
        private readonly IUserService _userService;

        public RatingController(IRatingService ratingService, ILaptopService laptopService, IUserService userService)
        {
            _ratingService = ratingService;
            _laptopService = laptopService;
            _userService = userService;
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

        public IActionResult Save(Rating rating)
        {
            return View();
        }
    }
}