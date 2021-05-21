using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopify.Helper;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

        // get all Customer
        public List<ApplicationUser> GetAllCustomers()
        {
            List<ApplicationUser> customerData = new List<ApplicationUser>();
            var customerId = _db.Customers.ToList();

            foreach (var item in customerId)
            {
                var seller = _db.Users.Where(c => c.AdminLocked == false).FirstOrDefault(a => a.Id == item.CustomerId);

                customerData.Add(seller);
            }
            return customerData;

        }
        //edit Customer
        public async Task<ApplicationUser> editCustomer(string id, [FromBody] ApplicationUser user)
        {
            var customer =  _db.Customers.FirstOrDefault(s => s.CustomerId == id);

            var userCustomer = _db.Users.FirstOrDefault(s => s.Id == customer.CustomerId);
            userCustomer.Fname = user.Fname;
            userCustomer.Lname = user.Lname;
            userCustomer.Gender = user.Gender;
            userCustomer.Email = user.Email;
            userCustomer.UserName = user.UserName;
            userCustomer.Address = user.Address;
            userCustomer.Age = user.Age;
            await _db.SaveChangesAsync();
            return userCustomer;
        }




        


    }
}
