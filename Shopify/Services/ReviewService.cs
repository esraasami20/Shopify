using Shopify.Helper;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Shopify.Services
{
    public class ReviewService
    {
        private ShopifyContext _db;

        public ReviewService(ShopifyContext db)
        {
            _db = db;
        }
        //get all reviews
        public List<Review> GetReviews()
        {
            return _db.Reviews.ToList();
        }
        //get all reviews for spicific product
        public List<Review> GetReviewForSpicificProduct(int id)
        {
            var product = _db.Products.FirstOrDefault(a => a.ProductId == id);
            return _db.Reviews.Where(a => a.ProductId == product.ProductId && a.Isdeleted == false).ToList();
        }
        // get all review for spicific customer
        public List<Review> GetReviewForCustomer(int id, IIdentity customer)
        {
            var customerId = HelperMethods.GetAuthnticatedUserId(customer);
           List<Review> review = _db.Reviews.Where(a => a.ProductId == id && a.CustomerId == customerId && a.Isdeleted == false).ToList();
            return review;
        }
        //Edit Review
        public async Task<bool> editReview(int id, IIdentity customer, Review Newreview)
        {
            var customerId = HelperMethods.GetAuthnticatedUserId(customer);
            Review review = _db.Reviews.FirstOrDefault(a => a.ProductId == id && a.CustomerId == customerId && a.Isdeleted == false);
            if(Newreview != null)
            {
                review.review = Newreview.review;
                review.comment = Newreview.comment;
                _db.SaveChanges();
                return true;
            }
            return false;

        }
        //add review
        public Review addNewReview(int id, IIdentity customer,Review Newreview)
        {
            var customerId = HelperMethods.GetAuthnticatedUserId(customer);
            var product = _db.Products.FirstOrDefault(a => a.ProductId == id);
            Review review = _db.Reviews.FirstOrDefault(p => p.ProductId == product.ProductId && p.CustomerId == customerId && p.Isdeleted == false);

            if (Newreview != null && customerId!=null && product != null)
            {
                _db.Reviews.Add(Newreview);
                _db.SaveChanges();
                return review;
            }
            return review;
        }
        //delete review 
        public bool deleteReview(int id , IIdentity customer)
        {
            var customerId = HelperMethods.GetAuthnticatedUserId(customer);
            Review review = _db.Reviews.FirstOrDefault(a => a.ProductId == id && a.CustomerId == customerId && a.Isdeleted == false);
            if(review != null)
            {
                review.Isdeleted = true;
                _db.SaveChanges();
                return true;
            }
            return false;
        }
        

    }
}
