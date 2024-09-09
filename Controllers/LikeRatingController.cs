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
    public class LikeRatingController : Controller
    {
        private readonly ILikeRatingService _likeRatingService;
        private readonly ILaptopService _laptopService;
        private readonly IUserService _userService;

        public LikeRatingController(ILikeRatingService likeRatingService, ILaptopService laptopService, IUserService userService)
        {
            _likeRatingService = likeRatingService;
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

        public IActionResult Save(LikeRating likeRating)
        {
            return View();
        }
    }
}