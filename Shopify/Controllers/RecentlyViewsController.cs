using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecentlyViewsController : ControllerBase
    {
        private readonly RecentlyViewRepo _recentlyViewRepo;

        public  RecentlyViewsController( RecentlyViewRepo recentlyViewRepo)
        {
            _recentlyViewRepo = recentlyViewRepo;
        }

        // add  recently views to customer
        [Authorize]
        [HttpPost("add-recently-view/{id}")]
        public ActionResult AddrecentlyView(int id)
        {
            var result = _recentlyViewRepo.AddRecentlyViewToCustomer(id, User.Identity);
            if (result.Status == "Success")
            {
                return Ok();
            }
            else if (result.Status == "Found Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            else
            {
                return NotFound(result);
            }
        }




        // get customer recently view

        [Authorize]
        [HttpGet("get-recently-view")]
        public ActionResult GetRecentlyView()
        {
            var result = _recentlyViewRepo.GetRecentlyViewFoCustomer(User.Identity);
            return Ok(result);
        }




        // add product to favourite
        [Authorize]
        [HttpPost("add-to-favourite/{id}")]
        public ActionResult AddToFavoutite(int id)
        {
            var result = _recentlyViewRepo.AddToFavourite(id, User.Identity);
            if (result.Status == "Success")
                return Ok(result);
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }


        // add product to favourite
        [Authorize]
        [HttpDelete("remove-from-favourite/{id}")]
        public ActionResult RemoveFromFavoutite(int id)
        {
            var result = _recentlyViewRepo.RemoveFromFavourite(id, User.Identity);
            if (result.Status == "Success")
                return Ok(result);
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }


        // get customer favourites 
        [Authorize]
        [HttpGet("favourites")]
        public ActionResult GetCustomerFavoutites()
        {
            var result = _recentlyViewRepo.GetCustomerFavoutites(User.Identity);
            return Ok(result);
        }




    }
}
