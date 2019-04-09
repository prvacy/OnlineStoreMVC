using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MVCTest.Models.Product;

namespace MVCTest.Models.ViewModels
{
    public class IndexVM
    {
        public List<Category> Categories { get; set; }

        public List<SubCategory> SubCategories { get; set; }

        public List<Product.Product> Products { get; set; }

        public int? PagesCount { get; set; }
    }
}
