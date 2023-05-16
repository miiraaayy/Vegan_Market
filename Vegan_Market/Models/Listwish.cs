using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vegan_Market.Models
{
    public class Listwish
    {
        private List<Wishcart> wishcart = new List<Wishcart>();

        public List<Wishcart> Wishcart { get => wishcart; }


        public void Add_wishcart(Product product)
        {
            var exist_product = wishcart.FirstOrDefault(x => x.product.product_id == product.product_id);
            if (exist_product == null)
            {
                wishcart.Add(new Wishcart { product = product});
            }

        }

        public void Remove_wishcart(Product product)
        {
            wishcart.RemoveAll(x => x.product.product_id == product.product_id);
        }
        public void Clear_cart()
        {
            wishcart.Clear();
        }
    }
}