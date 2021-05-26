using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopify.Helper;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
        public List<Inventory> GetAllInventories(IIdentity seller)
        {
              var sellerId  = HelperMethods.GetAuthnticatedUserId(seller);
              return _db.Sellers.Include(i=>i.Inventories).FirstOrDefault(s=>s.SellerId == sellerId).Inventories;
        }

        // get inventory by id
        public Inventory GetInventoryById(int id)
        {
            Inventory inventory = _db.Inventories.Include("Seller").SingleOrDefault(c => c.InventoryId == id && c.Isdeleted == false);
            return inventory;
        }
        // add   inventory
        public async Task<Inventory> AddInventory(Inventory inventory, IIdentity seller)
        {
            string sellerId = HelperMethods.GetAuthnticatedUserId(seller);
            if (inventory != null)
            {
                inventory.sellerId = sellerId;
                _db.Inventories.Add(inventory);
                _db.SaveChanges();
            }
            return inventory;
        }


        //  Edit inventory
        public async Task<Inventory> EditInventoryAsync(Inventory inventory, int id)
        {
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

        //  delete inventory
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
