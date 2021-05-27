using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Models;
using Shopify.Helper;
using Shopify.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Shopify.Controllers
{ 
    [Authorize(Roles ="Seller")]
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private PromotionService _promotionRepo;

        public PromotionController(PromotionService promotionRepo)
        {
            _promotionRepo = promotionRepo;
        }
        [HttpGet]
        public ActionResult<List<Promotions>> GetAll()
        {
            return _promotionRepo.GetAllPromotions();
        }
        //get promotion by id
        [HttpGet("{id}")]
        public ActionResult<Promotions> GetPromotion(int id)
        {
            var result = _promotionRepo.GetPromotion(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }




        // add promotion
        [HttpPost]
        public ActionResult<Promotions> AddCategoryAsync([FromBody] Promotions promotion)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Promotions result =  _promotionRepo.addPromotion(promotion, User.Identity);

                return Ok(result);
            }

        }


        // add pormotion to product 
        [HttpPost("a/{PormotionId}")]
        public ActionResult AddPromotionToProduct(int PormotionId, [FromBody]int ProductId)
        {
            var result = _promotionRepo.AddPromotionToProduct(PormotionId, ProductId, User.Identity);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }



        //edit Promotions
        [HttpPut("{id}")]
        public  ActionResult AddCategoryAsync(int id, [FromBody] Promotions promotion)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                promotion.PromotionsId = id;
                var result =  _promotionRepo.EditPromotionAsync(promotion , User.Identity);
                if (result)
                    return NoContent();
                return NotFound();
            }

        }



        // delete promotion
        [HttpDelete("{id}")]
        public ActionResult deletePromotion(int id)
        {

            var result = _promotionRepo.Deletepromotion(id , User.Identity);
            if (result)
                return NoContent();
            return NotFound();
        }

    }
}
