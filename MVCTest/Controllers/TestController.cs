using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCTest.Models;

namespace MVCTest.Controllers
{
    public class TestController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        SiteDbContext db;
        public TestController(SiteDbContext context, IHostingEnvironment hostingEnvironment) : base(context)
        {
            db = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Run()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Run")]
        public async Task<IActionResult> Run_Post()
        {
            var file = HttpContext.Request.Form.Files.First();
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/Products/Images/", file.FileName); /*+ file.FileName*/;
            Console.WriteLine($"Copying file to path: {path}");
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream); //Upload file to /Products/Images/filename.ext
                Console.WriteLine($"Copying process finished");
            }
               
                

            return View();
        }
    }
}