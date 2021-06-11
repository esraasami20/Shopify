using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.Models;
using Shopify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Shopify.Controllers
{
    [Authorize(Roles = "Seller")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        //get all reviews
        [HttpGet]
        public ActionResult<List<Review>> getAll()
        {
            return Ok(_reviewService.GetReviews());
        }
        //get all reviews for spicific product
        [HttpGet("{id}")]
        public ActionResult<List<Review>> getspicificReview(int id)
        {
            return Ok(_reviewService.GetReviewForSpicificProduct(id));
        }
        // get all review for spicific customer
        [HttpGet]
        [Route("customer-review/{id}")]
        public ActionResult<List<Review>> getcustumersReview(int id)
        {
            return Ok(_reviewService.GetReviewForCustomer(id, User.Identity));
        }

        //Edit Review
        [HttpPut("{id}")]
        public async Task<ActionResult<Review>> editReview(int id, [FromBody] Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _reviewService.editReview(id, User.Identity, review);
                if (result)
                    return NoContent();
                return NotFound();
            }
        }
        //add review
        [HttpPost("{ProductId}")]
        public ActionResult AddReviewToProduct(int ProductId, [FromBody] Review review)
        {
            var result = _reviewService.addNewReview(ProductId, User.Identity,review);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        // delete review
        [HttpDelete("{id}")]
        public ActionResult deletereview(int id)
        {

            var result = _reviewService.deleteReview(id, User.Identity);
            if (result)
                return NoContent();
            return NotFound();
        }




    }
}
