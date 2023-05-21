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

        public ActionResult Product_Command()
        {
       
            var commands = db.Product_Command.ToList();
            return View(commands);
        }

        public ActionResult DateCommand(DateTime? startDate, DateTime? endDate)
        {
            endDate = endDate?.AddDays(1); //Bitiş tarihini saatten dolayı almadığı için böyle bir şey ekledik.Bitiş tarihini bir sonraki gün alsın ki girdiğim bitiş tarihinide  kabul etsin ve listelesin.
            var result = db.Product_Command.Where(r => r.command_date >= startDate && r.command_date < endDate).ToList();
            return View(result);
           
        }

        public ActionResult Search(string customerName)
        {
            if (string.IsNullOrEmpty(customerName))
            {
                ViewBag.Message = "Müşteri adı girilmedi.";
                return View("NoResults");
            }

            var results = db.Product_Command
                .Where(r => r.Customer.customer_name.Contains(customerName))
                .ToList();

            if (results.Count == 0)
            {
                ViewBag.Message = "Bu üye hiç yorum yapmamıştır.";
                return View("NoResults");
            }
            return View(results);

        }

        public ActionResult NoResults()
        {
            return View();
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
