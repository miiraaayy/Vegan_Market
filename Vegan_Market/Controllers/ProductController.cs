using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vegan_Market.Models;
using System.IO;

namespace Vegan_Market.Controllers
{
    public class ProductController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: Product
        public async Task<ActionResult> Index()
        {
            var product = db.Product.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Sub_Category);
      
            return View(await product.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.brand_no = new SelectList(db.Brand, "brand_id", "brand_name");
            ViewBag.cate_no = new SelectList(db.Category, "Cate_id", "Cate_name");
            ViewBag.sub_no = new SelectList(db.Sub_Category, "sub_id", "sub_name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "product_id,product_name,product_price,cate_no,sub_no,brand_no,description,picture_1,picture_2,picture_3,picture_4,stock_no,product_discount,barcode_no,discount_text,product_num")] Product product, HttpPostedFileBase[] file)
        {

            if (ModelState.IsValid)
            {

                foreach (HttpPostedFileBase files in file)
                {


                    if (file != null)
                    {

                        if (files.ContentLength > 0)//dosya transfer olduysa
                        {
                            String uzanti = Path.GetExtension(files.FileName);
                            if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                            {

                                var dosya_adi = Path.GetFileName(files.FileName);
                                String yol = Path.Combine(Server.MapPath("~/image"), dosya_adi);
                                files.SaveAs(yol);

                                if (product.picture_1 == null)
                                {
                                    product.picture_1 = "/image/" + dosya_adi;
                                    continue;
                                }
                                else if (product.picture_2 == null)
                                {
                                    product.picture_2 = "/image/" + dosya_adi;
                                    continue;
                                }
                                else if (product.picture_3 == null)
                                {
                                    product.picture_3 = "/image/" + dosya_adi;
                                    continue;
                                }
                                else if (product.picture_4 == null)
                                {
                                    product.picture_4 = "/image/" + dosya_adi;
                                    break;
                                }



                            }


                        }
                    }


                    db.Product.Add(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            



            }
            ViewBag.brand_no = new SelectList(db.Brand, "brand_id", "brand_name", product.brand_no);
            ViewBag.cate_no = new SelectList(db.Category, "Cate_id", "Cate_name", product.cate_no);
            ViewBag.sub_no = new SelectList(db.Sub_Category, "sub_id", "sub_name", product.sub_no);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.brand_no = new SelectList(db.Brand, "brand_id", "brand_name", product.brand_no);
            ViewBag.cate_no = new SelectList(db.Category, "Cate_id", "Cate_name", product.cate_no);
            ViewBag.sub_no = new SelectList(db.Sub_Category, "sub_id", "sub_name", product.sub_no);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "product_id,product_name,product_price,cate_no,sub_no,brand_no,description,picture_1,picture_2,picture_3,picture_4,stock_no,product_discount,barcode_no,discount_text,product_num")] Product product, HttpPostedFileBase[] file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file[0] != null)
                {
                    foreach (HttpPostedFileBase files in file)
                    {
                        if (files.ContentLength > 0)//dosya transfer olduysa
                        {

                            String uzanti = Path.GetExtension(files.FileName);
                            if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                            {
                                var dosya_adi = Path.GetFileName(files.FileName);
                                String yol = Path.Combine(Server.MapPath("~/image"), dosya_adi);
                                files.SaveAs(yol);
                                if (product.picture_1 == null)
                                {
                                    product.picture_1 = "/image/" + dosya_adi;
                                    continue;
                                }
                                else if (product.picture_2 == null)
                                {
                                    product.picture_2 = "/image/" + dosya_adi;
                                    continue;
                                }
                                else if (product.picture_3 == null)
                                {
                                    product.picture_3 = "/image/" + dosya_adi;
                                    continue;
                                }
                                else if (product.picture_4 ==null)
                                {
                                    product.picture_4 = "/image/" + dosya_adi;
                                    continue;
                                }
                                else
                                {
                                    return ViewBag.exception = "En fazla 4 fotoğraf seçebilirsiniz";
                                }
                            }

                        }
                    }
                }

                else
                {
                    ModelState.AddModelError("picture_1", "Lütfen bir resim dosyası seçin.");
                    ViewBag.brand_no = new SelectList(db.Brand, "brand_id", "brand_name", product.brand_no);
                    ViewBag.cate_no = new SelectList(db.Category, "Cate_id", "Cate_name", product.cate_no);
                    ViewBag.sub_no = new SelectList(db.Sub_Category, "sub_id", "sub_name", product.sub_no);
                    return View(product);


                }
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.brand_no = new SelectList(db.Brand, "brand_id", "brand_name", product.brand_no);
            ViewBag.cate_no = new SelectList(db.Category, "Cate_id", "Cate_name", product.cate_no);
            ViewBag.sub_no = new SelectList(db.Sub_Category, "sub_id", "sub_name", product.sub_no);
            return View(product);
        }

       
        public async Task<ActionResult> Delete(int id)
        {
            Product product = await db.Product.FindAsync(id);
            db.Product.Remove(product);
            await db.SaveChangesAsync();
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
