using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Models;
using Microsoft.AspNetCore.Identity;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private ShopifyContext _shopifyContext;
        private readonly UserManager<ApplicationUser> _applicationUser;

        public SellerController(ShopifyContext shopifyContext ,UserManager<ApplicationUser> applicationUser)
        {
            _shopifyContext = shopifyContext;
            _applicationUser = applicationUser;
        }
        
        //get sellers data
        [HttpGet("all-seller")]
        public  List<ApplicationUser>  GetAlSellers()
        {
            List<ApplicationUser> sellerData = new List<ApplicationUser>();
            var sellerId = _shopifyContext.Sellers.Where(c => c.Isdeleted == false).ToList();

            foreach (var item in sellerId)
            {
                var seller = _applicationUser.Users.FirstOrDefault(a => a.Id == item.SellerId);
                
                sellerData.Add(seller);
            }
            return  sellerData;

        }
        //get seller by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSellerById(string id)
        {
            var seller = await _shopifyContext.Sellers.Include("ApplicationUser").FirstOrDefaultAsync(s=>s.SellerId==id);
            if (seller == null)
            {
                return NotFound();
            }
            return seller;
        }
        //edit seller
        [HttpPut("{id}")]
        public async Task<IActionResult> EditSeller(string id,ApplicationUser seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }
            _shopifyContext.Entry(seller).State = EntityState.Modified;
            //_shopifyContext.Users.Find(). = EntityState.Modified;

            try
            {
                await _shopifyContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (id != null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // delete seller
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(string id)
        {
            ApplicationUser app = new ApplicationUser();
            var seller = await _shopifyContext.Sellers.FindAsync(id);
            var seller1 = await _shopifyContext.Users.Where(a=>a.Id== id).Select(a=>a.);
            var sellerInUser = await _shopifyContext.Users.FindAsync(seller.SellerId);
            if (seller == null)
            {
                return NotFound();
            }
            if(sellerInUser != null)
            {
        
                seller.Isdeleted = true;
                await _shopifyContext.SaveChangesAsync();
            }        
            return NoContent();
        }
       

        //private bool SellerExists(string id)
        //{
        //    return _shopifyContext.Sellers.Any(e => e.SellerId == id);
        //}
    }
}
