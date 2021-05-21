using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // get all Seller
        public List<ApplicationUser> GetAllSellers()
        {
            List<ApplicationUser> sellerData = new List<ApplicationUser>();
            var sellerId = _db.Sellers.ToList();

            foreach (var item in sellerId)
            {
                var seller = _db.Users.Where(c => c.AdminLocked == false).FirstOrDefault(a => a.Id == item.SellerId);

                sellerData.Add(seller);
            }
            return sellerData;

        }
        //edit seller
        public async Task<ApplicationUser> PutSeller(string id, [FromBody] ApplicationUser user)
        {
            var seller = await _db.Sellers.FirstOrDefaultAsync(s => s.SellerId == id);

            var userSeller =  _db.Users.FirstOrDefault(s => s.Id == seller.SellerId);
            userSeller.Fname = user.Fname;
            userSeller.Lname = user.Lname;
            userSeller.Gender = user.Gender;
            userSeller.Email = user.Email;
            userSeller.UserName = user.UserName;
            userSeller.Address = user.Address;
            userSeller.Age = user.Age;
            await _db.SaveChangesAsync();
            return userSeller;
        }
    }
}
