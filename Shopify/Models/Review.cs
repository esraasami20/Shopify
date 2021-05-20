using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DefaultValue(false)]
        public bool Isdeleted { get; set; }
        public virtual Product Product { get; set; }

    }
}
