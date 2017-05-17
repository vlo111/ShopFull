using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopFull.Models;
using System.IO;

namespace ShopFull.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ShopDBEntities db = new ShopDBEntities();

        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,TopBrands,Description,Image,Count,Price,CategoryId")]  Product product, HttpPostedFileBase Image)
        {

            if (ModelState.IsValid)
            {
                if (product.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(Image.FileName);
                    fileName += extension;

                    List<string> extensions = new List<string>() { ".png", ".jpg", };
                    if (extensions.Contains(extension))
                    {
                        Image.SaveAs(Server.MapPath("/Content/images/" + fileName));
                        product.Image = "/Content/images/" + fileName;
                    }
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Title,TopBrands,Description, Image, Count,Price,CategoryId")] Product product, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (product.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(Image.FileName);
                    fileName += extension;

                    List<string> extensions = new List<string>() { ".png", ".jpg", };
                    if (extensions.Contains(extension))
                    {
                        Image.SaveAs(Server.MapPath("/Content/images/" + fileName));
                        product.Image = "/Content/images/" + fileName;
                    }
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);

                if (db.Carts.Find(product.Id) != null)
                {
                    var cart = db.Carts.Find(product.Id);
                    cart.ProductName = product.TopBrands;
                    cart.Price = product.Price;
                    cart.Image = product.Image;
                    db.SaveChanges();
                }

            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
