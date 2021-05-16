using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class ProductImages
    {

        [ForeignKey("Product")]

        public int ProductId { get; set; }


        public string Image { get; set; }


        public virtual Product Product { get; set; }

    }
}
