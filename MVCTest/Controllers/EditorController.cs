using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MVCTest.Models;
using MVCTest.Models.Product;

namespace MVCTest.Controllers
{
    public class EditorController : BaseController
    {
        SiteDbContext db;
        public EditorController(SiteDbContext context) : base(context)
        {
            db = context;
        }

        // GET: Editor
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Editor/Product.cshtml");
        }


        [HttpPost]
        public async Task<ActionResult> Index(string Name, string Price, int Quantity, string Description, string ImagePath, string SubCategory)
        {
            if (String.IsNullOrEmpty(ImagePath))
            {
                ImagePath = $"/Products/Images/{Name}.jpg";
            }

            var product = new Product();

            product.Name = Name;
            product.Price = Convert.ToDouble(Price);
            product.Quantity = Quantity;
            product.Description = Description;
            product.ImagePath = ImagePath;
            product.SubCategory = db.SubCategories.SingleOrDefault(s => s.Name == SubCategory);



            await db.AddAsync(product);
            await db.SaveChangesAsync();

            return View("~/Views/Home/Index.cshtml"); //TODO: redirect to success
        }


    }
}