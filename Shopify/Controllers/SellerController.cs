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
        private readonly ShopifyContext _shopifyContext;
        private readonly UserManager<ApplicationUser> _applicationUser;

        public SellerController(ShopifyContext shopifyContext ,UserManager<ApplicationUser> applicationUser)
        {
            _shopifyContext = shopifyContext;
            _applicationUser = applicationUser;
        }
        
        //get sellers data
        [HttpGet("all-seller")]
        public async Task<ActionResult<IEnumerable<Seller>>> GetAlSellers()
        {
            var sellerId = _applicationUser.Users.FirstOrDefault().Id;
            return await _shopifyContext.Sellers.Where(w=>w.SellerId == sellerId).ToListAsync();          

        }
        //get seller by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSellerById(string id)
        {

            var ID = await _shopifyContext.Sellers.FindAsync(id);

            if (ID == null)
            {
                return NotFound();
            }

            return ID;
        }

    }
}
