using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using productmanagementsystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace productmanagementsystems.Controllers
{
    public class DriverCartController : Controller
    {
        sd2Entities db = new sd2Entities();
        private string drCart = "drCart";
        // GET: DriverCart
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult ConfirmOrderNow(int? id,int? pid,int? uid) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[drCart] == null)
            {
                List<drCart> dsCart = new List<drCart>
                {

                    new drCart(db.buys.Find(id))



                };
                Session[drCart] = dsCart;
            }
            else {
                List<drCart> dsCart = (List<drCart>)Session[drCart];
                int check = IsExistingCheck(id);
               if (check == -1)
                {

                   dsCart.Add(new drCart(db.buys.Find(id)));
                    //return Content("Al");


                }
                else
                    //return Content("Already added");
                    return View("Index");

                Session[drCart] = dsCart;
            }
            db.delivereds.Add(new delivered());
            return View("Index");

        }
        private int IsExistingCheck(int? id) {
            List<drCart> dsCart = (List<drCart>)Session[drCart];
            for (int i = 0; i < dsCart.Count; i++)
            {
                if (dsCart[i].Buy.Purchase_Id == id) {
                    
                    return i;
                }
               

            }

            return -1;
        }
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(id);
            List<drCart> dsCart = (List<drCart>)Session[drCart];
            dsCart.RemoveAt(check);
            return View("Index");
        }

        public ActionResult UpdateCart(FormCollection frc)
        {
            return View("Index");
        }



        public ActionResult ConfirmNow(int? id, int? uid, int? pid,int? qtn)
        {
            string str = Session["driverid"].ToString();
            int did = Int32.Parse(str);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            delivered delivered = new delivered()
            {
                p_id = pid,
                u_id = uid,

                DriverID = did,
                o_quantity = qtn
               




            };
            db.delivereds.Add(delivered);
            db.SaveChanges();


            return Content("Order Confirmed!!!");
          //  return RedirectToAction("Index");

            //return View("Index");

        }
        public ActionResult ProcessOrder(FormCollection frc) {
            string str = Session["driverid"].ToString();
            int did = Int32.Parse(str);
            List<drCart> dsCart = (List<drCart>)Session[drCart];

            foreach (drCart cart in dsCart)
            {


                delivered orderdetail = new delivered()
                {
                    DriverID=did,
                    p_id=cart.Buy.Productid,
                        u_id=cart.Buy.Clinetid,
                        o_quantity = Int32.Parse(cart.Buy.Qtn)




                };
                db.delivereds.Add(orderdetail);
                db.SaveChanges();



            }
           // return View("OrderSuccess");
            return RedirectToAction("Index", "buys");

        }
    }
    
}