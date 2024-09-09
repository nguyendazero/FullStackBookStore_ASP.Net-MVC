using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.LaptopStore.Models;
using DotNet.LaptopStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.LaptopStore.Controllers
{
    public class LaptopController : Controller
    {
        private readonly ILaptopService _laptopService;
        private readonly ICategoryService _categoryService;
        private readonly IColorService _colorService;

        public LaptopController(ILaptopService laptopService, ICategoryService categoryService, IColorService colorService)
        {
            _laptopService = laptopService;
            _categoryService = categoryService;
            _colorService = colorService;
        }
        public IActionResult Index()
        {
            var laptops = _laptopService.GetAllLaptops();
            return View(laptops);
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

        public IActionResult Save(Laptop Laptop)
        {
            return View();
        }
    }
}