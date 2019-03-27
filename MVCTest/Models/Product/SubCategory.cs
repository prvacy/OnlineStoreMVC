using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.Product
{
    public class SubCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        #region ref

        public ICollection<Product> Products { get; set; } // SubCategory 1-m Product 

        public Category Category { get; set; } // Category 1-m SybCategory

        #endregion
    }
}
