using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productmanagementsystems.Models
{
    public class Cart
    {
        public product Product { get; set; }
        public int Quantity { get; set; }
        public Cart(product product, int quantity) {

            Product = product;
            Quantity=quantity;
        
        }
    }
}