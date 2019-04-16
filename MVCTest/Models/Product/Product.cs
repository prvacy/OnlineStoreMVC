using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.Product
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public double? Weight { get; set; }

        public double? Volume { get; set; }


        #region References

        public ICollection<OrderItem> OrderItems { get; set; } //Product 1-m OrderItems

        public ICollection<WeightOption> WeightOptions { get; set; }

        public ICollection<VolumeOption> VolumeOptions { get; set; }

        public SubCategory SubCategory { get; set; } // 1 to 1 |Product 1-m SubCategory

        public int? SubCategoryId { get; set; }

        #endregion
    }
}
