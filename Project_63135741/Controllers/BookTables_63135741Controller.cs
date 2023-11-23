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
    public class BookTables_63135741Controller : Controller
    {
        private Project_63135741Entities db = new Project_63135741Entities();

        // GET: BookTables_63135741
        string LayMaTable()
        {
            var maMax = db.BookTables.ToList().Select(n => n.BookTableID).Max();
            int maTable = int.Parse(maMax.Substring(2)) + 1;
            string TB = String.Concat("00", maTable.ToString());
            return "TB" + TB.Substring(maTable.ToString().Length - 1);
        }

        public ActionResult Index()
        {
            return View(db.BookTables.ToList());
        }

        // GET: BookTables_63135741/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTable bookTable = db.BookTables.Find(id);
            if (bookTable == null)
            {
                return HttpNotFound();
            }
            return View(bookTable);
        }

        // GET: BookTables_63135741/Create
        public ActionResult Create()
        {
            ViewBag.BookTableID = LayMaTable();
            return View();
        }

        // POST: BookTables_63135741/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookTableID,NameCus,PhoneCus,QuantityPP,TimeBook,DayBook,Note")] BookTable bookTable)
        {
            if (ModelState.IsValid)
            {
                bookTable.BookTableID = LayMaTable();

                try
                {
                    db.BookTables.Add(bookTable);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError("", $"Lỗi: {validationError.ErrorMessage}");
                        }
                    }
                }
            }

            return View(bookTable);
        }

        // GET: BookTables_63135741/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTable bookTable = db.BookTables.Find(id);
            if (bookTable == null)
            {
                return HttpNotFound();
            }
            return View(bookTable);
        }

        // POST: BookTables_63135741/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookTableID,NameCus,PhoneCus,QuantityPP,TimeBook,DayBook,Note")] BookTable bookTable)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(bookTable).State = EntityState.Modified; // Mark the entity as modified
                    db.SaveChanges(); // Save the changes

                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError("", $"Lỗi: {validationError.ErrorMessage}");
                        }
                    }
                    return View(bookTable); // Return to the view with validation errors
                }
            }
            return View(bookTable);
        }

        // GET: BookTables_63135741/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTable bookTable = db.BookTables.Find(id);
            if (bookTable == null)
            {
                return HttpNotFound();
            }
            return View(bookTable);
        }

        // POST: BookTables_63135741/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BookTable bookTable = db.BookTables.Find(id);
            db.BookTables.Remove(bookTable);
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
    }
}
