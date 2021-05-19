using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Repository
{
    public class CustomerRepo
    {
        ShopifyContext _db;
        public CustomerRepo(ShopifyContext db)
        {
            _db = db ;
        }

        public void AddCustomerId(string id)
        {
             _db.Customers.Add(new Customer() { CustomerId = id });
             _db.SaveChanges(); 
        }

    }
}
