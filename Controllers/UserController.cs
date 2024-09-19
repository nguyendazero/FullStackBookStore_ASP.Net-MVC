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

        public IActionResult ChangePassword_Page()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangPassword()
        {
            var userJson = HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToAction("Login", "User");
            }

            var user = JsonSerializer.Deserialize<User>(userJson);

            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }

            string currentPassword = Request.Form["currentPassword"].FirstOrDefault() ?? string.Empty;
            string newPassword = Request.Form["newPassword"].FirstOrDefault() ?? string.Empty;
            string confirmPassword = Request.Form["confirmPassword"].FirstOrDefault() ?? string.Empty;

            if (!currentPassword.Equals(user.Password))
            {
                TempData["error"] = "Mật khẩu hiện tại không đúng!";
            }
            else if (!newPassword.Equals(confirmPassword))
            {
                TempData["error"] = "Mật khẩu nhập lại không khớp!";
            }
            else if (currentPassword.Equals(newPassword))
            {
                TempData["error"] = "Mật khẩu mới không được trùng với mật khẩu cũ!";
            }
            else
            {
                TempData["success"] = "Đã thay đổi mật khẩu thành công";
                user.Password = newPassword;
                _userService.UpdateUser(user);
            }

            return RedirectToAction("ChangePassword_Page", "User");
        }

        public async Task<IActionResult> ChangeUserInfo(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                TempData["error"] = "User not found.";
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult ChangeUserInfo(User userChange)
        {
            var userJson = HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToAction("Login", "User");
            }

            var user = JsonSerializer.Deserialize<User>(userJson);

            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }
            // Update user information
            user.FullName = userChange.FullName;
            user.Address = userChange.Address;
            user.DateOfBirth = userChange.DateOfBirth;
            user.Email = userChange.Email;
            user.Telephone = userChange.Telephone;
            user.Gender = userChange.Gender;
            user.UserName = user.UserName;
            user.Password = user.Password;
            user.Role = user.Role;

            _userService.UpdateUser(user);
            TempData["success"] = "Thay đổi thông tin thành công.";
            return RedirectToAction("ChangeUserInfo", "User");
        }
    }
}