using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        // citty street bu

        [Required]
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }

        [ForeignKey("Seller")]
        public string sellerId { get; set; }
        public Seller Seller { get; set; }


    }
}
