using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class ShoppingController : Controller
    {
        private Model1Container db;

        public ShoppingController()
        {
            db = new Model1Container();
        }

        public ActionResult Index()
        {
            IEnumerable<Shopping> list = (from objBook in db.Books
                                           join objCat in db.Categories 
                                           on objBook.Category.Id equals objCat.Id
                                          select new Shopping
                                          {
                                              bookId = objBook.Id,
                                              title = objBook.title,
                                              author = objBook.author,
                                              description = objBook.description,
                                              price = objBook.price,
                                              imagePath = objBook.image,
                                              category = objCat.name,
                                              isbn = objBook.isbn
                                              
                                          }).ToList();
            return View(list);
        }
    }
}