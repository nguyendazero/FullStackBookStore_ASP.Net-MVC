using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DotNet.LaptopStore.Models;
using LaptopStore.Models;

namespace DotNet.LaptopStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string? userJson = HttpContext.Session.GetString("User");

        User? user = null;

        if (!string.IsNullOrEmpty(userJson))
        {
            user = JsonSerializer.Deserialize<User>(userJson);
        }

        ViewBag.FullName = user?.FullName ?? "";

        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
