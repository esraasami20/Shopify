using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Helper
{
    public class FileHelper
    {

       static public async Task<string> SaveImageAsync(int categoryId, IFormFile file, string path)
        {
            var fileExtension = Path.GetExtension(Path.GetFileName(file.FileName));
           
            var newFileName = String.Concat(Convert.ToString(categoryId), fileExtension);
            string filePath = Path.Combine("Images/" + path, newFileName);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {

                await file.CopyToAsync(fileStream);
            }
            return filePath;
        }
    }
}
