using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.LaptopStore.Models;
using DotNet.LaptopStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.LaptopStore.Controllers
{
    public class ColorController : Controller
    {
        private readonly IColorService _ColorService;
        private readonly ILaptopService _laptopService;

        public ColorController(IColorService ColorService, ILaptopService laptopService)
        {
            _ColorService = ColorService;
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

        public IActionResult Save(Color Color)
        {
            return View();
        }
    }
}