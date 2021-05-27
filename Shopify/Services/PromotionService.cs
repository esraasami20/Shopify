using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shopify.Helper;
using Shopify.Models;
using System.IO;
using System.Security.Principal;

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
        public Promotions addPromotion(Promotions promotion, IIdentity seller)
        {
              var sellerId =  HelperMethods.GetAuthnticatedUserId(seller);
              promotion.SellerId = sellerId;
             _db.Promotions.Add(promotion);
             _db.SaveChanges();
             
            return promotion;
        }

        public Product AddPromotionToProduct(int PormotionId, int productId, IIdentity seller)
        {

            var sellerId = HelperMethods.GetAuthnticatedUserId(seller);
            var promotion = _db.Promotions.FirstOrDefault(p => p.PromotionsId == PormotionId && p.SellerId ==sellerId && p.Isdeleted == false);

            if (promotion != null)
            {
                Product Product = _db.Products.FirstOrDefault(p => p.ProductId == productId);
                Product.PromotionId = PormotionId;
                _db.SaveChanges();
                return Product;
            }
            return null;
        }

        public bool EditPromotionAsync(Promotions promotion , IIdentity seller)
        {
            var sellerId = HelperMethods.GetAuthnticatedUserId(seller);
            Promotions promotion1  = _db.Promotions.FirstOrDefault(p => p.PromotionsId == promotion.PromotionsId && p.SellerId == sellerId && p.Isdeleted == false);

            if (promotion1 != null)
            {
                promotion1.Description = promotion.Description;
                promotion1.Discount = promotion.Discount;
                _db.SaveChanges();
                return true;
            }
        
            return false;
        }

        //  Edit promotion
        //public async Task<Promotions> EditPromotionAsync(Promotions promotion, IFormFile file)
        //{
        //    Promotions promotionDetails = GetPromotion(promotion.PromotionsId);
        //    if (promotionDetails != null)
        //    {
        //        if (file != null)
        //        {

        //            // delete old image

        //            File.Delete(promotionDetails.Image);

        //            // create new image
        //            string imagepath = await FileHelper.SaveImageAsync(promotionDetails.PromotionsId, file, "Promotions");
        //            promotionDetails.Image = imagepath;
        //        }
        //        promotionDetails.Discount = promotion.Discount;
        //        promotionDetails.Description = promotion.Description;
        //        promotionDetails.SellerId = promotion.SellerId;

        //        _db.SaveChanges();
        //    }
        //    return promotionDetails;
        //}

        //  delete category
        public bool Deletepromotion(int id , IIdentity seller)
        {


            var sellerId = HelperMethods.GetAuthnticatedUserId(seller);
            Promotions promotion1 = _db.Promotions.FirstOrDefault(p => p.PromotionsId == id && p.SellerId == sellerId && p.Isdeleted == false);

            if (promotion1 != null)
            {
                promotion1.Isdeleted = true;
                _db.SaveChanges();
                return true;
            }

            return false;

        }
    }
}
