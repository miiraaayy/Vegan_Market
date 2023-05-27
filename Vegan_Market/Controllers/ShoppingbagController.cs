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
        ViewModel vm = new ViewModel();
        public ActionResult AOrderList()
        {
            var orderlist = db.OrderT.ToList();
            return View(orderlist);
        }

        public ActionResult Search(string customerName)
        {
            if (string.IsNullOrEmpty(customerName))
            {
                ViewBag.Message = "Müşteri adı girilmedi.";
                return View("NoResults");
            }

            var results = db.OrderT
                .Where(r => r.Customer.customer_name.Contains(customerName))
                .ToList();

            if (results.Count == 0)
            {
                ViewBag.Message = "Bu üyenin hiç siparişi yoktur.";
                return View("NoResults");
            }
            return View(results);
        }
        public ActionResult NoResults()
        {
            return View();
        }

        public ActionResult DateOrder(DateTime? startDate, DateTime? endDate)
        {
            endDate = endDate?.AddDays(1); //Bitiş tarihini saatten dolayı almadığı için böyle bir şey ekledik.Bitiş tarihini bir sonraki gün alsın ki girdiğim bitiş tarihinide  kabul etsin ve listelesin.
            var result = db.OrderT.Where(r => r.order_date >= startDate && r.order_date < endDate).ToList();
            return View(result);
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
        public ActionResult Index(string order_message,int? product_id)
        {
            var product_zero = db.Product.FirstOrDefault(x => x.product_id == product_id && x.product_num <= 0);

            if(product_zero != null)
            {
                return RedirectToAction("Stock");
            }
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

       public ActionResult Stock()
        {
            ViewBag.Message = "Ürün stokta kalmamıştır.";
            return View();
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
                int productId = register_bag.product.product_id;
                int productNumber = register_bag.product_number;
               
                var product1 = db.Product.FirstOrDefault(x => x.product_id == productId);

                if (product1 != null)
                {
                    product1.product_num -= productNumber;
                }
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