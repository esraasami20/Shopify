using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Models;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private ShopifyContext _shopifyContext;

        public PromotionController(ShopifyContext shopifyContext)
        {
            _shopifyContext = shopifyContext;
        }
        [HttpGet]
        public List<Promotions> GetAllPromotions()
        {
            return _shopifyContext.Promotions.Where(c => c.Isdeleted == false).ToList();
        }
        [HttpGet("{id}")]
        public Promotions GetPromotionById(int id)
        {
            Promotions promotions = _shopifyContext.Promotions.SingleOrDefault(c => c.PromotionsId == id && c.Isdeleted == false);
            return promotions;
        }
        //add promotions
        //[HttpPost]
        //public async Task<Promotions> AddCategory(Category category, IFormFile file)
        //{
        //    _db.Categories.Add(category);
        //    _db.SaveChanges();
        //    if (file != null)
        //    {
        //        string imagepath = await FileHelper.SaveImageAsync(category.CategoryId, file, "Categories");
        //        category.Image = imagepath;
        //        _db.SaveChanges();
        //    }
        //    return category;
        //}
    }
}
