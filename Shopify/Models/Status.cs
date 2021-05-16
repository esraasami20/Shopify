using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class Status
    {

        public int StatusId { get; set; }
        [Required]
        [StringLength(30,MinimumLength =5)]
        public string StatusName { get; set; }

        public virtual  List<Cart> Carts { get; set; }
    }
}
