using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart
{
    public class ShoppingCart
    {
        public string bookId { get; set; }

        public double Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double Total { get; set; }

        public string ImagePath { get; set; }

        public string ItemName { get; set; }
    }
}