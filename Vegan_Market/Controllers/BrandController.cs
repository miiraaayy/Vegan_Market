using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vegan_Market.Models;
using System.IO;

namespace Vegan_Market.Controllers
{
    public class BrandController : Controller
    {
       Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: Brand
        public async Task<ActionResult> Index()
        {
            return View(await db.Brand.ToListAsync());
        }

        
        // GET: Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "brand_id,brand_name,brand_logo")] Brand brand, HttpPostedFileBase file)
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
                            String yol = Path.Combine(Server.MapPath("~/brandimage"), dosya_adi);
                            file.SaveAs(yol);
                            brand.brand_logo = "/brandimage/" + dosya_adi;
                           }


                        }
                   }
                      
                    db.Brand.Add(brand);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            
            return View(brand);
        }

        // GET: Brand/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = await db.Brand.FindAsync(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "brand_id,brand_name,brand_logo")] Brand brand, HttpPostedFileBase file)
        {
          //  brand.brand_name = brand.brand_name;

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
                            String yol = Path.Combine(Server.MapPath("~/brandimage"), dosya_adi);
                            file.SaveAs(yol);
                            brand.brand_logo = "/brandimage/" + dosya_adi;
                        }

                    }
                  }
               
                    db.Entry(brand).State = EntityState.Modified;
                 
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            return View(brand);
                }
            
           /* return View(brand);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brandupdate = db.Brand.Find(id);
            if (TryUpdateModel(brandupdate, "", new string[] { "brand_name"}))
            {
                try {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException /* dex 
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }*/
            
  // GET: Brand/Delete/5
     
        public async Task<ActionResult> Delete(int? id)
        {
            
                Brand brand = await db.Brand.FindAsync(id);
                db.Brand.Remove(brand);
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
