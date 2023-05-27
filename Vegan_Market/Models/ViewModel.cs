using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vegan_Market.Models
{
    public class ViewModel
    {
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<Sub_Category> Sub_category { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Brand> Brand { get; set; }
        public IEnumerable<Product_Command> Product_command { get; set; }
        
        public IEnumerable<Shoppingcart> Shoppingcarts { get; set; }
        public Customer Customer { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Footer> Footers { get; set; }
     
        public IEnumerable<Nav> Navs { get; set; }
        public IEnumerable<AboutUs> AboutUs { get; set; }
     
        public IEnumerable<WebFooter> WebFooters { get; set; }
        public IEnumerable<SlideLogo> SlideLogos { get; set; }

    }
}