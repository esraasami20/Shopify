using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class Seller: ApplicationUser
    {
       

     
        public virtual List<Inventory> Inventories { get; set; }
        public virtual List<Promotions> Promotions { get; set; }



    }
}
