using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.Helper;
using Shopify.Models;
using Shopify.Repository;
using Shopify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        SubCategoryRepo _subCategoryRepo;

        public SubCategoryController(SubCategoryRepo subCategoryRepo)
        {
            _subCategoryRepo = subCategoryRepo;
        }


        // get sub category by id

        [HttpGet("{id}")]
        public ActionResult<SubCategory> GetSubCategory(int id)
        {
            var result = _subCategoryRepo.GetSubCategory(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        // add category
        [HttpPost("{id}")]
        public async Task<ActionResult<SubCategory>> AddCategoryAsync(int id,[FromForm] SubCategory subCategory, [FromForm] IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                subCategory.CategoryId = id;
                SubCategory result = await _subCategoryRepo.AddSubCategoryAsync(  subCategory, file);
                if (result == null)
                
                    return NotFound(new Response { Status="Error",Message="This Categoty Not Found" });
                
                return Ok(result);
            }

        }


        //edit sub category
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> EditSubCategoryAsync(int id, [FromForm] SubCategory subCategory, [FromForm] IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                subCategory.SubCategoryId= id;
                var result = await _subCategoryRepo.EditSubCategoryAsync(subCategory, file);
                if (result != null)
                    return NoContent();
                return NotFound();
            }

        }



        // delete sub category
        [HttpDelete("{id}")]
        public ActionResult<Category> deleteSubCategory(int id)
        {

            var result = _subCategoryRepo.DeleteSubCategory(id);
            if (result != null)
                return NoContent();
            return NotFound();
        }



        // add brand to sub category
        [HttpPost("add-to-Brand")]
        public ActionResult<Response> AddBrandToSubCategory([FromBody] BrandSubCatViewModel model)
        {
            if (model.BrandId==0&&model.SubCatId==0)
            {
                return BadRequest();
            }
            var result = _subCategoryRepo.AddBrandToSubCategory(model.BrandId,model.SubCatId);
            if (result.Status=="Error")
                return NotFound(result);
            return Ok(result);
        }


    }
}
