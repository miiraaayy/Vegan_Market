using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vegan_Market.Models;
namespace Vegan_Market.Controllers
{
    public class ShoppingbagController : Controller
    {
        Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();
  
        public ActionResult AOrderList()
        {
            var orderlist = db.OrderT.ToList();
            return View(orderlist);
        }
        public Shoppingcart Bringthebag()
        {
                 
            var ourbag = (Shoppingcart)Session["ourbag"];
            if(ourbag == null)
            {
                ourbag = new Shoppingcart();
                Session["ourbag"] = ourbag;
            }
          
            return ourbag;
        }
        // GET: Shoppingcart
        public ActionResult Index(string order_message)
        {

            ViewBag.customer_no = Session["customers"];
          
            ViewBag.order_message = order_message;
            return View(Bringthebag());
        }

        
       

        public ActionResult Addto_cart(int? product_id,byte? product_number)
        {
           
            var product_num = product_number ?? 0;
            var product = db.Product.FirstOrDefault(x => x.product_id == product_id);

            if(product != null)
            {
                Bringthebag().Add_cart(product,product_num);
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
                Bringthebag().Remove_cart(product);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("index");
        }

        public ActionResult orderlist(int id)
        {
            var list_order = db.OrderT.Where(x => x.customer_no == id).ToList();
            return View(list_order);
        }


        public ActionResult Siparisteslim(string order_message)
        {
            ViewBag.order_message = order_message;
            return View();
        }

        public ActionResult clear_Cart(int? product_id)
        {
            var product = db.Product.FirstOrDefault(x => x.product_id == product_id);
            if (product != null)
            {
                Bringthebag().Clear_cart();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("index");
        }
        public ActionResult Take_order()
        {
            var ourbag = (Shoppingcart)Session["ourbag"];
            var new_order_num = db.OrderT.Max(x => (int?)x.order_num);
            if (new_order_num == null)
            {
                new_order_num = 1;
            }
            else
            {
                new_order_num = new_order_num + 1;
            }
            foreach (var register_bag in ourbag.Shoppingbag)
            {
                OrderT bag_register = new OrderT
                {
                    order_num = (int)new_order_num,

                    product_no = register_bag.product.product_id,
                    customer_no = ((Customer)Session["customer"]).customer_id,                                 
                    product_number = register_bag.product_number,
                    order_date = DateTime.Now,
                    price = Bringthebag().Sum_cart()
                    
                };
                db.OrderT.Add(bag_register);
                db.SaveChanges();
           
            }
            Bringthebag().Clear_cart();
            string order_message = "Sipariş alındı Sipariş no=" + new_order_num;

            return RedirectToAction("Siparisteslim", "Shoppingbag", new { order_message });
        }
    }
}