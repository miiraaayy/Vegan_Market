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
    public class Product_CommandController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: Product_Command
        public async Task<ActionResult> Index()
        {
            var product_Command = db.Product_Command.Include(p => p.Customer).Include(p => p.Product);
            return View(await product_Command.ToListAsync());
        }

        // GET: Product_Command/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Command product_Command = await db.Product_Command.FindAsync(id);
            if (product_Command == null)
            {
                return HttpNotFound();
            }
            return View(product_Command);
        }

        // GET: Product_Command/Create
        public ActionResult Create()
        {
            ViewBag.customer_no = Session["customers"];
            ViewBag.product_no = Session["product_no"];
            return View();
        }

        // POST: Product_Command/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "command_id,product_no,customer_no,command_text,command_date,rating")] Product_Command product_Command)
        {
            if (ModelState.IsValid)
            {
                db.Product_Command.Add(product_Command);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Products");
            }

            ViewBag.customer_no = Session["customers"];
            ViewBag.product_no = Session["product_no"];
            return View(product_Command);
        }

        // GET: Product_Command/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Command product_Command = await db.Product_Command.FindAsync(id);
            if (product_Command == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_no = new SelectList(db.Customer, "customer_id", "customer_name", product_Command.customer_no);
            ViewBag.product_no = new SelectList(db.Product, "product_id", "product_name", product_Command.product_no);
            return View(product_Command);
        }

        // POST: Product_Command/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "command_id,product_no,customer_no,command_text,command_date,rating")] Product_Command product_Command)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Command).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.customer_no = new SelectList(db.Customer, "customer_id", "customer_name", product_Command.customer_no);
            ViewBag.product_no = new SelectList(db.Product, "product_id", "product_name", product_Command.product_no);
            return View(product_Command);
        }

        // GET: Product_Command/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Command product_Command = await db.Product_Command.FindAsync(id);
            if (product_Command == null)
            {
                return HttpNotFound();
            }
            return View(product_Command);
        }

        // POST: Product_Command/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product_Command product_Command = await db.Product_Command.FindAsync(id);
            db.Product_Command.Remove(product_Command);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public ActionResult Product_Command()
        {
       
            var commands = db.Product_Command.ToList();
            return View(commands);
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
