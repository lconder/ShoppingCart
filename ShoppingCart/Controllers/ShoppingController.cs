﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class ShoppingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}