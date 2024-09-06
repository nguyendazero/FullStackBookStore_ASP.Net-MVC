using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.BookStore.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;

        public RatingController(IRatingService ratingService, IBookService bookService, IUserService userService)
        {
            _ratingService = ratingService;
            _bookService = bookService;
            _userService = userService;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}