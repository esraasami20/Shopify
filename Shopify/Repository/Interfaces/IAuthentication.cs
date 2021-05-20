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
        public Task<ResponseAuth> RegisterEmployeeAsync(RegisterModel model);
        public Task<ResponseAuth> RegisterSellerAsync(RegisterModel model);
        public Task<ResponseAuth> RegisterAdminAsync(RegisterModel model);
        public Task<ResponseAuth> LoginAsync(LoginModel model);
        public Task<Response> ForgetPasswordAsync(ForgetPasswordModel model);
        public Task<Response> ResetPasswordAsync(ResetPasswordModel model);
    }
}
