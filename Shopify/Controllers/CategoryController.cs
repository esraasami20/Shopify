using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.Models;
using Shopify.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Controllers
{
     [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryRepo _categoryRepo;

        public CategoryController(CategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        // get all categories
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Category>> GetAll()
        {
            return _categoryRepo.GetAllCategories();
        }
        // get gategory by id
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Category> GetCategory(int id)
        {
            var result = _categoryRepo.GetCategory(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        // add category
        [HttpPost]
       
        public async Task<ActionResult<Category>> AddCategoryAsync([FromForm]Category category, [FromForm] IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Category result = await _categoryRepo.AddCategory(category, file);
                
                return Ok(result);
            }
           
        }


        //edit category
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> AddCategoryAsync(int id,[FromForm] Category category, [FromForm] IFormFile file)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                category.CategoryId = id;
                var result = await _categoryRepo.EditCategoryAsync(category, file);
                if (result!=null)
                return NoContent();
               return NotFound();
            }

        }



        // delete category
        [HttpDelete("{id}")]
        public ActionResult<Category> deleteCategory(int id)
        {

          var result =  _categoryRepo.DeleteCategory(id);
            if(result!=null)
               return NoContent();
            return NotFound();
        }





    }
}
