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
            return _db.Reviews.Where(r=>r.Isdeleted==false).ToList();
        }


        //get all reviews for spicific product
        public Response GetReviewForSpicificProduct(int id)
        {
            var product = _db.Products.FirstOrDefault(a => a.ProductId == id && a.IsdeletedBySeller==false);
            if (product != null)
                return new Response { Status = "Success", data = _db.Reviews.Where(a => a.ProductId == product.ProductId && a.Isdeleted == false).ToList() };
            else
                return new Response {Status="Error" , Message="Product Not Found" };
        }


        // get all review for spicific customer
        public Response GetReviewForCustomer(int id, IIdentity customer)
        {
            var customerId = HelperMethods.GetAuthnticatedUserId(customer);
            var product = _db.Products.FirstOrDefault(a => a.ProductId == id && a.IsdeletedBySeller == false);
            if (product != null)
                return new Response { Status = "Success", data = _db.Reviews.Where(a => a.ProductId == id && a.CustomerId == customerId && a.Isdeleted == false).ToList() };
            else
                return new Response { Status = "Error", Message = "Product Not Found" };
        }



        //Edit Review
        public bool EditReview(IIdentity customer, Review Newreview)
        {
            var customerId = HelperMethods.GetAuthnticatedUserId(customer);
            Review review = _db.Reviews.FirstOrDefault(a => a.ProductId ==Newreview.ProductId && a.CustomerId == customerId && a.Isdeleted == false);
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
        public Review AddNewReview(IIdentity customer,Review review)
        {
            var customerId = HelperMethods.GetAuthnticatedUserId(customer);
            var product = _db.Products.FirstOrDefault(a => a.ProductId == review.ProductId);
            if (product != null)
            {
                    _db.Reviews.Add(review);
                    _db.SaveChanges();
                    return review;
                
            }
            return null;
        }




        //delete review 
        public bool DeleteReview(int id , IIdentity customer)
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
