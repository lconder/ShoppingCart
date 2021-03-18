using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart
{
    public partial class Book
    {
        public IEnumerable<SelectListItem> categorySelectedList { get; set; }

        public HttpPostedFileBase ImagePath { get; set; }

        public int Category_Id { get; set; }
    }
}