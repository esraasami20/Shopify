using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Models;
using Microsoft.AspNetCore.Identity;
using Shopify.Repository;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private ShopifyContext _shopifyContext;
        private readonly UserManager<ApplicationUser> _applicationUser;
        private readonly SellerService _sellerRepo;

        public SellerController(ShopifyContext shopifyContext ,UserManager<ApplicationUser> applicationUser,SellerService sellerRepo )
        {
            _shopifyContext = shopifyContext;
            _applicationUser = applicationUser;
            _sellerRepo = sellerRepo;
        }

        //get sellers data
        [HttpGet]
        public ActionResult<List<ApplicationUser>> GetAll()
        {
            return _sellerRepo.GetAllSellers();
        }
        //get seller by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSellerById(string id)
        {
            var seller = await _shopifyContext.Sellers.Include( "ApplicationUser" ).FirstOrDefaultAsync(s=>s.SellerId==id);
            if (seller == null)
            {
                return NotFound();
            }
            return seller;
        }
        //edit seller
        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationUser>> EditSellerAsync(string id, [FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                user.Id = id;
                var result = await _sellerRepo.PutSeller( id,  user);
                if (result != null)
                    return NoContent();
                return NotFound();
            }

        }
        // delete seller
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(string id)
        {
            ApplicationUser app = new ApplicationUser();
            var seller = await _shopifyContext.Sellers.FindAsync(id);
            var sellerInUser = await _shopifyContext.Users.FindAsync(seller.SellerId);
            if (seller == null)
            {
                return NotFound();
            }
            if(sellerInUser != null)
            {
                sellerInUser.AdminLocked = true;
                seller.Isdeleted = true;
                await _shopifyContext.SaveChangesAsync();
            }        
            return NoContent();
        }



       
        
    }
}
