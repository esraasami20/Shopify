﻿using Microsoft.AspNetCore.Http;
using Shopify.Helper;
using Shopify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Repository.Interfaces
{
   public interface IAuthentication
    {
        public Task<ResponseAuth> RegisterCustomerAsync(RegisterModel model);
        public Task<ResponseAuth> RegisterEmployeeAsync(RegisterEmployeeViewModel model);
        public Task<ResponseAuth> RegisterSellerAsync(RegisterSellerModel model, IFormFile[] file);
        public Task<ResponseAuth> RegisterAdminAsync(RegisterModel model);
        public Task<ResponseAuth> LoginAsync(LoginModel model);
        public Task<ResponseAuth> LoginWithFacebookAsync(string accessToken);
        public Task<Response> ForgetPasswordAsync(ForgetPasswordModel model);
        public Task<Response> ResetPasswordAsync(ResetPasswordModel model);
    }
}
