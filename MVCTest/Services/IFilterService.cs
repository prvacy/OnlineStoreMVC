using MVCTest.Models.Filters;
using MVCTest.Models.Product;
using MVCTest.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Services
{
    public interface IFilterService
    {
        Task<IQueryable<Product>> Filter(IndexFilter filter);
    }
}
