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
    public class FAQsController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: FAQs
        public async Task<ActionResult> Index()
        {
            return View(await db.FAQ.ToListAsync());
        }

       
        // GET: FAQs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FAQs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FaqId,question_txt,answer_txt,image")] FAQ fAQ, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var dosya_adi = "";
                if (file != null)
                {
                    if (file.ContentLength > 0)//dosya transfer olduysa
                    {
                        String uzanti = Path.GetExtension(file.FileName);
                        if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                        {
                            dosya_adi = Path.GetFileName(file.FileName);
                            String yol = Path.Combine(Server.MapPath("~/faqimage"), dosya_adi);
                            file.SaveAs(yol);

                    
                        }
                     
                    }
                  
                }
                fAQ.image = dosya_adi;
                db.FAQ.Add(fAQ);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fAQ);
        }

        // GET: FAQs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ fAQ = await db.FAQ.FindAsync(id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            return View(fAQ);
        }

        // POST: FAQs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FaqId,question_txt,answer_txt,image")] FAQ fAQ, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var dosya_adi = "";
                if (file != null)
                {
                    if (file.ContentLength > 0)//dosya transfer olduysa
                    {
                        String uzanti = Path.GetExtension(file.FileName);
                        if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                        {
                            dosya_adi = Path.GetFileName(file.FileName);
                            String yol = Path.Combine(Server.MapPath("~/faqimage"), dosya_adi);
                            file.SaveAs(yol);


                        }

                    }

                }
                fAQ.image = dosya_adi;
                db.Entry(fAQ).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fAQ);
        }

    

        public async Task<ActionResult> Delete(int id)
        {
            FAQ fAQ = await db.FAQ.FindAsync(id);
            db.FAQ.Remove(fAQ);
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
