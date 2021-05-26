using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class Governorate
    {
        public int GovernorateId { get; set; }

        [Required]
        [StringLength(30,MinimumLength =3)]
        public string GovernorateName { get; set; }


        [Required]
        public int Duration { get; set; }


        [Required]
        public float ShippingValue { get; set; }
        [DefaultValue(false)]
        public bool Isdeleted { get; set; }

        public virtual List<ShippingDetail> ShippingDetails { get; set; }


    }
}
