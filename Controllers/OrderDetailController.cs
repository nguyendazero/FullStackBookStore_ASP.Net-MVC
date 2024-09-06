using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet.BookStore.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;

        public OrderDetailController(IOrderDetailService orderDetailService, IOrderService orderService, IBookService bookService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _bookService = bookService;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}