using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopify.Helper;
using Shopify.Models;
using Shopify.Repository;
using Shopify.Repository.Interfaces;
using Shopify.Services.Interfaces;
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






        //// add product details

        //[HttpPost("add-details/{productId}/{InventoryId}")]
        //[Authorize(Roles = "Seller")]

        //public ActionResult AddProductDetails(int productId, int InventoryId, ProductDetail[] productDetails)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = _productRepo.AddProdctDetails(productId, InventoryId, productDetails, User.Identity);
        //        if (result.Status == "Success")
        //            return Ok(new Response { Status = "Success", Message = "Dtails Added successfully" });
        //        return NotFound();
        //    }
        //    return BadRequest(ModelState);

        //}







        // get products pagination

        [HttpGet("{subCategoryId}")]
        public async Task<ActionResult> GetAllAsync(int subCategoryId , [FromQuery] PaginationFilter filter)
        {

            var result = await _productRepo.GetProductsAsync(subCategoryId, filter, Request);
            return Ok(result);
        }



        // search 

        [HttpGet("search")]
        public  ActionResult Search([FromQuery] string name)
        {

            var result =  _productRepo.SearchProduct(name);
            return Ok(result);
        }


        // get product by id

        [HttpGet("Details/{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var result = _productRepo.GetProductById(id);
             if(result!=null)
                return Ok(result);
            return NotFound();
        }






        //edit product
        //[HttpPut("{id}")]
        //public ActionResult EditProduct(int id, [FromBody] Product product)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        product.ProductId = id;
        //        var result = _productRepo.EditProductAsync(product);
        //        if (result != null)
        //            return NoContent();
        //        return NotFound();
        //    }

        //}



        // edit Product data
        [Authorize(Roles = "Seller")]
        [HttpPut("{id}")]
        public ActionResult EditProduct(int id, [FromBody] Product product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                product.ProductId = id;
                var result = _productRepo.EditProductDataAsync(product , User.Identity);
                if (result.Status == "Error"|| result.Status == "Error" && result.data ==null)
                     return NotFound();
                    return NoContent();
               
            }

        }



        // edit Product images
        [Authorize(Roles = "Seller")]
        [HttpPut("images/{id}")]
        public async Task<ActionResult> EditProductImagesAsync(int id, [FromForm] IFormFile [] files)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
               
                var result =await _productRepo.EditProductImagesAsync(id,files, User.Identity);
                if (result.Status == "Error" || result.Status == "Error" && result.data == null)
                    return NotFound();
                return NoContent();

            }

        }



        // delete product
        [Authorize(Roles ="Seller")]
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var result = _productRepo.DeleteProduct(id);
            if (result)
                return NoContent();
            return NotFound();
        }





        // get top seales
        [HttpGet("top-seales")]
        public ActionResult<List<Product>> GetTopSeales()
        {
            List<Product> topProducts = _productRepo.GetTopSeales();
            return Ok(topProducts);
        }



        // get seller products in his inventory
        [HttpGet("seller")]
        public ActionResult GetSellerProducts()
        {
            List<List<Product>> data = _productRepo.GetSellerProducts(User.Identity);
            return Ok(data);
        }


    }
}
