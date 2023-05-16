//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vegan_Market.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Favourites = new HashSet<Favourites>();
            this.OrderT = new HashSet<OrderT>();
            this.Product_Command = new HashSet<Product_Command>();
        }
    
        public int product_id { get; set; }
        public string product_name { get; set; }
        public decimal product_price { get; set; }
        public int cate_no { get; set; }
        public int sub_no { get; set; }
        public int brand_no { get; set; }
        public string description { get; set; }
        public string picture_1 { get; set; }
        public string picture_2 { get; set; }
        public string picture_3 { get; set; }
        public string picture_4 { get; set; }
        public int stock_no { get; set; }
        public bool product_discount { get; set; }
        public int barcode_no { get; set; }
        public Nullable<int> discount_text { get; set; }
        public int product_num { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favourites> Favourites { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderT> OrderT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Command> Product_Command { get; set; }
        public virtual Sub_Category Sub_Category { get; set; }
    }
}