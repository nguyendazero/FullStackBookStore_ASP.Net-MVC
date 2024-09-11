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
        public IActionResult Index(string? status, int? categoryId)
        {
            List<Laptop> laptops = new List<Laptop>();
            var colors = _colorService.GetAllColors();
            var manufactures = _categoryService.GetAllCategories();
            ViewBag.Colors = colors;
            ViewBag.Manufactures = manufactures;
            if (string.IsNullOrEmpty(status) && categoryId == null)
            {
                laptops = _laptopService.GetAllLaptops();
                ViewBag.Title = "ALL PRODUCT";
            }
            else if (!string.IsNullOrEmpty(status))
            {
                laptops = _laptopService.GetLaptopsByStatus(status);
                ViewBag.Title = status;
            }
            else if (categoryId != null)
            {
                laptops = _laptopService.GetLaptopsByCategory(categoryId.Value);
                var cate = _categoryService.GetCategoryById(categoryId.Value);
                ViewBag.Title = cate?.Name;
            }
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

        public IActionResult Details(int id)
        {
            var laptop = _laptopService.GetLaptopById(id);
            return View(laptop);
        }


    }
}