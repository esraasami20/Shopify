using Microsoft.AspNetCore.Http;
using Shopify.Helper;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Repository.Interfaces
{
    public class CategoryRepo
    {
        ShopifyContext _db;
        public CategoryRepo(ShopifyContext db)
        {
            _db = db;
        }

        // get all categories
        public List<Category> GetAllCategories()
        {
            return _db.Categories.Where(c=>c.Isdeleted==false).ToList();
        }

        // get category by id
        public Category GetCategory(int id)
        {
            Category category = _db.Categories.SingleOrDefault(c => c.CategoryId == id&&c.Isdeleted==false);
            return category;
        }


        // add   category
        public async Task<Category> AddCategory(Category category, IFormFile file)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            if (file != null)
            {
                string imagepath = await FileHelper.SaveImageAsync(category.CategoryId, file, "Categories");
                category.Image = imagepath;
                _db.SaveChanges();
            }
            return category;
        }

    


        //  Edit Category
        public async Task<Category> EditCategoryAsync(Category category , IFormFile file)
        {
           Category categoryDetails = GetCategory(category.CategoryId);
            if (categoryDetails != null)
            {
                if (file != null)
                {

                    // delete old image

                    File.Delete(categoryDetails.Image);

                    // create new image
                    string imagepath = await FileHelper.SaveImageAsync(categoryDetails.CategoryId, file, "Categories");
                    categoryDetails.Image = imagepath;
                }
                categoryDetails.Name = category.Name;
                
                _db.SaveChanges();
            }
            return categoryDetails;           
        }

        //  delete category
        public Category DeleteCategory(int id)
        {
            Category category = GetCategory(id);
            if (category != null) {
                category.Isdeleted = true;
                _db.SaveChanges();
               
            }
            return category;
        }
    }
}
