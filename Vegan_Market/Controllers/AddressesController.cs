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
    public class AddressesController : Controller
    {
        private Aslan_Commerce_vtEntities db = new Aslan_Commerce_vtEntities();

        // GET: Addresses
        public async Task<ActionResult> Index()
        {
            var address = db.Address.Include(a => a.Customer);
            return View(await address.ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Address.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }


        public ActionResult AddresCheck(int customer_no)
        {

            var exist_adres = db.Address.FirstOrDefault(x => x.customer_no == customer_no);
            if (exist_adres == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("AddresList", new { id = customer_no });
            }

        }


        // GET: Addresses/Create
        public ActionResult Create()
        {
            ViewBag.customer_no = Session["customers"];
            ViewBag.customer = Session["custom"];


            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "adress_id,country,city,town,address_text,customer_no")] Address address)
        {
            ViewBag.customer_no = Session["customers"];
            if (ModelState.IsValid)
            {
                db.Address.Add(address);
                await db.SaveChangesAsync();
          
                return RedirectToAction("AddresList",new { id=address.customer_no});

            }
         
        
            return View(address);
        }

        public async Task<ActionResult> AddresList(int id)
        {
            var address = db.Address.Where(x => x.customer_no == id).ToList();
            
            return View(address);
        }



        // GET: Addresses/Create
        public ActionResult New_Adres()
        {
            ViewBag.customer_no = Session["customers"];
            ViewBag.customer = Session["custom"];

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New_Adres([Bind(Include = "adress_id,country,city,town,address_text,customer_no")] Address address)
        {
            ViewBag.customer_no = Session["customers"];
            if (ModelState.IsValid)
            {
                db.Address.Add(address);
                await db.SaveChangesAsync();

                return RedirectToAction("AdresList", new { id = Session["customers"] });

            }


            return View(address);
        }


        public async Task<ActionResult> AdresList(int id)
        {
            var address = db.Address.Where(x => x.customer_no == id).ToList();
            return View(address);
        }

     

        public async Task<ActionResult> remove_Adress(int? id)
        {
            Address address = await db.Address.FindAsync(id);
            db.Address.Remove(address);
            await db.SaveChangesAsync();
            return RedirectToAction("AdresList", new { id=Session["customers"] });
        }

        // GET: Addresses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.customer_no = Session["customers"];
            ViewBag.customer = Session["custom"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Address.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
          
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "adress_id,country,city,town,customer_no,address_text")] Address address)
        {
            ViewBag.customer_no = Session["customers"];
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                await db.SaveChangesAsync();
               
                return RedirectToAction("AdresList",new { id=address.customer_no});
            }
    
            return View(address);
        }

  
        public async Task<ActionResult> Delete(int id)
        {
            Address address = await db.Address.FindAsync(id);
            db.Address.Remove(address);
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
