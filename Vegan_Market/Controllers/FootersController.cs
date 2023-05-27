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
    public class FootersController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: Footers
        public async Task<ActionResult> Index()
        {
            return View(await db.Footer.ToListAsync());
        }

     

        // GET: Footers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Footers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FooterId,descript_txt,location_txt,location_head,account_txt,account_head,main_txt,main_head,footer_menu_txt,menu_head,account_txt_1,main_txt_1,main_txt_3,footer_menu_txt_1")] Footer footer)
        {
            if (ModelState.IsValid)
            {
                db.Footer.Add(footer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(footer);
        }

        // GET: Footers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Footer footer = await db.Footer.FindAsync(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        // POST: Footers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FooterId,descript_txt,location_txt,location_head,account_txt,account_head,main_txt,main_head,footer_menu_txt,menu_head,account_txt_1,main_txt_1,main_txt_3,footer_menu_txt_1")] Footer footer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(footer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(footer);
        }

        public async Task<ActionResult> Delete(int id)
        {
            Footer footer = await db.Footer.FindAsync(id);
            db.Footer.Remove(footer);
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
