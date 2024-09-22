using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.LaptopStore.Models;
using DotNet.LaptopStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BCrypt.Net;

namespace DotNet.LaptopStore.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRatingService _ratingService;
        private readonly ILaptopService _laptopService;

        private readonly ICartService _cartService;

        public UserController(IUserService userService, IRatingService ratingService, ILaptopService laptopService, ICartService cartService)
        {
            _userService = userService;
            _ratingService = ratingService;
            _laptopService = laptopService;
            _cartService = cartService;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Login_Page()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            string userName = Request.Form["username"].FirstOrDefault() ?? string.Empty;
            string password = Request.Form["password"].FirstOrDefault() ?? string.Empty;

            var user = _userService.GetUserByUsername(userName.Trim());
            if (user != null && BCrypt.Net.BCrypt.Verify(password.Trim(), user.Password))
            {
                // Nạp cart của người dùng từ database
                user.Cart = _cartService.GetCartByIdUser(user.Id);

                var userJson = JsonSerializer.Serialize(user);
                HttpContext.Session.SetString("User", userJson);

                // In ra thông tin session
                Console.WriteLine($"User session: {HttpContext.Session.GetString("User")}");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!!";
                return View("Login_Page");
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
        public IActionResult ChangePassword()
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

            // Kiểm tra mật khẩu hiện tại bằng cách sử dụng BCrypt để so sánh
            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
            {
                TempData["error"] = "Mật khẩu hiện tại không đúng!";
            }
            else if (!newPassword.Equals(confirmPassword))
            {
                TempData["error"] = "Mật khẩu nhập lại không khớp!";
            }
            else if (BCrypt.Net.BCrypt.Verify(newPassword, user.Password))
            {
                TempData["error"] = "Mật khẩu mới không được trùng với mật khẩu cũ!";
            }
            else
            {
                // Mã hóa mật khẩu mới trước khi cập nhật
                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                _userService.UpdateUser(user);
                TempData["success"] = "Đã thay đổi mật khẩu thành công";
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

        public IActionResult Register_Page()
        {
            return View();
        }

        public IActionResult Register(User newUser)
        {
            var existUser = _userService.GetAllUsers();
            foreach (var user in existUser)
            {
                if (user.UserName.Equals(newUser.UserName.Trim()))
                {
                    ViewBag.error = "Username đã tồn tại!";
                    return View("Register_Page");
                }
            }

            newUser.UserName = newUser.UserName.Trim();
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password.Trim());

            _userService.CreateUser(newUser);

            // Khởi tạo Cart cho người dùng mới
            var cart = new Cart { UserId = newUser.Id };
            _cartService.CreateCart(cart);

            // Gán Cart vào User
            newUser.Cart = cart;

            ViewBag.Success = "Đăng ký thành công, hãy đăng nhập!";
            return View("Login_Page");
        }

    }
}