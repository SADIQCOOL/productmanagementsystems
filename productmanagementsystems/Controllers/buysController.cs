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
    public class buysController : Controller
    {
        private sd2Entities db = new sd2Entities();


        // GET: buys

        public ActionResult Index()
        {
            var buys = db.buys.Include(b => b.Client).Include(b => b.product);
            return View(buys.ToList());
        }

       
            // GET: buys/Details/5
            public ActionResult Details(int? id)
            {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy buy = db.buys.Find(id);
            if (buy == null)
            {
                return HttpNotFound();
            }
            delivered delivered = new delivered() {
            p_id=id
            
            };
            db.delivereds.Add(delivered);
            db.SaveChanges();


            return View(buy);
        }

        // GET: buys/Create
        public ActionResult Create()
        {
            ViewBag.Clinetid = new SelectList(db.Clients, "Clinetid", "ClinetName");
            ViewBag.Productid = new SelectList(db.products, "Productid", "Description");
            return View();
        }

        // POST: buys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Purchase_Id,Qtn,Productid,Clinetid")] buy buy)
        {
            if (ModelState.IsValid)
            {
                db.buys.Add(buy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Clinetid = new SelectList(db.Clients, "Clinetid", "ClinetName", buy.Clinetid);
            ViewBag.Productid = new SelectList(db.products, "Productid", "Description", buy.Productid);
            return View(buy);
        }

        // GET: buys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy buy = db.buys.Find(id);
            if (buy == null)
            {
                return HttpNotFound();
            }
            ViewBag.Clinetid = new SelectList(db.Clients, "Clinetid", "ClinetName", buy.Clinetid);
            ViewBag.Productid = new SelectList(db.products, "Productid", "Description", buy.Productid);
            return View(buy);
        }

        // POST: buys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Purchase_Id,Qtn,Productid,Clinetid")] buy buy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Clinetid = new SelectList(db.Clients, "Clinetid", "ClinetName", buy.Clinetid);
            ViewBag.Productid = new SelectList(db.products, "Productid", "Description", buy.Productid);
            return View(buy);
        }

        // GET: buys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buy buy = db.buys.Find(id);
            if (buy == null)
            {
                return HttpNotFound();
            }
            return View(buy);
        }

        // POST: buys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string str = Session["driverid"].ToString();
            buy buy = db.buys.Find(id);
            db.buys.Remove(buy);
           
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*
        // GET: deliver/confirm/5
        public ActionResult Confirm(int? id,int? uid,int? pid,int? qtn) {
        
            delivered deliver = db.delivereds.Find(id);
            if (deliver == null)
            {
                return HttpNotFound();
            }
            return View(deliver);

            return View();
        }
        */
        // POST: deliver/confirm/5
        [HttpPost, ActionName("Confirm")]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(int id,int uid, int pid, int qtn)
        {
            string str = Session["driverid"].ToString();
            int did = Int32.Parse(str);
           // delivered deliver = db.delivereds.Find(id);
            

            delivered delivers = new delivered()
                {
                  DriverID = did,
                  p_id=pid,
                  u_id=uid,
                  o_quantity=qtn

                 



                };

            

            
            db.delivereds.Add(delivers);

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
