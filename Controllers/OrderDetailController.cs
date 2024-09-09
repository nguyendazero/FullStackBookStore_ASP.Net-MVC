using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.BookStore.Models;
using DotNet.BookStore.Services;
using DotNet.LaptopStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.BookStore.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        private readonly ILaptopService _laptopService;

        public OrderDetailController(IOrderDetailService orderDetailService, IOrderService orderService, ILaptopService laptopService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
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

        public IActionResult Save(OrderDetail orderDetail)
        {
            return View();
        }
    }
}