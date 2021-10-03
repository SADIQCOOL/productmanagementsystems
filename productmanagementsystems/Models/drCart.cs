using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productmanagementsystems.Models
{
    public class drCart
    {
        public buy Buy { get; set; }
        public drCart(buy buy) {

            Buy = buy;
        
        }



    }
}