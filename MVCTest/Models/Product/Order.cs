using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.Product
{
    public class Order
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }  //ref to product
        public Product Product { get; set; }//

        public int Quantity { get; set; }

        public string Promo { get; set; } //Promo id



    }
}
