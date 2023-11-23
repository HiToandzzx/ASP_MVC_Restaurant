using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_63135741.Models;

namespace Project_63135741.Controllers
{
    public class Foods_63135741Controller : Controller
    {
        private Project_63135741Entities db = new Project_63135741Entities();

        // GET: Foods_63135741
        public ActionResult Index(string FoodsName = "", string KindID = "")
        {
            //var foods = db.Foods.Include(f => f.KindOfMenu);
            ViewBag.FoodsName = FoodsName;
            ViewBag.KindID = new SelectList(db.KindOfMenus, "KindID", "NameOfKind");
            var foods = db.Foods.SqlQuery("Find_Menu'" + FoodsName + "',N'" + KindID + "'");
            if (foods.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(foods.ToList());
        }

        // GET: Foods_63135741/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods_63135741/Create
        public ActionResult Create()
        {
            ViewBag.KindID = new SelectList(db.KindOfMenus, "KindID", "NameOfKind");
            return View();
        }

        // POST: Foods_63135741/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodsID,PicFood,FoodsName,Descrip,Price,KindID")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Foods.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KindID = new SelectList(db.KindOfMenus, "KindID", "NameOfKind", food.KindID);
            return View(food);
        }

        // GET: Foods_63135741/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.KindID = new SelectList(db.KindOfMenus, "KindID", "NameOfKind", food.KindID);
            return View(food);
        }

        // POST: Foods_63135741/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodsID,PicFood,FoodsName,Descrip,Price,KindID")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KindID = new SelectList(db.KindOfMenus, "KindID", "NameOfKind", food.KindID);
            return View(food);
        }

        // GET: Foods_63135741/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods_63135741/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Cart()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
        }

        public ActionResult AddToCart(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            // Assuming you have a session-based cart stored as a list of CartItems
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            // Check if the item is already in the cart
            CartItem existingItem = cart.FirstOrDefault(item => item.FoodID == id);

            if (existingItem != null)
            {
                // If the item is already in the cart, increase its quantity
                existingItem.Quantity++;
            }
            else
            {
                // If not, add it to the cart
                cart.Add(new CartItem
                {
                    FoodID = food.FoodsID,
                    FoodsName = food.FoodsName,
                    Quantity = 1,
                    Price = (decimal)food.Price
                });
            }

            // Store the updated cart back in the session
            Session["Cart"] = cart;

            return RedirectToAction("Index");
        }

        // GET: Foods_63135741/EditCartItem/5
        public ActionResult EditCartItem(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            CartItem itemToEdit = cart.FirstOrDefault(item => item.FoodID == id);

            if (itemToEdit == null)
            {
                return HttpNotFound();
            }

            return View(itemToEdit);
        }

        // POST: Foods_63135741/EditCartItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCartItem(CartItem editedItem)
        {
            if (ModelState.IsValid)
            {
                List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
                CartItem itemToEdit = cart.FirstOrDefault(item => item.FoodID == editedItem.FoodID);

                if (itemToEdit != null)
                {
                    itemToEdit.Quantity = editedItem.Quantity;
                    // Add additional logic if needed (e.g., price changes, etc.)

                    // Store the updated cart back in the session
                    Session["Cart"] = cart;

                    return RedirectToAction("Cart");
                }
            }

            return View(editedItem);
        }


        // GET: Foods_63135741/DeleteCartItem/5
        public ActionResult DeleteCartItem(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            CartItem itemToDelete = cart.FirstOrDefault(item => item.FoodID == id);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }

            return View(itemToDelete);
        }

        // POST: Foods_63135741/DeleteCartItem/5
        [HttpPost, ActionName("DeleteCartItem")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirme(string id)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            CartItem itemToDelete = cart.FirstOrDefault(item => item.FoodID == id);

            if (itemToDelete != null)
            {
                // Remove the item from the cart
                cart.Remove(itemToDelete);

                // Store the updated cart back in the session
                Session["Cart"] = cart;

                return RedirectToAction("Cart");
            }

            return HttpNotFound();
        }

        
    }
}
