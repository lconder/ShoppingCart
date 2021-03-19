using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart
{
    public class Shopping
    {
        public int bookId { get; set; }

        public string title { get; set; }

        public string author { get; set; }

        public double price { get; set; }

        public string imagePath { get; set; }

        public string description { get; set; }

        public string category { get; set; }

        public string isbn { get; set; }
    }
}