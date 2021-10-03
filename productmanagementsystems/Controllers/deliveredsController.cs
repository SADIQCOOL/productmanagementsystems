using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using productmanagementsystems.Models;

namespace productmanagementsystems.Controllers
{
    public class deliveredsController : Controller
    {
        private sd2Entities db = new sd2Entities();

        // GET: delivereds
        public ActionResult Index()
        {
            var delivereds = db.delivereds.Include(d => d.Client).Include(d => d.Driver).Include(d => d.product);
            return View(delivereds.ToList());
        }



      
        // GET: delivereds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            delivered delivered = db.delivereds.Find(id);
            if (delivered == null)
            {
                return HttpNotFound();
            }
            return View(delivered);
        }

        // GET: delivereds/Create
        public ActionResult Create()
        {
            ViewBag.u_id = new SelectList(db.Clients, "Clinetid", "ClinetName");
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverName");
            ViewBag.p_id = new SelectList(db.products, "Productid", "Description");
            return View();
        }

        // POST: delivereds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "delever_Id,DriverID,p_id,u_id,o_quantity")] delivered delivered)
        {
            if (ModelState.IsValid)
            {
                db.delivereds.Add(delivered);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.u_id = new SelectList(db.Clients, "Clinetid", "ClinetName", delivered.u_id);
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverName", delivered.DriverID);
            ViewBag.p_id = new SelectList(db.products, "Productid", "Description", delivered.p_id);
            return View(delivered);
        }

        // GET: delivereds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            delivered delivered = db.delivereds.Find(id);
            if (delivered == null)
            {
                return HttpNotFound();
            }
            ViewBag.u_id = new SelectList(db.Clients, "Clinetid", "ClinetName", delivered.u_id);
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverName", delivered.DriverID);
            ViewBag.p_id = new SelectList(db.products, "Productid", "Description", delivered.p_id);
            return View(delivered);
        }

        // POST: delivereds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "delever_Id,DriverID,p_id,u_id,o_quantity")] delivered delivered)
        {
            if (ModelState.IsValid)
            {
                db.Entry(delivered).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.u_id = new SelectList(db.Clients, "Clinetid", "ClinetName", delivered.u_id);
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverName", delivered.DriverID);
            ViewBag.p_id = new SelectList(db.products, "Productid", "Description", delivered.p_id);
            return View(delivered);
        }

        // GET: delivereds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            delivered delivered = db.delivereds.Find(id);
            if (delivered == null)
            {
                return HttpNotFound();
            }
            return View(delivered);
        }

        // POST: delivereds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            delivered delivered = db.delivereds.Find(id);
            db.delivereds.Remove(delivered);
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
