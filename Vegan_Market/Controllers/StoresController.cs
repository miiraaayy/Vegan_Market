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
    public class StoresController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: Stores
        public async Task<ActionResult> Index()
        {
            return View(await db.Stores.ToListAsync());
        }

       

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "storesId,fabric_name,fabric_address,fabric_worktime,image")] Stores stores,HttpPostedFileBase file)
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
                            String yol = Path.Combine(Server.MapPath("~/storeimage"), dosya_adi);
                            file.SaveAs(yol);
                            stores.image = "/storeimage/" + dosya_adi;
                        }


                    }
                }
                db.Stores.Add(stores);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stores);
        }

        // GET: Stores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stores stores = await db.Stores.FindAsync(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "storesId,fabric_name,fabric_address,fabric_worktime,image")] Stores stores, HttpPostedFileBase file)
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
                            String yol = Path.Combine(Server.MapPath("~/storeimage"), dosya_adi);
                            file.SaveAs(yol);
                            stores.image = "/storeimage/" + dosya_adi;
                        }


                    }
                }
                db.Entry(stores).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stores);
        }


        public async Task<ActionResult> Delete(int id)
        {
            Stores stores = await db.Stores.FindAsync(id);
            db.Stores.Remove(stores);
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
