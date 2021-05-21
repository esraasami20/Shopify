﻿using Shopify.Models;
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


        // get product by id
        public Product GetProduct(int id)
        {
            return _db.Products.Where(b => b.ProductId == id && b.Isdeleted == false).FirstOrDefault();
        }




        // add product 
        public Product AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
        }


        // get product's brands

        public List<Product> getBrandsForProduct(int id)
        {
            SubCategory subCategory = _db.SubCategories.Where(s => s.SubCategoryId == id && s.Isdeleted == false).FirstOrDefault();
            if (subCategory != null)
            {
                return _db.SubCategories.Where(s => s.CategoryId == id).FirstOrDefault().Products;
            }
            return null;
        }



        //  edit product
        public Product EditProductAsync(Product product)
        {
            Product productDetails = GetProduct(product.ProductId);
            if (productDetails != null)
            {
                productDetails.ProductName = product.ProductName;
                productDetails.Price = product.Price;
                productDetails.Discount = product.Discount;
                productDetails.Size = product.Size;
                productDetails.Color = product.Color;
                productDetails.SubCategotyId = product.SubCategotyId;
                productDetails.PromotionId = product.PromotionId;
                _db.SaveChanges();
                return productDetails;
            }
            return null;

        }


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
