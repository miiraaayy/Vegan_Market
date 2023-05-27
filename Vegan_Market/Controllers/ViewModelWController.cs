using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vegan_Market.Models;

namespace Vegan_Market.Controllers.Web_Controller
{
    public class ViewModelWController : Controller
    {
        // GET: Categories
        Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();
        ViewModel vm = new ViewModel();
      

        public async Task<ActionResult> Index(int id)
        {
            vm.Category = (from c in db.Category where c.Cate_id == id select c).ToList();
            vm.Sub_category = db.Sub_Category.Where(x=>x.cate_no1==id).ToList();
            vm.Product = db.Product.Where(x => x.cate_no == id).ToList();
            ViewBag.catename = db.Category.FirstOrDefault<Category>(x => x.Cate_id == id).Cate_name;
            ViewBag.catedescript = db.Category.FirstOrDefault<Category>(x => x.Cate_id == id).Cate_descript;
            ViewBag.productnum = db.Product.Where(x => x.cate_no == id).Count();
            ViewBag.cate_id = db.Category.FirstOrDefault(x => x.Cate_id == id).Cate_id;
            vm.WebFooters = db.WebFooter.ToList();
          

            return View(vm);
        }


      

        public ActionResult All_Filter(int? sub_category_ids,int? stocks,bool? availability)
        {
            
            vm.Sub_category = db.Sub_Category.ToList();
            vm.Product = db.Product.Where(x => x.sub_no == sub_category_ids || x.product_num > stocks || x.product_discount == availability).ToList();
            ViewBag.count= db.Product.Where(x => x.sub_no == sub_category_ids || x.product_num > stocks || x.product_discount == availability).Count();
            return View(vm);
        }

        public ActionResult Instock(int id)
        {
            vm.Category = db.Category.ToList();
            vm.Sub_category = db.Sub_Category.Where(x=>x.cate_no1 == id).ToList();
            vm.Brand = db.Brand.ToList();
            vm.Product = db.Product.Where(x => x.cate_no == id && x.product_num > 0).ToList();
            ViewBag.count = db.Product.Where(p =>p.cate_no ==id  && p.product_num > 0).Count();
            ViewBag.cate_id = db.Category.FirstOrDefault(x => x.Cate_id == id).Cate_id;
            vm.WebFooters = db.WebFooter.ToList();
            return View(vm);
        }

        public ActionResult Sales(int id)
        {
            vm.Category = db.Category.ToList();
            vm.Sub_category = db.Sub_Category.Where(x => x.cate_no1 == id).ToList();
            vm.Brand = db.Brand.ToList();
            vm.Product = db.Product.Where(p =>p.cate_no==id  && p.product_discount == true).ToList();
            ViewBag.count = db.Product.Where(p =>p.cate_no ==id    && p.product_discount == true).Count();
            ViewBag.cate_id = db.Category.FirstOrDefault(x => x.Cate_id == id).Cate_id;
            vm.WebFooters = db.WebFooter.ToList();
            return View(vm);

        }

        [HttpPost]
        public ActionResult Filter(int minPrice, int maxPrice,int cate_id)
        {
            vm.Category = db.Category.ToList();
            ViewBag.cate_id = db.Category.FirstOrDefault(x => x.Cate_id == cate_id).Cate_id;
            vm.Sub_category = db.Sub_Category.Where(x => x.cate_no1 == cate_id).ToList();
            vm.Brand = db.Brand.ToList();
            vm.Product = db.Product
           .Where(p =>p.cate_no == cate_id  && p.product_price >= minPrice && p.product_price <= maxPrice)
           .ToList();
            ViewBag.count = db.Product.Where(p => p.cate_no == cate_id && p.product_price >= minPrice && p.product_price <= maxPrice).Count();
            vm.WebFooters = db.WebFooter.ToList();
            return View(vm);
        }


