using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class Customer:ApplicationUser
    {
        public  virtual List<View>  Views{ get; set; }
        public  virtual List<Review> Reviews{ get; set; }
        public  virtual List<Cart> Carts{ get; set; }
    }
}
