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
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;
        private readonly ILaptopService _laptopService;
        private readonly IUserService _userService;
        private readonly ICouponService _couponService;

        public CartController(ICartService cartService, ILaptopService laptopService, IUserService userService, ICouponService couponService, ICartItemService cartItemService)
        {
            _cartService = cartService;
            _laptopService = laptopService;
            _userService = userService;
            _couponService = couponService;
            _cartItemService = cartItemService;
        }
        public IActionResult Index(int id)
        {
            var cart = _cartService.GetCartByIdUser(id);
            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItem>();
            }
            double? total = cart.CartItems
                .Where(item => item.Laptop != null)
                .Sum(item => item.Laptop?.Price * item.Quantity);

            ViewBag.CartTotal = total;

            // Kiểm tra TempData và gán giá trị cho ViewBag nếu có giá trị discount
            if (TempData["discountedTotal"] != null)
            {
                ViewBag.DiscountTotal = TempData["discountedTotal"];
            }
            else
            {
                ViewBag.DiscountTotal = total; // Nếu không có giảm giá, dùng tổng giỏ hàng
            }

            return View(cart);
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

        public IActionResult Save(Cart cart)
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            var userJson = HttpContext.Session.GetString("User");
            if (string.IsNullOrEmpty(userJson))
                return RedirectToAction("Login_Page", "User");
            var user = JsonSerializer.Deserialize<User>(userJson);
            if (user == null)
                return RedirectToAction("Login", "User");

            _cartService.AddToCart(id, user);

            TempData["SuccessMessage"] = "Sản phẩm đã được thêm vào giỏ hàng!";

            return RedirectToAction("Details", "Laptop", new { id = id });
        }


        [HttpPost]
        public IActionResult ApplyCoupon(string couponCode)
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

            // Áp dụng mã giảm giá và tính tổng tiền sau khi giảm
            double discountedTotal = _cartService.ApplyCoupon(user, couponCode);

            if (discountedTotal > 0)
            {
                TempData["discountedTotal"] = discountedTotal.ToString();
                Coupon? coupon = _couponService.GetCouponByCode(couponCode);

                if (coupon != null)
                {
                    HttpContext.Session.SetInt32("couponId", coupon.Id);
                    Console.WriteLine(coupon.ToString());
                }
            }
            else
            {
                TempData["CouponError"] = "Invalid or expired coupon.";
            }
            return RedirectToAction("Index", "Cart", new { id = user.Id });
        }

        [HttpPost]
        public IActionResult Checkout(List<CartItem> CartItems, double Total, double DiscountTotal)
        {
            ViewBag.Total = Total;
            ViewBag.DiscountTotal = DiscountTotal;
            return View(CartItems);
        }
    }
}