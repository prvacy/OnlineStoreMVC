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
            var cat = await db.Categories
                .Include(c => c.SubCategories)
                .ToListAsync();
            return View("~/Views/Editor/Product.cshtml", cat);
        }




        [HttpPost]
        public async Task<ActionResult> Index(Product product, string Price_string)
        {
            var file = HttpContext.Request.Form.Files.First();
            if (file != null)
            {
                var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/Products/Images/", file.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream); //Upload file to /Products/Images/filename.ext
                }
                product.ImagePath = Path.Combine("/Products/Images/", file.FileName);
            }
            else
            {
                if (String.IsNullOrEmpty(product.ImagePath))
                {
                    product.ImagePath = $"/Products/Images/{product.Name}.jpg"; 
                }
            }

            //product.SubCategory = await db.SubCategories.SingleOrDefaultAsync(s => s.Name == subCategory);

            product.Price = Convert.ToDouble(Price_string);

            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();

            var cat = await db.Categories
                .Include(c => c.SubCategories)
                .ToListAsync();

            return View("~/Views/Editor/Product.cshtml", cat);
        }


    }
}