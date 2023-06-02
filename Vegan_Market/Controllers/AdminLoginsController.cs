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
    public class AdminLoginsController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: AdminLogins
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(AdminLogin adminLogin)
        {
            string message = "";
            var admin = db.AdminLogin.FirstOrDefault(x => x.admin_email == adminLogin.admin_email && x.admin_password == adminLogin.admin_password);
            if (admin != null)
            {
                Session["admin"] = admin;

                Session["admin_name"] = admin.admin_email;

                return RedirectToAction("Index", "Product");
            }
            else
            {
                message = "Yanlış E-mail veya Şifre";
                ViewBag.uyari = message;
                return View(new { message });
            }
        }

        public ActionResult safeout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index");
        }


        // GET: AdminLogins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "admin_email,admin_password")] AdminLogin adminLogin)
        {
            if (ModelState.IsValid)
            {
                db.AdminLogin.Add(adminLogin);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(adminLogin);
        }

        // GET: AdminLogins/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminLogin adminLogin = await db.AdminLogin.FindAsync(id);
            if (adminLogin == null)
            {
                return HttpNotFound();
            }
            return View(adminLogin);
        }

        // POST: AdminLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AdminLogin_id,admin_email,admin_password,admin_repassword")] AdminLogin adminLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminLogin).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(adminLogin);
        }

        // GET: AdminLogins/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminLogin adminLogin = await db.AdminLogin.FindAsync(id);
            if (adminLogin == null)
            {
                return HttpNotFound();
            }
            return View(adminLogin);
        }

        // POST: AdminLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AdminLogin adminLogin = await db.AdminLogin.FindAsync(id);
            db.AdminLogin.Remove(adminLogin);
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
