using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Repository.Interfaces;
using Shopify.Repository;
using Shopify.Models;
using Microsoft.AspNetCore.Authorization;

namespace Shopify.Controllers
{
    [Authorize(Roles = "Seller")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        
       
        private InventoryServices _inventoryRepo;
        public InventoryController(InventoryServices inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
          
        }


       
        // get all inventories for seller
        [HttpGet]
        public ActionResult<List<Inventory>> GetAll()
        {
            return _inventoryRepo.GetAllInventories(User.Identity);
        }




        // get Inventory by id
        [HttpGet("{id}")]
        public ActionResult<Inventory> GetCategory(int id)
        {
            var result = _inventoryRepo.GetInventoryById(id , User.Identity);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        // add Inventory
        
        [HttpPost]
        public async Task<ActionResult<Inventory>> AddInventory(Inventory inventory)
        {
            if (ModelState.IsValid)
            {

                 var result = await _inventoryRepo.AddInventory(inventory , User.Identity);
                if (result != null)
                    return Ok(inventory);
                return   StatusCode(StatusCodes.Status500InternalServerError, "Try again");
            }
            return BadRequest(ModelState);
        }



        //edit Inventory
        [HttpPut("{id}")]
        public async Task<ActionResult<Inventory>> AddInventoryAsync(int id, [FromBody] Inventory inventory)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(inventory);
            }
            else
            {
                inventory.InventoryId = id;
                var result =_inventoryRepo.EditInventoryAsync(inventory , User.Identity);
                if (result != null)
                    return NoContent();
                return NotFound();
            }

        }


        //delete inventory
       [HttpDelete("{id}")]
        public ActionResult<Inventory> deleteInventory(int id)
        {
            var result = _inventoryRepo.DeleteInventory(id ,User.Identity);
            if (result != null)
                return NoContent();
            return NotFound();
        }

    }
}
