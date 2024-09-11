using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.LaptopStore.Models;
using DotNet.LaptopStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DotNet.LaptopStore.Controllers
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

        public IActionResult Login()
        {
            ViewData["Layout"] = "_SimpleLayout";
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            var user = _userService.GetUserByUsernameAndPassword(userName, password);
            if (user != null)
            {
                // Chuyển đổi toàn bộ đối tượng người dùng thành chuỗi JSON
                var userJson = JsonSerializer.Serialize(user);
                // Lưu chuỗi JSON vào session
                HttpContext.Session.SetString("User", userJson);
                return RedirectToAction("Index", "Home"); // Chuyển hướng sau khi đăng nhập
            }
            else
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!!";
                return View();
            }

        }
        // Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa session
            return RedirectToAction("Index", "Home");
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