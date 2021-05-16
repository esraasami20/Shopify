﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(50,MinimumLength =5)]
        public string ProductName { get; set; }

        [Required]
        [Range(5,maximum:100000)]
        public float Price { get; set; }

        public float ?Discount { get; set; }

       

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Size { get; set; }

        [Required]
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        public virtual List<ProductImages> ProductImages { get; set; }
        public virtual List<ProductDetail> ProductDetails { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("subCategory")]
        public int SubCategotyId { get; set; }
        public SubCategory subCategory { get; set; }


        [ForeignKey("Promotions")]
        public int PromotionId { get; set; }
        public Promotions Promotions { get; set; }


        public virtual List<Review> Reviews { get; set; }
        public virtual List<View> Views { get; set; }
       






    }
}
