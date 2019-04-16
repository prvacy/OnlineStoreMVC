using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.ViewModels
{
    public class EditorProductVM
    {
        public ICollection<Product.Category> Categories { get; set; }

        public Product.Product Product { get; set; }
    }
}
