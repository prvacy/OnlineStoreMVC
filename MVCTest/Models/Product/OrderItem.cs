using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.Product
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }  //ref to product
        public Product Product { get; set; }//

        public Order Order { get; set; }//ref to order
        public int OrderId { get; set; }
    }
}
