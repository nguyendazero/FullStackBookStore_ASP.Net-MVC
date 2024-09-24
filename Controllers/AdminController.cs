using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DotNet.LaptopStore.Models;
using DotNet.LaptopStore.Services;

namespace DotNet.LaptopStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public AdminController(ILogger<AdminController> logger, IOrderService orderService, IUserService userService)
        {
            _logger = logger;
            _orderService = orderService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            string? userJson = HttpContext.Session.GetString("User");
            User? user = null;
            if (!string.IsNullOrEmpty(userJson))
                user = JsonSerializer.Deserialize<User>(userJson);

            var orders = _orderService.GetAllOrders();
            return View(orders);
        }


    }
}
