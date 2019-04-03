using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCTest.Models;
using MVCTest.Models.Filters;
using MVCTest.Models.Product;
using MVCTest.Models.ViewModels;

namespace MVCTest.Services
{
    public class FilterService : IFilterService
    {
        private readonly SiteDbContext db;
        public FilterService(SiteDbContext context)
        {
            db = context;
        }


        public async Task<IQueryable<Product>> Filter(IndexFilter filter)
        {
            IQueryable<Product> filterCat = null;

            IQueryable<Product> filterPrice = null;

            if (filter.SubCategory != null)
            {
                var subc = await db.SubCategories.SingleOrDefaultAsync(s => s.Name == filter.SubCategory);

                filterCat = db.Products
                    .Where(p => subc == p.SubCategory);

                //selProducts.AddRange(queryresult);
            }


            var priceRange = new
            {
                min = filter.MinPrice.HasValue ? filter.MinPrice : 0,
                max = filter.MaxPrice.HasValue ? filter.MaxPrice : 100000000  //min:0, max: 100 000 000
            };

            if (!(priceRange.min == 0 && priceRange.max == 100000000))
            {
                filterPrice = db.Products
                    .Where(p => (p.Price >= priceRange.min) && (p.Price <= priceRange.max));

            }

            var selProducts = filterCat.Concat(filterPrice);

            return selProducts;
        }
    }
}
