using MVCTest.Models.Filters;
using MVCTest.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Services
{
    public interface IFilterService
    {
        IndexVM Filter(IndexFilter filter);
    }
}
