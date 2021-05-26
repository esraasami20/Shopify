﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.Models;
using Shopify.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {



        BrandRepo _brandRepo;

        public BrandController(BrandRepo brandRepo)
        {
            _brandRepo = brandRepo;
        }


        // get brands for sub category
        [HttpGet("subCat/{id}")]
        public ActionResult<List<Brand>> GetAllBrandForSubCat(int id)
        {
           var result=  _brandRepo.getBrandsForSubCategory(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }



        // get brand by id
        [HttpGet("{id}")]
        public ActionResult<Brand> GetBrand(int id)
        {
            var result = _brandRepo.GetBrand(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }



        // add brand
        [HttpPost]
        public ActionResult<Brand> AddCategory([FromBody] Brand brand)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Brand result =  _brandRepo.AddBrand(brand);

                return Ok(result);
            }

        }


        //edit brand
        [HttpPut("{id}")]
        public ActionResult EditBrand(int id, [FromBody] Brand brand)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                brand.BrandId = id;
                var result = _brandRepo.EditBrandAsync(brand);
                if (result != null)
                    return NoContent();
                return NotFound();
            }

        }



        // delete category
        [HttpDelete("{id}")]
        public ActionResult deleteCategory(int id)
        {


            var result = _brandRepo.DeleteBrand(id);
            if (result != null)
                return NoContent();
            return NotFound();
        }







    }
}
