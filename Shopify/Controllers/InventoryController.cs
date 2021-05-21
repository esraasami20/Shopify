using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Repository.Interfaces;
using Shopify.Repository;
using Shopify.Models;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        
        private readonly ShopifyContext _shopifyContext;
        private InventoryRepo _inventoryRepo;
        public InventoryController(InventoryRepo inventoryRepo, ShopifyContext shopifyContext)
        {
            _inventoryRepo = inventoryRepo;
            _shopifyContext = shopifyContext;
        }
       
        // get all inventories
        [HttpGet]
        public ActionResult<List<Inventory>> GetAll()
        {
            return _inventoryRepo.GetAllInventories();
        }
        // get Inventory by id
        [HttpGet("{id}")]
        public ActionResult<Inventory> GetCategory(int id)
        {
            var result = _inventoryRepo.GetInventoryById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        // add Inventory
        [HttpPost]
        public async Task<Inventory> AddInventory(Inventory inventory)
        {
            if (inventory != null)
            {
                _shopifyContext.Inventories.Add(inventory);
                _shopifyContext.SaveChanges();
            }
            return inventory;
        }


        //edit Inventory
        [HttpPut("{id}")]
        public async Task<ActionResult<Inventory>> AddInventoryAsync(int id, [FromForm] Inventory inventory)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                inventory.InventoryId = id;
                var result = await _inventoryRepo.EditInventoryAsync(inventory,id);
                if (result != null)
                    return NoContent();
                return NotFound();
            }

        }



        // delete inventory
        [HttpDelete("{id}")]
        public ActionResult<Inventory> deleteInventory(int id)
        {


            var result = _inventoryRepo.DeleteInventory(id);
            if (result != null)
                return NoContent();
            return NotFound();
        }

    }
}
