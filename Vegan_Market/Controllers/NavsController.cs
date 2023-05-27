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
    public class NavsController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: Navs
        public async Task<ActionResult> Index()
        {
            return View(await db.Nav.ToListAsync());
        }

     
        // GET: Navs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Navs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NavId,head_txt,descript_txt,image,image_1,head_txt_2,descript_txt_2")] Nav nav, HttpPostedFileBase file, HttpPostedFileBase files)
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
                            String yol = Path.Combine(Server.MapPath("~/navimg"), dosya_adi);
                            file.SaveAs(yol);
                            nav.image = "/navimg/" + dosya_adi;
                        }


                    }
                }


                if (files != null)
                {

                    if (files.ContentLength > 0)//dosya transfer olduysa
                    {
                        String uzanti = Path.GetExtension(files.FileName);
                        if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                        {

                            var dosya_adi = Path.GetFileName(files.FileName);
                            String yol = Path.Combine(Server.MapPath("~/navimg"), dosya_adi);
                            files.SaveAs(yol);
                            nav.image_1 = "/navimg/" + dosya_adi;
                        }


                    }
                }







                db.Nav.Add(nav);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nav);
        }

        // GET: Navs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nav nav = await db.Nav.FindAsync(id);
            if (nav == null)
            {
                return HttpNotFound();
            }
            return View(nav);
        }

        // POST: Navs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NavId,head_txt,descript_txt,image,image_1,head_txt_2,descript_txt_2")] Nav nav, HttpPostedFileBase file, HttpPostedFileBase files)
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
                            String yol = Path.Combine(Server.MapPath("~/navimg"), dosya_adi);
                            file.SaveAs(yol);
                            nav.image = "/navimg/" + dosya_adi;
                        }


                    }
                }


                if (files != null)
                {

                    if (files.ContentLength > 0)//dosya transfer olduysa
                    {
                        String uzanti = Path.GetExtension(files.FileName);
                        if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                        {

                            var dosya_adi = Path.GetFileName(files.FileName);
                            String yol = Path.Combine(Server.MapPath("~/navimg"), dosya_adi);
                            files.SaveAs(yol);
                            nav.image = "/navimg/" + dosya_adi;
                        }


                    }
                }


                db.Entry(nav).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nav);
        }

        public async Task<ActionResult> Delete(int id)
        {
            Nav nav = await db.Nav.FindAsync(id);
            db.Nav.Remove(nav);
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
