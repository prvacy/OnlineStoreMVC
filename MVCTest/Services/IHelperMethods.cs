using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Services
{
    public interface IHelperMethods
    {
        Task<int> GetPagesCount();
    }
}
