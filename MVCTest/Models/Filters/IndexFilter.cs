using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.Filters
{
    public class IndexFilter
    {
        public string SubCategory { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }
    }
}
