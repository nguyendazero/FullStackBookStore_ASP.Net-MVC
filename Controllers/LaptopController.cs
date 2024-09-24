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
        private readonly ICloudinaryService _cloudinaryService;


        public LaptopController(ILaptopService laptopService, ICategoryService categoryService, IColorService colorService, ICloudinaryService cloudinaryService)
        {
            _laptopService = laptopService;
            _categoryService = categoryService;
            _colorService = colorService;
            _cloudinaryService = cloudinaryService;
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
            _laptopService.DeleteLaptop(id);
            return RedirectToAction("Laptop", "Admin");
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

        //-----------------ADMIN--------------
        public IActionResult AddLaptopPage()
        {
            List<string> status = new List<string>() { "onsale", "discount", "runout" };
            ViewBag.Category = _categoryService.GetAllCategories();
            ViewBag.Color = _colorService.GetAllColors();
            ViewBag.Status = status;
            return View();
        }

        [HttpPost]
        public IActionResult Add(Laptop laptop, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var imageUrl = _cloudinaryService.UploadImage(image);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    laptop.Image = imageUrl;
                }
            }
            // Kiểm tra trạng thái của Model
            if (ModelState.IsValid)
            {
                _laptopService.CreateLaptop(laptop);
                return RedirectToAction("Laptop", "Admin");
            }
            return View("AddLaptopPage");
        }



    }
}