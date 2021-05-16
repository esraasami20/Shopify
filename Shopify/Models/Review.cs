using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class Review
    {

        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public float? review { get; set; }
        public string comment { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
