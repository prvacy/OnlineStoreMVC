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
        public int Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
