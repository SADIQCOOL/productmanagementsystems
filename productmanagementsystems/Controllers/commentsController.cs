﻿using System;
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
    public class commentsController : Controller
    {
        private sd2Entities db = new sd2Entities();

        // GET: comments
        public ActionResult Index(int? id)
        {
            var searchid = db.comments.Where(x => x.p_id == id).Take(10).ToList<comment>();

            var comments = db.comments.Include(c => c.Client).Include(c => c.product);

            return View(searchid);
        }

        // GET: comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: comments/Create
        public ActionResult Create()
        {
            ViewBag.u_id = new SelectList(db.Clients, "Clinetid", "ClinetName");
            ViewBag.p_id = new SelectList(db.products, "Productid", "Description");
            return View();
        }

        // POST: comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "commentid,u_id,p_id,commenttxt")] comment comment)
        {
            if (ModelState.IsValid)
            {
                db.comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.u_id = new SelectList(db.Clients, "Clinetid", "ClinetName", comment.u_id);
            ViewBag.p_id = new SelectList(db.products, "Productid", "Description", comment.p_id);
            return View(comment);
        }

        // GET: comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.u_id = new SelectList(db.Clients, "Clinetid", "ClinetName", comment.u_id);
            ViewBag.p_id = new SelectList(db.products, "Productid", "Description", comment.p_id);
            return View(comment);
        }

        // POST: comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentid,u_id,p_id,commenttxt")] comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.u_id = new SelectList(db.Clients, "Clinetid", "ClinetName", comment.u_id);
            ViewBag.p_id = new SelectList(db.products, "Productid", "Description", comment.p_id);
            return View(comment);
        }

        // GET: comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comment comment = db.comments.Find(id);
            db.comments.Remove(comment);
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
