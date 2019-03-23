using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTest.Models;
using MVCTest.Models.Product;
using MVCTest.Models.User;


namespace MVCTest.Controllers
{
    public class HomeController : BaseController
    {

        SiteDbContext db;
        public HomeController(SiteDbContext context) : base(context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            //var cart = db2.Orders.Include(i => i.OrderItems).Single(i => i.OrderId == 4).OrderItems;

            return View(db.Products.ToList());
            //return View(cart.ToList());
        }


        [HttpGet]
        public IActionResult Buy(int id)
        {
            ViewBag.ProductId = id;
            var product = db.Products.SingleOrDefault(i => i.Id == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Buy(int ProductId, int Quantity)
        {

            var orderitem = new OrderItem();
            var product = db.Products.SingleOrDefault(i => i.Id == ProductId);//may be null

            orderitem.Product = product;
            orderitem.Quantity = Quantity;

            var cartId = HttpContext.Request.Cookies["order-id"];
            if (String.IsNullOrEmpty(cartId))
            {
                var order = new Order();
                orderitem.Order = order;
                db.OrderItems.Add(orderitem);
                db.Orders.Add(order);
                db.SaveChanges();


                HttpContext.Response.Cookies.Append("order-id", Convert.ToString(order.Id));

            }
            else
            {
                var cart = db.Orders.Include(i=>i.OrderItems)
                    .SingleOrDefault(i => i.Id == Convert.ToInt32(cartId));

                var prodInCart = cart.OrderItems.SingleOrDefault(i => i.ProductId == ProductId && i.OrderId == Convert.ToInt32(cartId));
                if (prodInCart == null)
                {
                    db.OrderItems.Add(orderitem);
                    cart.OrderItems.Add(orderitem);
                }
                else
                {
                    prodInCart.Quantity += Quantity;
                }
                db.SaveChanges();
            }


            return View("~/Views/Home/Success.cshtml");
        }


        [HttpGet]
        public IActionResult Cart()
        {
            var cartId = HttpContext.Request.Cookies["order-id"];
            var cart = db.OrderItems.Include(i => i.Order)
                .Include(i => i.Product)
                .Where(i => i.OrderId == Convert.ToInt32(cartId));

            return View(cart.ToList());
        }

        [HttpPost]
        public IActionResult Cart(int[] Quantity, string[] ItemsToDelete, int OrderId)
        {
            var order = db.Orders.SingleOrDefault(i => i.Id == OrderId);
            var orderItems = db.OrderItems.Where(i => i.OrderId == OrderId);
            int it = 0;
            foreach (var item in orderItems)
            {
                if (item.Quantity != Quantity[it]) 
                {
                    item.Quantity = Quantity[it];
                }
                it++;
            }

            if (ItemsToDelete.FirstOrDefault() != null)
            {
                foreach (var item in ItemsToDelete)
                {
                    db.OrderItems.Remove(orderItems.SingleOrDefault(i => i.Id == Convert.ToInt32(item)));//TODO: if no elements display that cart is empty
                    db.SaveChanges();
                }
                var cart = db.OrderItems.Include(i => i.Product)
                    .Where(i => i.OrderId == OrderId).ToArray();
                return View(cart.ToList());
            }
            else
            {
                order.IsSubmitted = true;
                HttpContext.Response.Cookies.Append("order-id", "");//cookie will be deleted if order is submitted

                db.OrderItems.UpdateRange(orderItems);
                db.Orders.Update(order);
                db.SaveChanges();

                return View();//TODO: redirect to success page
            }


        }
    }
}