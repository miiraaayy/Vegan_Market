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
    public class SubcategoryController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: Subcategory
        public async Task<ActionResult> Index()
        {
            var sub_Category = db.Sub_Category.Include(s => s.Category);
            return View(await sub_Category.ToListAsync());
        }

        // GET: Subcategory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Category sub_Category = await db.Sub_Category.FindAsync(id);
            if (sub_Category == null)
            {
                return HttpNotFound();
            }
            return View(sub_Category);
        }

        // GET: Subcategory/Create
        public ActionResult Create()
        {
            ViewBag.cate_no1 = new SelectList(db.Category, "Cate_id", "Cate_name");
            return View();
        }

        // POST: Subcategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "sub_id,sub_name,cate_no1,sub_picture,sub_descript")] Sub_Category sub_Category, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {


                if (file != null)
                {

                    if (file.ContentLength > 0)//dosya transfer olduysa
                    {
                        String uzanti = Path.GetExtension(file.FileName);
                        if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                        {

                            var dosya_adi = Path.GetFileName(file.FileName);
                            String yol = Path.Combine(Server.MapPath("~/subcateimg"), dosya_adi);
                            file.SaveAs(yol);
                            sub_Category.sub_picture = "/subcateimg/" + dosya_adi;
                        }


                    }

                }
                    db.Sub_Category.Add(sub_Category);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cate_no1 = new SelectList(db.Category, "Cate_id", "Cate_name", sub_Category.cate_no1);
            return View(sub_Category);
        }

        // GET: Subcategory/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Category sub_Category = await db.Sub_Category.FindAsync(id);
            if (sub_Category == null)
            {
                return HttpNotFound();
            }
            ViewBag.cate_no1 = new SelectList(db.Category, "Cate_id", "Cate_name", sub_Category.cate_no1);
            return View(sub_Category);
        }

        // POST: Subcategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "sub_id,sub_name,cate_no1,sub_picture,sub_descript")] Sub_Category sub_Category, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    if (file.ContentLength > 0)//dosya transfer olduysa
                    {

                        String uzanti = Path.GetExtension(file.FileName);
                        if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                        {
                            var dosya_adi = Path.GetFileName(file.FileName);
                            String yol = Path.Combine(Server.MapPath("~/subcateimg"), dosya_adi);
                            file.SaveAs(yol);
                            sub_Category.sub_picture = "/subcateimg/" + dosya_adi;
                        }

                    }
                    }
                else
                {
                    ModelState.AddModelError("sub_picture", "Lütfen bir resim dosyası seçin.");
                    ViewBag.cate_no1 = new SelectList(db.Category, "Cate_id", "Cate_name", sub_Category.cate_no1);
                    return View(sub_Category);

                }
                db.Entry(sub_Category).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cate_no1 = new SelectList(db.Category, "Cate_id", "Cate_name", sub_Category.cate_no1);
            return View(sub_Category);
        }

       
       
        public async Task<ActionResult> Delete(int id)
        {
            Sub_Category sub_Category = await db.Sub_Category.FindAsync(id);
            db.Sub_Category.Remove(sub_Category);
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
