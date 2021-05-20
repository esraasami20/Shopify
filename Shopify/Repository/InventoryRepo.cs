using Microsoft.AspNetCore.Mvc;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Repository
{
    public class InventoryRepo
    {
        ShopifyContext _db;
        public InventoryRepo(ShopifyContext db)
        {
            _db = db;
        }
        //get all inventories
        public List<Inventory> GetAllInventories()
        {
            return _db.Inventories.Where(c => c.Isdeleted == false).ToList();
        }

        // get inventory by id
        public Inventory GetInventoryById(int id)
        {
            Inventory inventory = _db.Inventories.SingleOrDefault(c => c.InventoryId == id && c.Isdeleted == false);
            return inventory;
        }
        // add   inventory
        public async Task<Inventory> AddInventory(Inventory inventory)
        {
            if (inventory != null)
            {
                _db.Inventories.Add(inventory);
                _db.SaveChanges();
            }
            return inventory;
        }


        //  Edit Category
        public async Task<Inventory> EditInventoryAsync(Inventory inventory, int id)
        {
            //Inventory inventoryDetails = GetInventory(inventory.InventoryId);
            var inventoryDetails = await _db.Inventories.FindAsync(id);
            if (inventoryDetails != null)
            {
                inventoryDetails.City = inventory.City;
                inventoryDetails.Street = inventory.Street;
                inventoryDetails.BuildingNumber = inventory.BuildingNumber;
                inventoryDetails.sellerId = inventory.sellerId;

                _db.SaveChanges();
            }
            return inventoryDetails;
        }

        //  delete category
        public Inventory DeleteInventory(int id)
        {
            Inventory inventory = GetInventoryById(id);
            if (inventory != null)
            {
                inventory.Isdeleted = true;
                _db.SaveChanges();

            }
            return inventory;

        }
    }
}
