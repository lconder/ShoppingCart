using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class BookController : Controller
    {
        private Model1Container db;
        public BookController()
        {
            db = new Model1Container();
        }

        // GET: Book
        public ActionResult Index()
        {
            Book book = new Book();
            book.categorySelectedList = (from objCat in db.Categories
                select new SelectListItem() {
                    Text = objCat.name,
                    Value = objCat.Id.ToString(),
                    Selected = true
                });
            return View(book);
        }

        [HttpPost]
        public JsonResult Index(Book objBook)
        {

            string NewImage = Guid.NewGuid() + Path.GetExtension(objBook.ImagePath.FileName);
            objBook.ImagePath.SaveAs(Server.MapPath("~/Images/" + NewImage));

            Book newBook = new Book();
            newBook.image = "~/Images/" + NewImage;
            newBook.isbn = objBook.isbn;
            newBook.title = objBook.title;
            newBook.description = objBook.description;
            newBook.author = objBook.author;
            newBook.price = objBook.price;
            newBook.stock = objBook.stock;
            newBook.Category = db.Categories.First(i => i.Id==objBook.Category_Id);

            db.Books.Add(newBook);
            db.SaveChanges();

            return Json(new { Success = true, Message = "Libro creado satisfactoriamente"}, JsonRequestBehavior.AllowGet);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
