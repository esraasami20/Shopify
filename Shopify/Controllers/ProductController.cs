using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.Models;
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
        ProductRepo _productRepo;

        public ProductController(ProductRepo productRepo)
        {
            _productRepo = productRepo;
        }


        // get brands for sub category
        [HttpGet("pro/{id}")]
        public ActionResult<List<Product>> GetAllproductsBrand(int id)
        {
            var result = _productRepo.getBrandsForProduct(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }



        // get brand by id
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var result = _productRepo.GetProduct(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }



        // add product
        [HttpPost]
        public ActionResult<Brand> AddProduct([FromBody] Product product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Product result = _productRepo.AddProduct(product);

                return Ok(result);
            }

        }


        //edit brand
        [HttpPut("{id}")]
        public ActionResult EditProduct(int id, [FromBody] Product product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                product.ProductId = id;
                var result = _productRepo.EditProductAsync(product);
                if (result != null)
                    return NoContent();
                return NotFound();
            }

        }



        // delete category
        [HttpDelete("{id}")]
        public ActionResult deleteProduct(int id)
        {


            var result = _productRepo.DeleteProduct(id);
            if (result != null)
                return NoContent();
            return NotFound();
        }

    }
}
