using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class InventoryProduct
    {

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

      

        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }


        [Required]
        public int Quantity { get; set; }


    }
}
