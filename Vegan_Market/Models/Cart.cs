using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vegan_Market.Models
{
    public class Cart
    {
        public Product product { get; set; }
        public byte product_number { get; set; }
    }
}