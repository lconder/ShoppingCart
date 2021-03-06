﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class ShoppingController : Controller
    {
        private Model1Container db;
        private List<ShoppingCart> listOfShoppingCartModel;

        public ShoppingController()
        {
            db = new Model1Container();
            listOfShoppingCartModel = new List<ShoppingCart>();
        }

        public ActionResult Index()
        {
            IEnumerable<Shopping> list = (from objBook in db.Books
                                          where objBook.stock>0
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

        [HttpPost]
        public JsonResult Index(string bookId) 
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            Book objBook = db.Books.Single(model => model.Id.ToString() == bookId && model.stock>0);

            if (objBook == null) 
            {
                return Json(new { Success = false, Message = "No existen suficientes artículos para ser agregados." }, JsonRequestBehavior.AllowGet);
            }

            if (Session["CartCounter"] != null) 
            {
                listOfShoppingCartModel = Session["CartItem"] as List<ShoppingCart>;
            }
            
            if (listOfShoppingCartModel.Any(model => model.bookId == bookId))
            {
                shoppingCart = listOfShoppingCartModel.Single(model => model.bookId == bookId);
                shoppingCart.Quantity = shoppingCart.Quantity + 1;
                shoppingCart.Total = shoppingCart.Quantity * shoppingCart.UnitPrice;

            }
            else
            {
                shoppingCart.bookId = bookId;
                shoppingCart.ImagePath = objBook.image;
                shoppingCart.ItemName = objBook.title;
                shoppingCart.Quantity = 1;
                shoppingCart.Total = objBook.price;
                shoppingCart.UnitPrice = objBook.price;
                listOfShoppingCartModel.Add(shoppingCart);

            }

            Session["CartCounter"] = listOfShoppingCartModel.Count;
            Session["CartItem"] = listOfShoppingCartModel;

            return Json(new { Success = true, Counter = listOfShoppingCartModel.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShoppingCart() {

            if (Session["CartCounter"] != null)
            {
                listOfShoppingCartModel = Session["CartItem"] as List<ShoppingCart>;
            }

            return View(listOfShoppingCartModel);
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
    
            if (Session["CartCounter"] != null)
            {
                listOfShoppingCartModel = Session["CartItem"] as List<ShoppingCart>;
            }

            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = String.Format("{0:ddmmyyyyHHmmss}", DateTime.Now)
            };
            db.Orders.Add(order);
            db.SaveChanges();
            int orderId = order.Id;

            foreach(var item in listOfShoppingCartModel)
            {
                Book objBook = db.Books.First(b => b.Id.ToString() == item.bookId);
                if (objBook != null)
                {
                    objBook.stock = objBook.stock - (int)item.Quantity;
                }

                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderId = orderId;
                orderDetail.Total = item.Total;
                orderDetail.BookId = Int32.Parse(item.bookId);
                orderDetail.Quantity = item.Quantity;
                orderDetail.UnitPrice = item.UnitPrice;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }

            Session["CartCounter"] = null;
            Session["CartItem"] = null;
            return RedirectToAction("Index");
        }
    }
    
}