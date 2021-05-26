using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.Models;
using Shopify.Repository;
using Shopify.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductService _productRepo;
        ShopifyContext _shopifyContext;

        public ProductController(ProductService productRepo,ShopifyContext shopifyContext)
        {
            _productRepo = productRepo;
            _shopifyContext = shopifyContext;
        }




        // add product
        [Authorize(Roles ="Seller")]
        [HttpPost("{inventoryId}")]
        public async Task<ActionResult> AddProductAsync( [FromForm]Product product ,[FromForm] IFormFile [] files , int inventoryId)
        {
            if (ModelState.IsValid)
            {
                var result = await _productRepo.AddProdctAsync(product , inventoryId, files, User.Identity);
                if (result.Status== "Success")

                    return Ok(result);
                return BadRequest(result.Message);
            }
            else
            {
                return BadRequest();
            }
            
        }






        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        //{
        //    var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        //    var pagedData =  _shopifyContext.Products
        //        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
        //        .Take(validFilter.PageSize)
        //        .ToList();
        //    var totalRecords =  _shopifyContext.Products.Count();
        //    return Ok(new PagedPagination<List<Product>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        //}

        //// get product by id
        //[HttpGet("{id}")]
        //public ActionResult<Product> GetProduct(int id)
        //{
        //    var result = _productRepo.GetProduct(id);
        //    if (result == null)
        //        return NotFound();
        //    return Ok(result);
        //}
        //// add product
        //[HttpPost]
        //public ActionResult<Brand> AddProduct([FromBody] Product product)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        Product result = _productRepo.AddProduct(product);

        //        return Ok(result);
        //    }

        //}
        ////edit product
        ////[HttpPut("{id}")]
        ////public ActionResult EditProduct(int id, [FromBody] Product product)
        ////{

        ////    if (!ModelState.IsValid)
        ////    {
        ////        return BadRequest();
        ////    }
        ////    else
        ////    {
        ////        product.ProductId = id;
        ////        var result = _productRepo.EditProductAsync(product);
        ////        if (result != null)
        ////            return NoContent();
        ////        return NotFound();
        ////    }

        ////}



        //// delete product
        //[HttpDelete("{id}")]
        //public ActionResult deleteProduct(int id)
        //{


        //    var result = _productRepo.DeleteProduct(id);
        //    if (result != null)
        //        return NoContent();
        //    return NotFound();
        //}

    }
}
