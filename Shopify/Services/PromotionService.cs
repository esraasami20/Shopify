using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shopify.Helper;
using Shopify.Models;
using System.IO;

namespace Shopify.Repository.Interfaces
{
    public class PromotionService
    {
        ShopifyContext _db;
        public PromotionService(ShopifyContext db)
        {
            _db = db;
        }

        // get all promotions
        public List<Promotions> GetAllPromotions()
        {
            return _db.Promotions.Where(c => c.Isdeleted == false).ToList();
        }

        // get promotions by id
        public Promotions GetPromotion(int id)
        {
            Promotions promotion = _db.Promotions.Include("Seller").SingleOrDefault(c => c.PromotionsId == id && c.Isdeleted == false);
            return promotion;
        }

        // add Promotions
        public async Task<Promotions> addPromotion(Promotions promotion, IFormFile file)
        {
            _db.Promotions.Add(promotion);
            _db.SaveChanges();
            if (file != null)
            {
                string imagepath = await FileHelper.SaveImageAsync(promotion.PromotionsId, file, "Promotions");
                promotion.Image = imagepath;
                _db.SaveChanges();
            }
            return promotion;
        }
        //  Edit promotion
        public async Task<Promotions> EditPromotionAsync(Promotions promotion, IFormFile file)
        {
            Promotions promotionDetails = GetPromotion(promotion.PromotionsId);
            if (promotionDetails != null)
            {
                if (file != null)
                {

                    // delete old image

                    File.Delete(promotionDetails.Image);

                    // create new image
                    string imagepath = await FileHelper.SaveImageAsync(promotionDetails.PromotionsId, file, "Promotions");
                    promotionDetails.Image = imagepath;
                }
                promotionDetails.Discount = promotion.Discount;
                promotionDetails.Description = promotion.Description;
                promotionDetails.SellerId = promotion.SellerId;

                _db.SaveChanges();
            }
            return promotionDetails;
        }

        //  delete category
        public Promotions Deletepromotion(int id)
        {
            Promotions promotion = GetPromotion(id);
            if (promotion != null)
            {
                promotion.Isdeleted = true;
                _db.SaveChanges();

            }
            return promotion;
        }
    }
}
