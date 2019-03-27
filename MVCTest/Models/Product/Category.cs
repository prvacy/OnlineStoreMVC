using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.Product
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        #region Ref

        public ICollection<SubCategory> SubCategories { get; set; } // Category 1-m SybCategory

        #endregion
    }
}
