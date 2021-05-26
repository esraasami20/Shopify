using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Repository
{
    public class StatusRepo
    {
        ShopifyContext _db;
        public StatusRepo(ShopifyContext db)
        {
            _db = db;
        }
    }
}
