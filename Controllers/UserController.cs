using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.BookStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRatingService _ratingService;
        private readonly IBookService _bookService;

        public UserController(IUserService userService, IRatingService ratingService, IBookService bookService)
        {
            _userService = userService;
            _ratingService = ratingService;
            _bookService = bookService;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}