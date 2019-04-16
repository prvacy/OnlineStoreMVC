using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MVCTest.Models;
using MVCTest.Models.Product;
using MVCTest.Models.ViewModels;
using MVCTest.Services;

namespace MVCTest.Controllers
{
    public class HomeController : BaseController
    {

        SiteDbContext db;
        IFilterService filterService;

        public HomeController(SiteDbContext context, IFilterService _filter) : base(context)
        {
            db = context;
            filterService = _filter;
        }


        public async Task<IActionResult> Index(bool loadAll, int? subcatId, double? minPrice, double? maxPrice, int? page)
        {

            var currentSubc = TempData["currentSubc"] as int?;
            if (!subcatId.HasValue && currentSubc.HasValue)
            {
                subcatId = currentSubc;
            }

            if (loadAll)
            {
                subcatId = null;
            }

            var vm = new IndexVM
            {
                Categories = await db.Categories
                    .Include(c => c.SubCategories)
                    .Select(i => i).ToListAsync(),

                Products = await
                    filterService.Filter(new Models.Filters.IndexFilter
                    {
                        SubCategoryId = subcatId,
                        MinPrice = minPrice,
                        MaxPrice = maxPrice,
                        Page = page
                    }),

                PagesCount = filterService.PagesCount

            };

            if (subcatId.HasValue)
            {
                TempData["currentSubc"] = subcatId;
            }

            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> Buy(int id)
        {
            ViewBag.ProductId = id;
            var product = await db.Products.SingleOrDefaultAsync(i => i.Id == id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(int ProductId, int Quantity)
        {

            var orderitem = new OrderItem();
            var product = await db.Products.SingleOrDefaultAsync(i => i.Id == ProductId);//may be null

            orderitem.Product = product;
            orderitem.Quantity = Quantity;

            var cartId = HttpContext.Request.Cookies["order-id"];
            if (String.IsNullOrEmpty(cartId))
            {
                var order = new Order();
                orderitem.Order = order;
                await db.OrderItems.AddAsync(orderitem);
                await db.Orders.AddAsync(order);
                await db.SaveChangesAsync();


                HttpContext.Response.Cookies.Append("order-id", Convert.ToString(order.Id));

            }
            else
            {
                var cart = await db.Orders
                    .Include(i => i.OrderItems)
                    .SingleOrDefaultAsync(i => i.Id == Convert.ToInt32(cartId));

                var prodInCart = cart.OrderItems
                    .SingleOrDefault(i => i.ProductId == ProductId && i.OrderId == Convert.ToInt32(cartId));
                if (prodInCart == null)
                {
                    await db.OrderItems.AddAsync(orderitem);
                    cart.OrderItems.Add(orderitem);
                }
                else
                {
                    prodInCart.Quantity += Quantity;
                }
                await db.SaveChangesAsync();
            }


            return View("~/Views/Home/Success.cshtml");
        }


        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var cartId = HttpContext.Request.Cookies["order-id"];
            var cart = db.OrderItems
                .Include(i => i.Order)
                .Include(i => i.Product)
                .Where(i => i.OrderId == Convert.ToInt32(cartId));

            return View(await cart.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Cart(int[] Quantity, string[] ItemsToDelete, int OrderId)
        {
            var order = await db.Orders.SingleOrDefaultAsync(i => i.Id == OrderId);
            var orderItems = db.OrderItems.Where(i => i.OrderId == OrderId);
            int it = 0;

            orderItems.AsParallel()
                .Select(all => all)
                .ForAll(item =>
                {
                    if (item.Quantity != Quantity[it])
                    {
                        item.Quantity = Quantity[it];
                    }
                    it++;
                });
            

            //foreach (var item in orderItems)
            //{
            //    if (item.Quantity != Quantity[it]) 
            //    {
            //        item.Quantity = Quantity[it];
            //    }
            //    it++;
            //}

            if (ItemsToDelete.FirstOrDefault() != null)
            {
                foreach (var item in ItemsToDelete)
                {
                    db.OrderItems.Remove(await orderItems.SingleOrDefaultAsync(i => i.Id == Convert.ToInt32(item)));//TODO: if no elements display that cart is empty
                    await db.SaveChangesAsync();
                }
                var cart = db.OrderItems.Include(i => i.Product)
                    .Where(i => i.OrderId == OrderId);
                return View(await cart.ToListAsync());
            }
            else
            {
                order.IsSubmitted = true;
                HttpContext.Response.Cookies.Append("order-id", "");//cookie will be deleted if order is submitted

                db.OrderItems.UpdateRange(orderItems);
                db.Orders.Update(order);
                await db.SaveChangesAsync();

                return View();//TODO: redirect to success page
            }


        }
    }
}