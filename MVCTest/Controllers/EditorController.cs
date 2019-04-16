using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTest.Models;
using MVCTest.Models.Product;
using MVCTest.Models.ViewModels;

namespace MVCTest.Controllers
{
    public class EditorController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        SiteDbContext db;
        public EditorController(SiteDbContext context, IHostingEnvironment hostingEnvironment) : base(context)
        {
            db = context;
            _hostingEnvironment = hostingEnvironment;
        }

        //TODO: All users can go to this page!!!!!!!!

        // GET: Editor
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var categories = await db.Categories
                    .Include(c => c.SubCategories)
                    .ToListAsync();

            return View("~/Views/Editor/Product.cshtml", categories);
        }




        [HttpPost]
        public async Task<ActionResult> Index(Product product, string Price_string, string Weight_string, string Volume_string)
        {
            //var file = HttpContext.Request.Form.Files.First();
            //if (file != null)
            //{
            //    var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/Products/Images/", file.FileName);
            //    using (var fileStream = new FileStream(path, FileMode.Create))
            //    {
            //        await file.CopyToAsync(fileStream); //Upload file to /Products/Images/filename.ext
            //    }
            //    product.ImagePath = Path.Combine("/Products/Images/", file.FileName);
            //}
            //else
            //{
            //    if (String.IsNullOrEmpty(product.ImagePath))
            //    {
            //        product.ImagePath = $"/Products/Images/{product.Name}.jpg";
            //    }
            //}

            var path = await UploadImage(product.ImagePath);
            if (String.IsNullOrEmpty(path))
            {
                if (String.IsNullOrEmpty(product.ImagePath))
                {
                    product.ImagePath = $"/Products/Images/{product.Name}.jpg";
                }
            }

            //product.SubCategory = await db.SubCategories.SingleOrDefaultAsync(s => s.Name == subCategory);

            product.Price = Convert.ToDouble(Price_string);
            product.Weight = Convert.ToDouble(Weight_string);
            product.Volume = Convert.ToDouble(Volume_string);

            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();

            var cat = await db.Categories
                .Include(c => c.SubCategories)
                .ToListAsync();

            return View("~/Views/Editor/Product.cshtml", cat);
        }


        [HttpGet]
        public async Task<IActionResult> EditProduct(int productId)
        {
            var vm = new EditorProductVM
            {
                Product = await db.Products.FindAsync(productId),
                Categories = await db.Categories
                .Include(c => c.SubCategories)
                .ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(Product product, string Price_string, string Weight_string, string Volume_string)
        {
            var path = await UploadImage(product.ImagePath);
            if (String.IsNullOrEmpty(path))
            {
                if (String.IsNullOrEmpty(product.ImagePath))
                {
                    product.ImagePath = $"/Products/Images/{product.Name}.jpg";
                }
            }

            product.Price = Convert.ToDouble(Price_string);
            product.Weight = Convert.ToDouble(Weight_string);
            product.Volume = Convert.ToDouble(Volume_string);

            db.Products.Update(product);
            await db.SaveChangesAsync();

            return View("~/Views/Home/Index.cshtml");
        }


        private async Task<string> UploadImage(string imagePath)
        {
            string filePath = "";
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (file != null)
            {
                var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/Products/Images/", file.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream); //Upload file to /Products/Images/filename.ext
                }
                filePath = Path.Combine("/Products/Images/", file.FileName);
            }

            return filePath;
        }

    }

}
