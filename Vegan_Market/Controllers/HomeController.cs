using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vegan_Market.Models;
namespace Vegan_Market.Controllers
{
    public class HomeController : Controller
    {
        Aslan_Commerce_vtEntities vegan_entities = new Aslan_Commerce_vtEntities();
        ViewModel vm = new ViewModel();
     
        public ActionResult Index()
        {
            vm.Category = vegan_entities.Category.ToList();
            vm.Product = vegan_entities.Product.ToList();
            vm.Product_command = vegan_entities.Product_Command.ToList();

            return View(vm);
        }
   
        public void Lists()
        {
            vm.Category = vegan_entities.Category.ToList();
            vm.Sub_category = vegan_entities.Sub_Category.ToList();
            vm.Brand = vegan_entities.Brand.ToList();
        }
        public ActionResult HomePage()
        {
            Lists();
            vm.Product = vegan_entities.Product.ToList();
            ViewBag.count = vegan_entities.Product.Count();
           
            return View(vm);
        }

    
        public ActionResult Filter_categories(int id)
        {
            Lists();
            vm.Product = vegan_entities.Product.Where(x =>x.cate_no==id).ToList();
            ViewBag.category_count = vegan_entities.Product.Where(x => x.cate_no == id).Count();

            return View(vm);
        }

        public ActionResult Instock()
        {
            Lists();
            vm.Product = vegan_entities.Product.Where(p => p.product_num >0).ToList();
            ViewBag.count = vegan_entities.Product.Where(p => p.product_num > 0).Count();
            return View(vm);
        }

        public ActionResult Sales()
        {
            Lists();
            vm.Product = vegan_entities.Product.Where(p => p.product_discount == true).ToList();
            ViewBag.count = vegan_entities.Product.Where(p => p.product_discount == true).Count();
            return View(vm);
        }
        public ActionResult Filter_brand(int id)
        {
            Lists();
            vm.Product = vegan_entities.Product.Where(x => x.brand_no == id).ToList();
            ViewBag.count = vegan_entities.Product.Where(x => x.brand_no == id).Count();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Filter(int minPrice, int maxPrice)
        {
            Lists();
            vm.Product = vegan_entities.Product
           .Where(p => p.product_price >= minPrice && p.product_price <= maxPrice)
           .ToList();
            return View(vm);
        }
        public ActionResult catesub_list()
        {
            vm.Category = vegan_entities.Category.ToList();
            vm.Sub_category = vegan_entities.Sub_Category.ToList();
            return PartialView(vm);
        }
        public ActionResult Faq()
        {
            return View();
        }
        public ActionResult Stores()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult categorylist()
        {
            var categories = vegan_entities.Category.ToList();
            return PartialView(categories);
        }
        public ActionResult brand_image_listele()
        {
            
            var brand = vegan_entities.Brand.ToList();
            return PartialView(brand);
        }

        public ActionResult categorieslist()
        {
            var categories = vegan_entities.Category.ToList();
            return PartialView(categories);
        }
        public ActionResult About()
        {
          

            return View();
        }

        public ActionResult login_customer()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult login_customer(Customer customer)
        {
            string message = "";
            var customers = vegan_entities.Customer.FirstOrDefault(x => x.customer_email == customer.customer_email && x.customer_password == customer.customer_password);
            if (customers != null)
            {
                Session["customer"] = customers;
                Session["customers"] = customers.customer_id;
                Session["custom"] = customers.customer_name;

                return RedirectToAction("Index","Home");
            }
            else
            {
                message = "Yanlış E-mail veya Şifre";
                ViewBag.uyari = message;
                return View(new { message});
            }

           


        }
        public async Task<ActionResult> safe_out()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index");
        }
      
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "customer_id,customer_name,customer_surname,customer_phone,customer_email,customer_password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    vegan_entities.Customer.Add(customer);
                    
                    await vegan_entities.SaveChangesAsync();
                    if (customer != null)
                        Session["customer"] = customer;
                    return RedirectToAction("Index", "Home");


                }
                catch (Exception fault)
                {
                    
                    int new_email = fault.InnerException.ToString().IndexOf("customer_email");

                    if (new_email == -1)
                     ViewBag.kayit_durum = "Böyle Email var Kayıt Yapılamıyor"; 
                  

                   
               
                }
            }
          
            return View(customer);
        }

    }
}