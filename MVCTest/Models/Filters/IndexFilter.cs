using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.Filters
{
    public enum PriceSort
    {
        LowToHigh,
        HighToLow
    }
    public class IndexFilter
    {
        public int? SubCategoryId { get; set; }

        public double? MinPrice { get; set; }

        public double? MaxPrice { get; set; }

        public int? Page { get; set; }

        public string SearchQuery { get; set; }

        public PriceSort PriceSort { get; set; }
    }
    

}
