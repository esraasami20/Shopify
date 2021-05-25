using Microsoft.EntityFrameworkCore;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Product> GetAllProduct()
        {
            return _db.Products.Include(x => x.ProductImages)
                                .Include(t => t.ProductDetails)
                                .Include(b => b.Brand)
                                .Include(z => z.subCategory)
                                .ThenInclude(q => q.Category)
                                .Include(k => k.Promotions)
                                .Include(v => v.Reviews)
                                .Include(m => m.Views)
                                .Where(c => c.Isdeleted == false)
                                .ToList();
        }
        // get product by id
        public Product GetProduct(int id)
        {
            return _db.Products.Where(b => b.ProductId == id && 
                                      b.Isdeleted == false 
                                      )
                                .Include(x => x.ProductImages)
                                .Include(t=>t.ProductDetails)
                                .Include(b=>b.Brand)
                                .ThenInclude(s=>s.SubCategories)
                                .Include(z=>z.subCategory)
                                .ThenInclude(q=>q.Category)
                                .Include(k=>k.Promotions)
                                .Include(v=>v.Reviews)
                                .Include(m=>m.Views)
                                .FirstOrDefault();
        }

        // add product 
        public Product AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }

        //  edit product
        //public Product EditProductAsync(Product product)
        //{
        //    Product productDetails = GetProduct(product.ProductId);
        //    if (productDetails != null)
        //    {
        //        productDetails.ProductName = product.ProductName;
        //        productDetails.Price = product.Price;
        //        productDetails.Discount = product.Discount;
        //        productDetails.Size = product.Size;
        //        productDetails.Color = product.Color;
        //        productDetails.SubCategotyId = product.SubCategotyId;
        //        productDetails.PromotionId = product.PromotionId;
        //        _db.SaveChanges();
        //        return productDetails;
        //    }
        //    return null;
        //}


        //  delete Product
        public Product DeleteProduct(int id)
        {
            Product productDetails = GetProduct(id);
            if (productDetails != null)
            {
                productDetails.Isdeleted = true;
                _db.SaveChanges();

            }
            return productDetails;

        }
    }
}
