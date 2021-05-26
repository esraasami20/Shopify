using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Models;
using Shopify.Helper;
using Shopify.Repository.Interfaces;

namespace Shopify.Controllers
{
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
        public async Task<ActionResult<Promotions>> AddCategoryAsync([FromForm] Promotions promotion, [FromForm] IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Promotions result = await _promotionRepo.addPromotion(promotion, file);

                return Ok(result);
            }

        }


        //edit Promotions
        [HttpPut("{id}")]
        public async Task<ActionResult<Promotions>> AddCategoryAsync(int id, [FromForm] Promotions promotion, [FromForm] IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                promotion.PromotionsId = id;
                var result = await _promotionRepo.EditPromotionAsync(promotion, file);
                if (result != null)
                    return NoContent();
                return NotFound();
            }

        }



        // delete promotion
        [HttpDelete("{id}")]
        public ActionResult<Promotions> deletePromotion(int id)
        {


            var result = _promotionRepo.Deletepromotion(id);
            if (result != null)
                return NoContent();
            return NotFound();
        }

    }
}
