using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vegan_Market.Models;

namespace Vegan_Market.Controllers
{
    public class WishListController : Controller
    {
       
        Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        public Listwish Bringthecart()
        {

            var ourcart = (Listwish)Session["ourcart"];
            if (ourcart == null)
            {
                ourcart = new Listwish();
                Session["ourcart"] = ourcart;
            }
            return ourcart;
        }


   
        public ActionResult Index()
        {
            return View(Bringthecart());
        }


        public ActionResult Add_wish(int? product_id)
        {

          
            var product = db.Product.FirstOrDefault(x => x.product_id == product_id);
            if (product != null)
            {
                Bringthecart().Add_wishcart(product);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index");
        }


        public ActionResult remove_cart(int? product_id)
        {
            var product = db.Product.FirstOrDefault(x => x.product_id == product_id);
            if (product != null)
            {
                Bringthecart().Remove_wishcart(product);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("index");
        }



        public ActionResult clear_Cart(int? product_id)
        {
            var product = db.Product.FirstOrDefault(x => x.product_id == product_id);
            if (product != null)
            {
                Bringthecart().Clear_cart();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("index");
        }
    }
}