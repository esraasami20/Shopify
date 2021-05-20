using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class Promotions
    {

        public int PromotionsId { get; set; }
      

        public float Discount { get; set; }
        public float Description { get; set; }
        public string Image { get; set; }
        [DefaultValue(false)]
        public bool Isdeleted { get; set; }



        [ForeignKey("Seller")]
        public string SellerId { get; set; }
        public virtual Seller Seller { get; set; }

        public virtual List<Product> Products { get; set; }



    }
}
