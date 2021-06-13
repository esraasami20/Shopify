using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopify.Helper;
using Shopify.Models;
using Shopify.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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
             return _db.Products.Include(rr=>rr.Reviews).Include(r=>r.ProductDetails).Include(i=>i.ProductImages).Include(b=>b.Brand).FirstOrDefault(p=>p.ProductId == id && p.IsdeletedBySeller==false && p.Active==true);
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

        public Response EditProductDataAsync(Product product , IIdentity seller)
        {
            var sellerId =  HelperMethods.GetAuthnticatedUserId(seller);
            List<Inventory> inventories = _db.Inventories.Include(ip=>ip.InventoryProducts).Where(i => i.sellerId == sellerId && i.Isdeleted==false).ToList();
            if (inventories != null) { 
            Product ProductFound = null;
                foreach (var inventory in inventories)
                {
                    var InventoryProduct = inventory.InventoryProducts.Where(p => p.ProductId == product.ProductId && p.Isdeleted == false).FirstOrDefault();
                    ProductFound = _db.InventoryProducts.Include(p => p.Product).FirstOrDefault(p => p.ProductId == product.ProductId).Product;
                    if (ProductFound != null)
                    {
                        ProductFound.ProductName = product.ProductName;
                        ProductFound.Price = product.Price;
                        ProductFound.Description = product.Description;
                        ProductFound.Details = product.Details;
                        ProductFound.Discount = product.Discount;
                        ProductFound.Size = product.Size;
                        ProductFound.Color = product.Color;
                        ProductFound.Brand = product.Brand;
                        ProductFound.Manufacture = product.Manufacture;
                        ProductFound.SubCategotyId = product.SubCategotyId;
                        _db.SaveChanges();
                        return new Response { Status = "Success", Message = "Product update successfully", data = ProductFound };

                    }
                    return new Response { Status = "Error", Message = "Product Not Found", data = ProductFound };

                }
            }

            return new Response { Status = "Error", Message = "Inventory Not Found" };
           
        }

        internal List<List<Product>> GetSellerProducts(IIdentity seller)
        {
            var sellerId = HelperMethods.GetAuthnticatedUserId(seller);
            List<Inventory> inventories = _db.Inventories.Include(ip => ip.InventoryProducts).ThenInclude(p=>p.Product).Where(i => i.sellerId == sellerId && i.Isdeleted == false).ToList();

            List<List<Product>> products = inventories.Select(f => f.InventoryProducts.Where(f=>f.Isdeleted==false).Select(f => f.Product).Where(r=>r.IsdeletedBySeller==false).ToList()).ToList();
           
            return products;

        }

        public async Task<Response> EditProductImagesAsync(int id,IFormFile [] files, IIdentity seller)
        {
            var sellerId = HelperMethods.GetAuthnticatedUserId(seller);
            List<Inventory> inventories = _db.Inventories.Include(ip => ip.InventoryProducts).Where(i => i.sellerId == sellerId && i.Isdeleted == false).ToList();
            if (inventories != null)
            {
                Product ProductFound = null;
                foreach (var inventory in inventories)
                {
                    var InventoryProduct = inventory.InventoryProducts.Where(p => p.ProductId == id && p.Isdeleted == false).FirstOrDefault();
                    ProductFound = _db.InventoryProducts.Include(p => p.Product).ThenInclude(i=>i.ProductImages).FirstOrDefault(p => p.ProductId ==id).Product;
                    if (ProductFound != null)
                    {
                        // delete old image
                        for (int i = 0; i < ProductFound.ProductImages.Count; i++)
                        {
                            File.Delete(ProductFound.ProductImages[i].Image);
                        }

                        // create new image
                        List<string> pathes = await FileHelper.SaveImagesAsync(id, files, "Products");
                        for (int i = 0; i < pathes.Count; i++)
                        {
                            ProductFound.ProductImages[i].Image = pathes[i];
                        }
                        _db.SaveChanges();
                        return new Response { Status = "Success", Message = "Product images update successfully", data = ProductFound.ProductImages };

                    }
                    return new Response { Status = "Error", Message = "Product Not Found", data = ProductFound };

                }
            }

            return new Response { Status = "Error", Message = "Inventory Not Found" };
        }

        public List<Product>  GetTopSeales()
        {
          return  _db.Products.Where(p=>p.IsdeletedBySeller==false && p.Active ==true).OrderByDescending(p => p.QuantitySealed).Take(5).ToList();
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
            var totalRecords = await _db.Products.Where(s => s.SubCategotyId == subCategoryId && s.IsdeletedBySeller==false && s.Active==true).CountAsync();
           return PaginationHelper.CreatePagedReponse<Product>(pagedData, validFilter, totalRecords, _uriService, route);
        }



        public  List<Product> SearchProduct(string name)
        {
          return _db.Products.Include(i=>i.ProductImages).Where(p => p.ProductName.Contains(name) && p.IsdeletedBySeller ==false && p.Active == true).ToList();
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


        // get waiting product

    }
}
