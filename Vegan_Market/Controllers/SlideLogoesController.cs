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
    public class SlideLogoesController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: SlideLogoes
        public async Task<ActionResult> Index()
        {
            return View(await db.SlideLogo.ToListAsync());
        }

       
        // GET: SlideLogoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlideLogoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SlideLogoId,SlideImage,Logo,SlideImage_1,SlideImage_2")] SlideLogo slideLogo, HttpPostedFileBase[] file, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                foreach (HttpPostedFileBase fileses in file)
                {


                    if (file != null)
                    {

                        if (fileses.ContentLength > 0)//dosya transfer olduysa
                        {
                            String uzanti = Path.GetExtension(fileses.FileName);
                            if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                            {

                                var dosya_adi = Path.GetFileName(fileses.FileName);
                                String yol = Path.Combine(Server.MapPath("~/sliderimage"), dosya_adi);
                                fileses.SaveAs(yol);

                                if (slideLogo.SlideImage == null)
                                {
                                    slideLogo.SlideImage = "/sliderimage/" + dosya_adi;
                                    continue;
                                }
                                else if (slideLogo.SlideImage_1 == null)
                                {
                                    slideLogo.SlideImage_1 = "/sliderimage/" + dosya_adi;
                                    continue;
                                }
                                else
                                {
                                    slideLogo.SlideImage_2 = "/sliderimage/" + dosya_adi;
                                    break;
                                }


                            }


                        }
                    }

                  
                }
                var file_names = " ";
                if (files != null)
                {
                    if (files.ContentLength > 0)//dosya transfer olduysa
                    {
                        String uzanti = Path.GetExtension(files.FileName);
                        if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                        {
                            file_names = Path.GetFileName(files.FileName);
                            String yol = Path.Combine(Server.MapPath("~/logo"), file_names);
                            files.SaveAs(yol);

                    
                        }
                        
                    }
                   
                }

                slideLogo.Logo = file_names;
                db.SlideLogo.Add(slideLogo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(slideLogo);
        }

        // GET: SlideLogoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideLogo slideLogo = await db.SlideLogo.FindAsync(id);
            if (slideLogo == null)
            {
                return HttpNotFound();
            }
            return View(slideLogo);
        }

        // POST: SlideLogoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SlideLogoId,SlideImage,Logo,SlideImage_1,SlideImage_2")] SlideLogo slideLogo, HttpPostedFileBase[] file, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {



                foreach (HttpPostedFileBase fileses in file)
                {


                    if (file != null)
                    {

                        if (fileses.ContentLength > 0)//dosya transfer olduysa
                        {
                            String uzanti = Path.GetExtension(fileses.FileName);
                            if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                            {

                                var dosya_adi = Path.GetFileName(fileses.FileName);
                                String yol = Path.Combine(Server.MapPath("~/sliderimage"), dosya_adi);
                                fileses.SaveAs(yol);

                                if (slideLogo.SlideImage == null)
                                {
                                    slideLogo.SlideImage = "/sliderimage/" + dosya_adi;
                                    continue;
                                }
                                else if (slideLogo.SlideImage_1 == null)
                                {
                                    slideLogo.SlideImage_1 = "/sliderimage/" + dosya_adi;
                                    continue;
                                }
                                else
                                {
                                    slideLogo.SlideImage_2 = "/sliderimage/" + dosya_adi;
                                    break;
                                }


                            }


                        }
                    }


                }
                var file_names = " ";
                if (files != null)
                {
                    if (files.ContentLength > 0)//dosya transfer olduysa
                    {
                        String uzanti = Path.GetExtension(files.FileName);
                        if (uzanti.Equals(".jpg") || uzanti.Equals(".jpeg") || uzanti.Equals(".jfif") || uzanti.Equals(".png"))
                        {
                            file_names = Path.GetFileName(files.FileName);
                            String yol = Path.Combine(Server.MapPath("~/logo"), file_names);
                            files.SaveAs(yol);


                        }

                    }

                }
                slideLogo.Logo = file_names;
                db.Entry(slideLogo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(slideLogo);
        }


       

        public async Task<ActionResult> Delete(int id)
        {
            SlideLogo slideLogo = await db.SlideLogo.FindAsync(id);
            db.SlideLogo.Remove(slideLogo);
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
