using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MVCTest.Models.Product;

namespace MVCTest.Controllers
{
    public class HomeController : Controller
    {
        ProductContext db;
        public HomeController(ProductContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }


        [HttpGet]
        public IActionResult Buy(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Buy(Order order)
        {
            db.Orders.Add(order); 
            db.SaveChanges();//Save changes
            var productName = db.Products.Single(item => item.Id == order.ProductId).Name;//Select product name from db

            ViewBag.ProductQuantity = order.Quantity;
            ViewBag.ProductName = productName;
            return View("~/Views/Home/Success.cshtml");
        }

    }
}