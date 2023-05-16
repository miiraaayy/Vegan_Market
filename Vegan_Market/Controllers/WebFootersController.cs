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

namespace Vegan_Market.Controllers
{
    public class WebFootersController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: WebFooters
        public async Task<ActionResult> Index()
        {
            return View(await db.WebFooter.ToListAsync());
        }

       

        // GET: WebFooters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebFooters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WebId,contact_txt,support_txt,contact_head,support_head")] WebFooter webFooter)
        {
            if (ModelState.IsValid)
            {
                db.WebFooter.Add(webFooter);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(webFooter);
        }

        // GET: WebFooters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebFooter webFooter = await db.WebFooter.FindAsync(id);
            if (webFooter == null)
            {
                return HttpNotFound();
            }
            return View(webFooter);
        }

        // POST: WebFooters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WebId,contact_txt,support_txt,contact_head,support_head")] WebFooter webFooter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webFooter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(webFooter);
        }

        public async Task<ActionResult> Delete(int id)
        {
            WebFooter webFooter = await db.WebFooter.FindAsync(id);
            db.WebFooter.Remove(webFooter);
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
