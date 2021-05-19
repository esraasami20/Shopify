using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Repository
{
    public class SellerRepo
    {

        ShopifyContext _db;
        public SellerRepo(ShopifyContext db)
        {
            _db = db;
        }

        public void AddSellerId(string id)
        {
            _db.Sellers.Add(new Seller() { SellerId = id });
            _db.SaveChanges();
        }

    }
}
