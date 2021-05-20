using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Models;
using Microsoft.AspNetCore.Authorization;
using Shopify.Repository.Interfaces;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private ShopifyContext _shopifyContext;

        public InventoryController(ShopifyContext shopifyContext)
        {
            _shopifyContext = shopifyContext;
        }
        // get all inventory
        [HttpGet]
        public List<Inventory> GetAllInventories()
        {
            return _shopifyContext.Inventories.Include("Seller").Where(c => c.Isdeleted == false).ToList();
        }
        // get gategory by id
        [HttpGet("{id}")]
        public ActionResult<Inventory> GetInventory(int id)
        {
            Inventory inventory = _shopifyContext.Inventories.Include("Seller").SingleOrDefault(c => c.InventoryId == id && c.Isdeleted == false);
            return inventory;
        }
        // add inventory
        [HttpPost]
        public Inventory AddInventory(Inventory inventory)
        {
            if (inventory != null)
            {
                _shopifyContext.Inventories.Add(inventory);
                _shopifyContext.SaveChanges();
            }
            return inventory;
        }

        // edit inventory
        [HttpPut("{id}")]
        public async Task<Inventory> EditInventoryAsync(Inventory inventory, int id)
        {
            var inventoryDetails = await _shopifyContext.Inventories.FindAsync(id);
            if (inventoryDetails != null)
            {
                inventoryDetails.City = inventory.City;
                inventoryDetails.Street = inventory.Street;
                inventoryDetails.BuildingNumber = inventory.BuildingNumber;
                inventoryDetails.sellerId = inventory.sellerId;

                _shopifyContext.SaveChanges();
            }
            return inventoryDetails;
        }

        // delete Inventory
        [HttpDelete("{id}")]
        public Inventory DeleteInventory(int id)
        {
            var inventory = _shopifyContext.Inventories.FirstOrDefault(a=>a.InventoryId==id);
            if (inventory != null)
            {
                inventory.Isdeleted = true;
                _shopifyContext.SaveChanges();

            }
            return inventory;

        }


    }
}
