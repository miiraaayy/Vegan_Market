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
    public class AboutUsesController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: AboutUses
        public async Task<ActionResult> Index()
        {
            return View(await db.AboutUs.ToListAsync());
        }


        // GET: AboutUses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AboutUses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AboutId,descript_txt_1,descript_txt_2,descript_txt_3,image_1,image_2,image_3,image_4,image_5")] AboutUs aboutUs,HttpPostedFileBase[] file)
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

                                var file_name = Path.GetFileName(files.FileName);
                                String yol = Path.Combine(Server.MapPath("~/aboutusimg"), file_name);
                                files.SaveAs(yol);

                                if (aboutUs.image_1 == null)
                                {
                                    aboutUs.image_1 = "/aboutusimg/" + file_name;
                                    continue;
                                }
                                else if (aboutUs.image_2 == null)
                                {
                                    aboutUs.image_2 = "/aboutusimg/" + file_name;
                                    continue;
                                }
                                else if (aboutUs.image_3 == null)
                                {
                                    aboutUs.image_3 = "/aboutusimg/" + file_name;
                                    continue;
                                }
                                else if (aboutUs.image_4 == null)
                                {
                                    aboutUs.image_4 = "/aboutusimg/" + file_name;
                                    continue;
                                }

                                else
                                {
                                    aboutUs.image_5 = "/aboutusimg/" + file_name;
                                    break;
                                }

                            }


                        }
                    }
              

                }
                db.AboutUs.Add(aboutUs);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aboutUs);
        }

        // GET: AboutUses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = await db.AboutUs.FindAsync(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // POST: AboutUses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AboutId,descript_txt_1,descript_txt_2,descript_txt_3,image_1,image_2,image_3,image_4,image_5")] AboutUs aboutUs, HttpPostedFileBase[] file)
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

                                var file_name = Path.GetFileName(files.FileName);
                                String yol = Path.Combine(Server.MapPath("~/aboutusimg"), file_name);
                                files.SaveAs(yol);

                                if (aboutUs.image_1 == null)
                                {
                                    aboutUs.image_1 = "/aboutusimg/" + file_name;
                                    continue;
                                }
                                else if (aboutUs.image_2 == null)
                                {
                                    aboutUs.image_2 = "/aboutusimg/" + file_name;
                                    continue;
                                }
                                else if (aboutUs.image_3 == null)
                                {
                                    aboutUs.image_3 = "/aboutusimg/" + file_name;
                                    continue;
                                }
                                else if (aboutUs.image_4 == null)
                                {
                                    aboutUs.image_4 = "/aboutusimg/" + file_name;
                                    continue;
                                }

                                else
                                {
                                    aboutUs.image_5 = "/aboutusimg/" + file_name;
                                    break;
                                }

                            }


                        }
                    }


                }

                db.Entry(aboutUs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aboutUs);
        }

   
        public async Task<ActionResult> Delete(int id)
        {
            AboutUs aboutUs = await db.AboutUs.FindAsync(id);
            db.AboutUs.Remove(aboutUs);
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
