using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopFull.Models;

namespace ShopFull.Areas.Admin.Controllers
{
    public class CartsController : Controller
    {
        private ShopDBEntities db = new ShopDBEntities();

        // GET: Admin/Carts
        public ActionResult Index()
        {
            var carts = db.Carts.Include(c => c.Account).Include(c => c.Product);
            return View(carts.ToList());
        }

        // GET: Admin/Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Admin/Carts/Create
        public ActionResult Create()
        {
            ViewBag.accauntId = new SelectList(db.Accounts, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Title");
            return View();
        }

        // POST: Admin/Carts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,accauntId,ProductName,Image,Price,Quantity,TotalPrice,ProductId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.accauntId = new SelectList(db.Accounts, "Id", "Name", cart.accauntId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Title", cart.ProductId);
            return View(cart);
        }

        // GET: Admin/Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.accauntId = new SelectList(db.Accounts, "Id", "Name", cart.accauntId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Title", cart.ProductId);
            return View(cart);
        }

        // POST: Admin/Carts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,accauntId,ProductName,Image,Price,Quantity,TotalPrice,ProductId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.accauntId = new SelectList(db.Accounts, "Id", "Name", cart.accauntId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Title", cart.ProductId);
            return View(cart);
        }

        // GET: Admin/Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Admin/Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
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
