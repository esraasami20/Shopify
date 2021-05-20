using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shopify.Helper;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Repository
{
    public class SubCategoryRepo
    {

        ShopifyContext _db;
        public SubCategoryRepo(ShopifyContext db)
        {
            _db = db;
        }


        // get sub category by id
        public SubCategory GetSubCategory(int id)
        {
            SubCategory result= _db.SubCategories.Include("Products").Where(c => c.SubCategoryId == id && c.Isdeleted == false).FirstOrDefault();
            return result;
        }




        // add sub category 
        public async Task<SubCategory> AddSubCategoryAsync( SubCategory subCategory , IFormFile file)
        {
            Category category = _db.Categories.Where(c => c.CategoryId == subCategory.CategoryId && c.Isdeleted == false).FirstOrDefault();
            if (category != null)
            {
                _db.SubCategories.Add(subCategory);
                _db.SaveChanges();

                if (file != null)
                {
                    string imagepath = await FileHelper.SaveImageAsync(subCategory.SubCategoryId, file, "SubCategories");
                    subCategory.Image = imagepath;
                    _db.SaveChanges();
                }
                return subCategory;
            }
          
            return null;

        }




        //  edit sub category
        public async Task<SubCategory> EditSubCategoryAsync(SubCategory category, IFormFile file)
        {
            SubCategory subCategoryDetails = _db.SubCategories.Where(s=>s.SubCategoryId==category.SubCategoryId&&s.Isdeleted==false).FirstOrDefault();
            if (subCategoryDetails != null)
            {
                if (file != null)
                {

                    // delete old image

                    File.Delete(subCategoryDetails.Image);

                    // create new image
                    string imagepath = await FileHelper.SaveImageAsync(subCategoryDetails.SubCategoryId, file, "SubCategories");
                    subCategoryDetails.Image = imagepath;
                }
                subCategoryDetails.Name = category.Name;

                _db.SaveChanges();
            }
            return subCategoryDetails;

        }


        //  delete SubCategory
        public SubCategory DeleteSubCategory(int id)
        {
            SubCategory subCategory = _db.SubCategories.Where(c => c.SubCategoryId == id && c.Isdeleted == false).FirstOrDefault();
            if (subCategory != null)
            {
                subCategory.Isdeleted = true;
                _db.SaveChanges();

            }
            return subCategory;

        }




    }
}
