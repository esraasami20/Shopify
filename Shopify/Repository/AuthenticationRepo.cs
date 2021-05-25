using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shopify.Helper;
using Shopify.Models;
using Shopify.Repository.Interfaces;
using Shopify.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Repository
{
    public class AuthenticationRepo : IAuthentication
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private ManageRoles _manageRoles;
        private CustomerRepo _customerRepo;
        private EmployeeRepo _employeeRepo;
        private SellerRepo _sellerRepo;
        private  JwtHelper _jwt;
        EmailHelper _emailHelper ;

        public AuthenticationRepo(UserManager<ApplicationUser> userManager, ManageRoles manageRoles, CustomerRepo customerRepo, EmployeeRepo employeeRepo, SellerRepo sellerRepo, IOptions<JwtHelper> jwt , EmailHelper emailHelper )
        {
            _manageRoles = manageRoles;
            _userManager = userManager;
            _customerRepo=customerRepo;
            _sellerRepo = sellerRepo;
            _employeeRepo= employeeRepo;
            _jwt = jwt.Value;
             _emailHelper= emailHelper; ;


        }

     

        public async Task<ResponseAuth> LoginAsync(LoginModel model)
        {
          var user =  await _userManager.FindByEmailAsync(model.Email);
            if(user==null||!await _userManager.CheckPasswordAsync(user, model.password))
            {
                return new ResponseAuth { Message = "email or password not valid" };
            }
           
            var token = await CreateJwtToken(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            return new ResponseAuth
            {
                Email = user.Email,
                UserName = user.Email,
                Role =userRoles[0],
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = token.ValidTo,
                IsAuthenticated = true

            };

        }

        public async Task<ResponseAuth> RegisterCustomerAsync(RegisterModel model)
        {
           
            if (await _userManager.FindByEmailAsync(model.Email)!=null)
                return new ResponseAuth{Message="Email is already Exist"};

            if (await _userManager.FindByNameAsync(model.Username) != null)
                return new ResponseAuth { Message = "Username is already Exist" };

            ApplicationUser user = new ApplicationUser()
            {
                Fname = model.Fname,
                Lname = model.Lname,
                Address = model.Address,
                Email = model.Email,
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

                var result = await _userManager.CreateAsync(user, model.Password);
              if (!result.Succeeded)
              {
                var errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new ResponseAuth { Message =errors };

               }

               _customerRepo.AddCustomerId(user.Id);

                await _manageRoles.AddToCustomerRole(user);
                var token =await CreateJwtToken(user);
            return new ResponseAuth
            {
                Email = user.Email,
                UserName = user.Email,
                Role = "Customer",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = token.ValidTo,
                IsAuthenticated = true

             };
               
           

        }

        public async Task<ResponseAuth> RegisterEmployeeAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new ResponseAuth { Message = "Email is already Exist" };

            if (await _userManager.FindByNameAsync(model.Username) != null)
                return new ResponseAuth { Message = "Username is already Exist" };

            ApplicationUser user = new ApplicationUser()
            {
                Fname = model.Fname,
                Lname = model.Lname,
                Address = model.Address,
                Email = model.Email,
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new ResponseAuth { Message = errors };

            }

            _employeeRepo.AddEmployeeId(user.Id);

            await _manageRoles.AddToEmployeeRole(user);
            var token = await CreateJwtToken(user);
            return new ResponseAuth
            {
                Email = user.Email,
                UserName = user.Email,
                Role = "Employee",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = token.ValidTo,
                IsAuthenticated = true

            };
        }

        public async Task<ResponseAuth> RegisterSellerAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new ResponseAuth { Message = "Email is already Exist" };

            if (await _userManager.FindByNameAsync(model.Username) != null)
                return new ResponseAuth { Message = "Username is already Exist" };

            ApplicationUser user = new ApplicationUser()
            {
                Fname = model.Fname,
                Lname = model.Lname,
                Address = model.Address,
                Email = model.Email,
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new ResponseAuth { Message = errors };

            }

            _sellerRepo.AddSellerId(user.Id);

            await _manageRoles.AddToSellerRole(user);
            var token = await CreateJwtToken(user);
            return new ResponseAuth
            {
                Email = user.Email,
                UserName = user.Email,
                Role = "Seller",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = token.ValidTo,
                IsAuthenticated = true

            };
        }



        public async Task<ResponseAuth> RegisterAdminAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return new ResponseAuth { Message = "Email is already Exist" };

            if (await _userManager.FindByNameAsync(model.Username) != null)
                return new ResponseAuth { Message = "Username is already Exist" };

            ApplicationUser user = new ApplicationUser()
            {
                Fname = model.Fname,
                Lname = model.Lname,
                Address = model.Address,
                Email = model.Email,
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new ResponseAuth { Message = errors };

            }

            await _manageRoles.AddToSAdminRole(user);
            var token = await CreateJwtToken(user);
            return new ResponseAuth
            {
                Email = user.Email,
                UserName = user.Email,
                Role = "Admin",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = token.ValidTo,
                IsAuthenticated = true

            };
        }


        public async Task<Response> ForgetPasswordAsync(ForgetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new Response { Status = "Error", Message = "this email not valid" };
            }
            else
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodeToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodeToken);

               
               var result = await _emailHelper.SendEmailAsync(model.Email, validToken);
                if(result)
                return  new Response {Status = "Success", Message = "this email not valid" };
                return new Response { Status = "Error2", Message = "please try again" };
            }
        }



        public async Task<Response> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new Response { Status = "Error", Message = "this email not valid" };
            }
            else
            {
                var decodeToken = WebEncoders.Base64UrlDecode(model.Token);
                var normalToken = Encoding.UTF8.GetString(decodeToken);


                var result = await _userManager.ResetPasswordAsync(user, normalToken, model.Password);
                if (result.Succeeded)
                    return new Response { Status = "Success", Message = "this email not valid" };
                return new Response { Status = "Error", Message = "invalid password"};
            }
        }





  














        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

       
    }
}
