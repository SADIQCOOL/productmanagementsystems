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
    public class UsersController : Controller
    {
        private sd2Entities db = new sd2Entities();

        //Client
        [HttpGet]
        public ActionResult SignupClient()
        {


            return View();
        }
        [HttpPost]
        public ActionResult SignupClient(Client user)
        {
            if (ModelState.IsValid)
            {
                var searchdata = db.Clients.Where(x => x.Mail == user.Mail || x.CContactNo == user.CContactNo).SingleOrDefault();
                if (searchdata == null)
                {
                    db.Clients.Add(user);
                    db.SaveChanges();
                    return Content("Sign Up Successfull");

                }
                else {

                    return Content("Already Registered!!!");
                }
              

            }

            return View();
        }


        [HttpGet]
        public ActionResult LoginClient()
        {


            return View();
        }
        [HttpPost]
        public ActionResult LoginClient(TempClientUser tempUser)
        {
            if (ModelState.IsValid)
            {
                var user = db.Clients.Where(u => u.Mail.Equals(tempUser.Mail) && u.Password.Equals(tempUser.Password) ).FirstOrDefault();
                if (user != null)
                {
                    Session["user_email"] = user.Mail;
                    Session["clientid"] = user.Clinetid;
                    Session["clientname"] = user.ClinetName;
                    //return Content("Login Successful as Client");
                    // return RedirectToAction("showoptclient");
                    // return RedirectToAction("homeind");
                    Client client = new Client();


                    return RedirectToAction("ind", "products");

                }
                else
                {

                    return Content("Login Failed");
                }

            }

            return View();
        }

        //client logout

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("ind", "products");


        }


        //Client check opt
        public ActionResult showoptclient() {
            string email = Convert.ToString(Session["user_email"]);
            var user = db.Clients.Where(u => u.Mail.Equals(email)).FirstOrDefault();

            return View(user);
        }
        public ActionResult homeind()
        {


            return View(db.products.ToList());
        }


        //Driver

        [HttpGet]
        public ActionResult SignupDriver()
        {


            return View();
        }
        [HttpPost]
        public ActionResult SignupDriver(Driver user)
        {
            if (ModelState.IsValid)
            {
                var searchdata = db.Drivers.Where(x => x.DContactNo == user.DContactNo ).SingleOrDefault();
                if (searchdata == null)
                {
                    db.Drivers.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("driverind", "Users");
                    //return Content("Sign Up Successfull");

                }
                else
                {

                    return Content("Already Registered!!!");
                }
           

            }

            return View();
        }

        [HttpGet]
        public ActionResult LoginDriver()
        {


            return View();
        }
        [HttpPost]
        public ActionResult LoginDriver(TempDriverUser tempUser)
        {
            if (ModelState.IsValid)
            {
                var user = db.Drivers.Where(u => u.DContactNo.Equals(tempUser.DContactNo) && u.Password.Equals(tempUser.Password) ).FirstOrDefault();
                if (user != null)
                {
                    Session["drivercontact"] = user.DContactNo;
                    Session["driverid"] = user.DriverID;
                    Session["pickupno"] = user.PickupNo;
                    Session["drivername"] = user.DriverName;
                    // return Content("Login Successful as Driver");
                    return RedirectToAction("Index", "buys");
                }
                else
                {

                    return Content("Login Failed");
                }

            }

            return View();
        }

        public ActionResult driverind() {

            return View(db.buys.ToList());
        
        }

        //Admin


        [HttpGet]
        public ActionResult Loginadmin()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Loginadmin(TempAdminUser tempUser)
        {
            if (ModelState.IsValid)
            {
                var user = db.Adminlogs.Where(u => u.Admin_name.Equals(tempUser.Admin_name) && u.Admin_password.Equals(tempUser.Admin_password)).FirstOrDefault();
                if (user != null)
                {
                    Session["adminname"] = user.Admin_name;

                    //return Content("Login Successful as Admin");
                    // return RedirectToAction("addprdss","products");
                    return RedirectToAction("selectoption");
                }
                else
                {

                    return Content("Login Failed");
                }

            }

            return View();
        }

        //select register user


        public ActionResult selectuser() {

            return View();
        }

        //select login user


        public ActionResult selectuserlogin()
        {

            return View();
        }

        // admin page

        public ActionResult selectoption()
        {

            return View();
        }

        // admin add prd


        [HttpGet]
        public ActionResult addprd() {

            return View();
        }

        [HttpPost]
        public ActionResult addprd(product ad)
        {
            if (ModelState.IsValid)
            {
                var searchdata = db.products.Where(x => x.Description == ad.Description).SingleOrDefault();
                if (searchdata == null)
                {
                    db.products.Add(ad);
                    db.SaveChanges();
                    return Content("Product Added");

                }
                else {
             
                    return Content("Already Added");

                }

     

            }


            return View();
        }



        /*
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Clinetid,ClinetName,Password,CAddress,Mail,CContactNo")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Clinetid,ClinetName,Password,CAddress,Mail,CContactNo")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
        */
    }
}
