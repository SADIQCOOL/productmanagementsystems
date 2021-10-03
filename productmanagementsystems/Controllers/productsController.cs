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
    public class productsController : Controller
    {
        private sd2Entities db = new sd2Entities();
        public ActionResult ind(string searchBy,string search)
        {
            if (searchBy == "Category")
            {
                return View(db.products.Where(x=>x.category.StartsWith(search) || search == null).ToList());

            }
            else {
                return View(db.products.Where(x => x.Description.StartsWith(search) || search == null).ToList());
            }
            
           // return View(db.products.ToList());
         
        }
        public ActionResult cmnt(FormCollection frc) {
            string str = Session["clientid"].ToString();
            int did = Int32.Parse(str);
            comment comment = new comment() {
                commenttxt = frc["commenttxt"],
                u_id = did,
                p_id=Convert.ToInt32(frc["pid"])

            };
            db.comments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("ind", "products");
        }


        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ind");
        }


        // GET: products/Details/5
        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            List<product> sim_products = db.products.Where(p => p.category == product.category && p.category!=product.category).Take(10).ToList<product>();

            ViewBag.SimilarProducts = sim_products;
          


            return View(product);
        }

     




        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            // List<product> sim_products = db.products.Where(p => p.category == product.category && p.category != product.category).Take(10).ToList<product>();
            List<product> sim_products = new List<product>()
            {
                new product{
                Description="Sadiq"
              
                
                }


            };
            List<product> sim_productss = db.products.Take(10).ToList<product>();
            List<product> sim_productsss = db.products.Where(p => p.category == product.category && p.Description != product.Description).Take(10).ToList<product>();

           ViewBag.SimilarProducts = sim_productsss;
            return View(product);
        }

        // buy

        // GET: products/Delete/5
        public ActionResult buy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("buy")]
        [ValidateAntiForgeryToken]
        public ActionResult buy(int id)
        {
            product product = db.products.Find(id);
            db.products.Add(product);
            db.SaveChanges();
            return RedirectToAction("ind");
        }



        /*
        // add products for admin
        [HttpGet]
        public ActionResult addprdss()
        {

            return View();
        }
        [HttpPost]
        public ActionResult addprdss(product ad)
        {
            if (ModelState.IsValid)
            {

                db.products.Add(ad);
                db.SaveChanges();
                return Content("Product Added");

            }


            return View();
        }


        public ViewResult showprd() {

            var product = db.products.FirstOrDefault<product>();

            return View(product);
        }
        */



        /*

        
        // GET: products
        public ActionResult Index()
        {
            return View(db.products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Productid,Description,Price,Quantity,images,category")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Productid,Description,Price,Quantity,images,category")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
        */
    }
}
