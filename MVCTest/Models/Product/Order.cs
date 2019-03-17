using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.Product
{
    public class Order
    {
        public int Id { get; set; }

        //public int Quantity { get; set; }

        public string Promo { get; set; } //Promo id

        public bool IsSubmitted { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        //TODO: Add payment type
        //TODO: Add delivery options
    }
}
