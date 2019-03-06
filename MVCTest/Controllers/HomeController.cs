using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MVCTest.Models.Product;
using MVCTest.Models.User;


namespace MVCTest.Controllers
{
    public class HomeController : BaseController
    {

        ProductContext db2;
        public HomeController(UserContext context, ProductContext productContext) : base(context)
        {
            db2 = productContext;
        }




        public IActionResult Index()
        {
            return View(db2.Products.ToList());
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
            db2.Orders.Add(order); 
            db2.SaveChanges();//Save changes
            var productName = db2.Products.Single(item => item.Id == order.ProductId).Name;//Select product name from db

            ViewBag.ProductQuantity = order.Quantity;
            ViewBag.ProductName = productName;
            return View("~/Views/Home/Success.cshtml");
        }

    }
}