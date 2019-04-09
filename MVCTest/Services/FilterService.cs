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


        public int PagesCount { get; private set; }
        public int ItemsOnPage { get; set; } = 20;

        public FilterService(SiteDbContext dbContext)
        {
            db = dbContext;
        }

        
        public async Task<List<Product>> Filter(IndexFilter filter)
        {
            #region private_members
            IQueryable<Product> products = db.Products.Select(p => p);

            #endregion

            products = await FilterByCategoryAsync(products, filter.SubCategoryId);
            products = await FilterByPriceAsync(products, filter.MinPrice, filter.MaxPrice);
            products = await FilterByPageAsync(products, filter.Page);

            return await products.ToListAsync();
        }

        private async Task<IQueryable<Product>> FilterByCategoryAsync(IQueryable<Product> products, int? subcId)
        {
            if (subcId.HasValue)
            {
                products = products.Where(p => p.SubCategoryId == subcId);
            }

            return products;
        }

        private async Task<IQueryable<Product>> FilterByPriceAsync(IQueryable<Product> products, double? minPrice, double? maxPrice)
        {
            var priceRange = new
            {
                min = minPrice ?? 0,
                max = maxPrice ?? 100000000
            };

            if (!(priceRange.min == 0 && priceRange.max == 100000000))
            {
                products = products.Where(p => p.Price >= priceRange.min && p.Price <= priceRange.max);
            }

            return products;
        }

        private async Task<IQueryable<Product>> FilterByPageAsync(IQueryable<Product> products, int? page)
        {
            async Task getPagesCount()
            {
                var prodCount = await products.CountAsync();
                var pagesCount = prodCount / ItemsOnPage;
                if (prodCount % ItemsOnPage == 0)
                {
                    PagesCount = pagesCount;
                }
                else
                {
                    PagesCount = ++pagesCount;
                }
            }
            await getPagesCount();

            page = page ?? 0;

            var pageStart = page.Value * ItemsOnPage;
            products = products.Skip(pageStart).Take(ItemsOnPage);


            return products;
        }

    }
}