        public async Task<ActionResult> subcatelist(int id)
        {
            vm.Sub_category = db.Sub_Category.Where(x => x.sub_id == id).ToList();
            vm.Product = db.Product.Where(x => x.sub_no == id).ToList();
            ViewBag.subcatename = db.Sub_Category.FirstOrDefault(x=>x.sub_id == id).sub_name;
            ViewBag.subcatdespt = db.Sub_Category.FirstOrDefault(x=>x.sub_id == id).sub_descript;
            ViewBag.subpicture = db.Sub_Category.FirstOrDefault<Sub_Category>(x => x.sub_id == id).sub_picture;
            ViewBag.product_number = db.Product.Where(x=>x.sub_no == id).Count();
            ViewBag.catename = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).Category.Cate_name;
            ViewBag.cate_id= db.Sub_Category.FirstOrDefault(x => x.sub_id == id).Category.Cate_id;
            ViewBag.sub_id = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).sub_id;
            vm.WebFooters = db.WebFooter.ToList();
            return View(vm);
        }

        public ActionResult All_Filter_sub(int? stocks,bool? availability)
        {
            vm.Product = db.Product.Where(x => x.product_num < stocks || x.product_discount == availability).ToList();
            ViewBag.count = db.Product.Where(x => x.product_num < stocks || x.product_discount == availability).Count();
            return View();
        }
        public ActionResult Instock_subs(int id)
        {


            vm.Sub_category = db.Sub_Category.Where(x => x.sub_id == id).ToList();
            vm.Product = db.Product.Where(x => x.sub_no == id && x.product_num > 0).ToList();
            ViewBag.subcatename = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).sub_name;
            ViewBag.subcatdespt = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).sub_descript;
            ViewBag.subpicture = db.Sub_Category.FirstOrDefault<Sub_Category>(x => x.sub_id == id).sub_picture;
            ViewBag.product_number = db.Product.Where(x => x.sub_no == id && x.product_num > 0).Count();
            ViewBag.catename = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).Category.Cate_name;
            ViewBag.cate_id = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).Category.Cate_id;
            ViewBag.sub_id = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).sub_id;
            vm.WebFooters = db.WebFooter.ToList();
            return View(vm);
        
        }


        public ActionResult Sales_subs(int id)
        {
            vm.Sub_category = db.Sub_Category.Where(x => x.sub_id == id).ToList();
            vm.Product = db.Product.Where(p => p.sub_no == id && p.product_discount == true).ToList();
            ViewBag.subcatename = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).sub_name;
            ViewBag.subcatdespt = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).sub_descript;
            ViewBag.subpicture = db.Sub_Category.FirstOrDefault<Sub_Category>(x => x.sub_id == id).sub_picture;
            ViewBag.product_number = db.Product.Where(x => x.sub_no == id && x.product_discount == true).Count();
            ViewBag.catename = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).Category.Cate_name;
            ViewBag.cate_id = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).Category.Cate_id;
            ViewBag.sub_id = db.Sub_Category.FirstOrDefault(x => x.sub_id == id).sub_id;
            vm.WebFooters = db.WebFooter.ToList();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Filters(int minPrice, int maxPrice, int sub_id)
        {
            vm.Sub_category = db.Sub_Category.Where(x => x.sub_id == sub_id).ToList();
            vm.Product = db.Product
           .Where(p => p.sub_no == sub_id && p.product_price >= minPrice && p.product_price <= maxPrice)
           .ToList();
            ViewBag.subcatename = db.Sub_Category.FirstOrDefault(x => x.sub_id == sub_id).sub_name;
            ViewBag.subcatdespt = db.Sub_Category.FirstOrDefault(x => x.sub_id == sub_id).sub_descript;
            ViewBag.subpicture = db.Sub_Category.FirstOrDefault<Sub_Category>(x => x.sub_id == sub_id).sub_picture;
            ViewBag.product_number = db.Product.Where(x => x.sub_no == sub_id && x.product_price >= minPrice && x.product_price <= maxPrice).Count();
            ViewBag.catename = db.Sub_Category.FirstOrDefault(x => x.sub_id == sub_id).Category.Cate_name;
            ViewBag.cate_id = db.Sub_Category.FirstOrDefault(x => x.sub_id == sub_id).Category.Cate_id;
            ViewBag.sub_id = db.Sub_Category.FirstOrDefault(x => x.sub_id == sub_id).sub_id;
            vm.WebFooters = db.WebFooter.ToList();
            return View(vm);
        }

        public async Task<ActionResult> productlist(int id)
        {
            vm.Product = db.Product.Where(x => x.product_id == id).ToList();
            ViewBag.product_name= db.Product.FirstOrDefault(x => x.product_id == id).product_name;
            ViewBag.picture1 = db.Product.FirstOrDefault(x => x.product_id == id).picture_1;
            ViewBag.picture2 = db.Product.FirstOrDefault(x => x.product_id == id).picture_2;
            ViewBag.picture3 = db.Product.FirstOrDefault(x => x.product_id == id).picture_3;
            ViewBag.picture4 = db.Product.FirstOrDefault(x => x.product_id == id).picture_4;
            ViewBag.decription = db.Product.FirstOrDefault(x => x.product_id == id).description;
            ViewBag.brand = db.Product.FirstOrDefault(x => x.product_id == id).Brand.brand_name;
            ViewBag.cate = db.Product.FirstOrDefault(x => x.product_id == id).Category.Cate_name;
            ViewBag.subcate = db.Product.FirstOrDefault(x => x.product_id == id).Sub_Category.sub_name;
            var stock_num = db.Product.FirstOrDefault(x => x.product_id == id).product_num;
            var product_id = db.Product.FirstOrDefault(x => x.product_id == id);
            vm.WebFooters = db.WebFooter.ToList();
    
           
           
                var producti = product_id.product_num;
                if (producti <= 0)
                {
                    ViewBag.toplam = "Stokta kalmamıştır.";
                }
                else
                {
                    ViewBag.toplam = product_id.product_num;
                }
            
            

      

            ViewBag.customer_no = Session["customer"];
            ViewBag.product_no = Session["product_no"];

            if (Session["customer"] == null )
            {

                    vm.Product_command = db.Product_Command.Where(x => x.product_no == id).ToList();
             
            }
            else
            {
                var customer_no = (int)Session["customers"];
                bool anycommand = db.Product_Command.Any(x => x.customer_no == customer_no);

                if (anycommand == true)
                {
                    vm.Product_command = db.Product_Command.Where(x => x.product_no == id).OrderByDescending(x => x.customer_no == customer_no).ToList();

                }

                else
                {
                    vm.Product_command = db.Product_Command.Where(x => x.product_no == id).ToList();
                }
            }
           
            ViewBag.command_count = db.Product_Command.Where(x => x.product_no == id).Count();
            var average_rating = db.Product_Command.Where(x => x.product_no == id).Select(x => x.rating).DefaultIfEmpty(0).Average();
            ViewBag.ortalama = average_rating;
       
            return View(vm);
        }


        public ActionResult Command()
        {
            
            ViewBag.customer_no = Session["customer"];
            ViewBag.product_no = Session["product_no"];

            return View();

        }

        [HttpPost]
        public ActionResult Command(Product_Command view)
        {
            if (ModelState.IsValid)
            {
                view.Customer = db.Customer.Find(view.customer_no);
                view.Product = db.Product.Find(view.product_no);

                db.Product_Command.Add(view);
                db.SaveChanges();
                return RedirectToAction("productlist",new { id = view.product_no });
            }
            ViewBag.customer_no = Session["customer"];
            ViewBag.product_no = Session["product_no"];
            
            return View(view);

        }

    }
}