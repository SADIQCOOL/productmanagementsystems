using productmanagementsystems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace productmanagementsystems.Controllers
{
    public class ShoppingCartController : Controller
    {
         sd2Entities db = new sd2Entities();
        private string strCart = "Cart";
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<Cart> lsCart = new List<Cart> {

                new Cart(db.products.Find(id),1)
                };
                Session[strCart] = lsCart;
            }
            else {

                List<Cart> lsCart = (List<Cart>)Session[strCart];
                int check = IsExistingCheck(id);
                if (check == -1)
                {
                    lsCart.Add(new Cart(db.products.Find(id), 1));
                }
                else
                    lsCart[check].Quantity++;
                
                Session[strCart] = lsCart;
                //lsCart.Add(new Cart(db.products.Find(id),1));



            }

         //   return View("Index");
            return RedirectToAction("ind", "products");
        }

        private int IsExistingCheck(int? id) {

            List<Cart> lsCart = (List<Cart>)Session[strCart];
            for (int i = 0; i < lsCart.Count; i++) {
                if (lsCart[i].Product.Productid == id) return i;
            
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
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            lsCart.RemoveAt(check);
            return View("Index");
        }

        public ActionResult UpdateCart(FormCollection frc)
        {
            string[] quantities = frc.GetValues("quantity");
        
            List<Cart> lstCart = (List<Cart>)Session[strCart];
            for(int i=0;i<lstCart.Count; i++)
            {
                lstCart[i].Quantity = Convert.ToInt32(quantities[i]);
            }
            Session[strCart] = lstCart;
          
            return View("Index");
        }

        public ActionResult CheckOut()
        {
           


            return View("CheckOut");
        }
        public ActionResult ProcessOrder(FormCollection frc)
        {
            List<Cart> lstCart = (List<Cart>)Session[strCart];
            Payment order = new Payment()
            {
                PaymentSystem = frc["payName"],
               
                PaidTime = DateTime.Now

            };
           db.Payments.Add(order);
            db.SaveChanges();


            string str = Session["clientid"].ToString();
            int cid = Int32.Parse(str);

            foreach (Cart cart in lstCart) {
                
     
                buy orderdetail = new buy() { 

                Qtn=cart.Quantity.ToString(),
               // Productid=1,
                Productid=cart.Product.Productid,

                    Clinetid= cid



                };
                db.buys.Add(orderdetail);
                db.SaveChanges();

               

            }

            
            return View("OrderSuccess");
        }

    }
}