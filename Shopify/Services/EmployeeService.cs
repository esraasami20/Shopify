using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Repository
{
    public class EmployeeService
    {
        ShopifyContext _db;
        public EmployeeService(ShopifyContext db)
        {
            _db = db;
        }

        public void AddEmployeeId(string id)
        {
            _db.Employees.Add(new Employee() { EmployeeId = id});
            _db.SaveChanges();
        }

    }
}
