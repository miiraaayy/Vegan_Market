using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vegan_Market.Models
{
   
    public class Shoppingcart
    {

        private List<Cart> shoppingbag = new List<Cart>();

        public List<Cart> Shoppingbag { get => shoppingbag;}

      

        

        public void Add_cart(Product product,byte product_number)
        {
            
            var exist_product = shoppingbag.FirstOrDefault(x => x.product.product_id == product.product_id);
            if (exist_product == null)
            {
                shoppingbag.Add(new Cart { product = product, product_number = 1 });
            }
            else if (product_number == 0) exist_product.product_number += 1;
            else exist_product.product_number = product_number;


        }
        public void Remove_cart(Product product)
        {
            shoppingbag.RemoveAll(x => x.product.product_id == product.product_id);
        }
        public decimal Sum_cart()
        {
            return shoppingbag.Sum(x => x.product.product_price * x.product_number);
        }

        public void Clear_cart()
        {
            shoppingbag.Clear();
        }
    }
}