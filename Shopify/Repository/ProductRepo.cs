using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopify.Helper;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;


namespace Shopify.Repository.Interfaces
{
    public class ProductRepo
    {


        ShopifyContext _db;
        public ProductRepo(ShopifyContext db)
        {
            _db = db;
        }















        // add product 

        public async Task<Response> AddProdctAsync(Product product ,  int inventoryId, IFormFile [] files , IIdentity seller)
        {
            string sellerId = HelperMethods.GetAuthnticatedUserId(seller);
          
              var result =  _db.Inventories.FirstOrDefault(i => i.InventoryId == inventoryId && i.sellerId == sellerId);
                if (result != null)
                {
                   _db.Products.Add(product);
                   _db.SaveChanges();

             


                // add images of product 
                if (files != null)
                    {
                        var imagePaths = await FileHelper.SaveImagesAsync(product.ProductId, files, "Products");
                      
                        for (int i = 0; i < imagePaths.Count; i++)
                        {
                         _db.ProductImages.Add(new ProductImages { Image =imagePaths[i], ProductId = product.ProductId });
                        }
 
                      
                    }

                // add to inventory

                    _db.InventoryProducts.Add(new InventoryProduct { InventoryId = inventoryId, ProductId = product.ProductId });
                    _db.SaveChanges();
                    return new Response { Status = "Success", Message = "product added successfully" , data = product };
                }
                return new Response { Status = "Error", Message = "Inventory not found"};
        }





        // add product details
        public Response AddProdctDetails(int productid,int inventoryId, ProductDetail[] productDetails , IIdentity seller)
        {

              string sellerId = HelperMethods.GetAuthnticatedUserId(seller);
              var sellerInventory = _db.Sellers.Include(i=>i.Inventories).FirstOrDefault(s => s.SellerId == sellerId).Inventories.ToList() ;
              var InventoryProduct = sellerInventory.FirstOrDefault(f => f.InventoryId == inventoryId);
            if (InventoryProduct != null)
            {
                var product = _db.InventoryProducts.FirstOrDefault(r => r.InventoryId == InventoryProduct.InventoryId && r.ProductId == productid);
                if (product != null)
                {
                    if (productDetails != null)
                    {
                        for (int i = 0; i < productDetails.Length; i++)
                        {
                            _db.ProductDetails.Add(new ProductDetail { ProductId = product.ProductId, Detail = productDetails[i].Detail, Title = productDetails[i].Title });
                        }
                    }

                    _db.SaveChanges();
                    return new Response { Status = "Success", Message = "add successfully", data = productDetails };
                }
            }
            return new Response { Status = "Error" };

        }



        //public List<Product> GetAllProduct()
        //{
        //    return _db.Products.Include(x => x.ProductImages)
        //                        .Include(t => t.ProductDetails)
        //                        .Include(b => b.Brand)
        //                        .Include(z => z.subCategory)
        //                        .ThenInclude(q => q.Category)
        //                        .Include(k => k.Promotions)
        //                        .Include(v => v.Reviews)
        //                        .Include(m => m.Views)
        //                        .Where(c => c.Isdeleted == false)
        //                        .ToList();
        //}
        //// get product by id
        //public Product GetProduct(int id)
        //{
        //    return _db.Products.Where(b => b.ProductId == id && 
        //                              b.Isdeleted == false 
        //                              )
        //                        .Include(x => x.ProductImages)
        //                        .Include(t=>t.ProductDetails)
        //                        .Include(b=>b.Brand)
        //                        .ThenInclude(s=>s.SubCategories)
        //                        .Include(z=>z.subCategory)
        //                        .ThenInclude(q=>q.Category)
        //                        .Include(k=>k.Promotions)
        //                        .Include(v=>v.Reviews)
        //                        .Include(m=>m.Views)
        //                        .FirstOrDefault();
        //}

        //// add product 
        //public Product AddProduct(Product product)
        //{
        //    _db.Products.Add(product);
        //    _db.SaveChanges();
        //    return product;
        //}

        ////  edit product
        ////public Product EditProductAsync(Product product)
        ////{
        ////    Product productDetails = GetProduct(product.ProductId);
        ////    if (productDetails != null)
        ////    {
        ////        productDetails.ProductName = product.ProductName;
        ////        productDetails.Price = product.Price;
        ////        productDetails.Discount = product.Discount;
        ////        productDetails.Size = product.Size;
        ////        productDetails.Color = product.Color;
        ////        productDetails.SubCategotyId = product.SubCategotyId;
        ////        productDetails.PromotionId = product.PromotionId;
        ////        _db.SaveChanges();
        ////        return productDetails;
        ////    }
        ////    return null;
        ////}


        ////  delete Product
        //public Product DeleteProduct(int id)
        //{
        //    Product productDetails = GetProduct(id);
        //    if (productDetails != null)
        //    {
        //        productDetails.Isdeleted = true;
        //        _db.SaveChanges();

        //    }
        //    return productDetails;

        //}
    }
}
