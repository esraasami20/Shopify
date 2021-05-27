using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopify.Helper;
using Shopify.Models;
using Shopify.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;


namespace Shopify.Repository.Interfaces
{
    public class ProductService
    {


        ShopifyContext _db;
        private readonly IUriService _uriService;
        public ProductService(ShopifyContext db , IUriService uriService)
        {
            _db = db;
            _uriService = uriService;

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

                    _db.InventoryProducts.Add(new InventoryProduct { InventoryId = inventoryId, ProductId = product.ProductId  ,Quantity =product.Quantity});
                    _db.SaveChanges();
                    return new Response { Status = "Success", Message = "product added successfully" , data = product };
                }
                return new Response { Status = "Error", Message = "Inventory not found"};
        }

           public Product GetProductById(int id)
           {
             return _db.Products.Include(rr=>rr.Reviews).Include(r=>r.ProductDetails).Include(i=>i.ProductImages).FirstOrDefault(p=>p.ProductId == id);
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

        public bool DeleteProduct(int id)
        {
           Product product = _db.Products.FirstOrDefault(p => p.ProductId == id && p.IsdeletedBySeller==false);
            product.IsdeletedBySeller = true;
            _db.SaveChanges();
            return true;
        }






        // get products patination


        public async Task<PagedPagination<List<Product>>> GetProductsAsync(int subCategoryId, PaginationFilter filter, HttpRequest request)
        {
            var route = request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _db.Products.Include(i => i.ProductImages).Where(s => s.SubCategotyId == subCategoryId && s.IsdeletedBySeller==false)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _db.Products.Where(s => s.SubCategotyId == subCategoryId).CountAsync();
           return PaginationHelper.CreatePagedReponse<Product>(pagedData, validFilter, totalRecords, _uriService, route);
        }



        public  List<Product> SearchProduct(string name)
        {
          return _db.Products.Include(i=>i.ProductImages).Where(p => p.ProductName.Contains(name) && p.IsdeletedBySeller ==false && p.IsdeletedBySpoify == false).ToList();
        }




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
