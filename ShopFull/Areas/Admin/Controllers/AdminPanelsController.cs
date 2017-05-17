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
    public class AdminPanelsController : Controller
    {
        private ShopDBEntities db = new ShopDBEntities();

        // GET: Admin/AdminPanels
        public ActionResult Index()
        {
            return View(db.AdminPanels.ToList());
        }

        // GET: Admin/AdminPanels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminPanel adminPanel = db.AdminPanels.Find(id);
            if (adminPanel == null)
            {
                return HttpNotFound();
            }
            return View(adminPanel);
        }

        // GET: Admin/AdminPanels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminPanels/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdminName,Mail,Password")] AdminPanel adminPanel)
        {
            if (ModelState.IsValid)
            {
                db.AdminPanels.Add(adminPanel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminPanel);
        }

        // GET: Admin/AdminPanels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminPanel adminPanel = db.AdminPanels.Find(id);
            if (adminPanel == null)
            {
                return HttpNotFound();
            }
            return View(adminPanel);
        }

        // POST: Admin/AdminPanels/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdminName,Mail,Password")] AdminPanel adminPanel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminPanel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminPanel);
        }

        // GET: Admin/AdminPanels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminPanel adminPanel = db.AdminPanels.Find(id);
            if (adminPanel == null)
            {
                return HttpNotFound();
            }
            return View(adminPanel);
        }

        // POST: Admin/AdminPanels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminPanel adminPanel = db.AdminPanels.Find(id);
            db.AdminPanels.Remove(adminPanel);
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
