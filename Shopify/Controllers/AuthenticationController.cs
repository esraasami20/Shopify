using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shopify.Helper;
using Shopify.Models;
using Shopify.Repository;
using Shopify.Repository.Interfaces;
using Shopify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
      
        private IAuthentication _authentication;

        public AuthenticationController(UserManager<ApplicationUser> userManager,   IAuthentication authentication)
        {
            this.userManager = userManager;
            _authentication = authentication;
        }
     

        // sign up customer 

        [HttpPost]
        [Route("register-customer")]

        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result= await _authentication.RegisterCustomerAsync(model);
                if (!result.IsAuthenticated)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }






        // sign up emplyee


        [HttpPost]
        [Route("register-emplyee")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Registeremplyee([FromBody] RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _authentication.RegisterEmployeeAsync(model);
                if (!result.IsAuthenticated)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }


        // sign up as seller 



        [HttpPost]
        [Route("register-seller")]
        
        public async Task<IActionResult> Registerseller([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authentication.RegisterSellerAsync(model);
                if (!result.IsAuthenticated)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }


        // login


        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authentication.Login(model);
                if (!result.IsAuthenticated)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }





        // forget password
        [HttpGet("forget-password")]

        public async Task<ActionResult> ForgetPassword([FromBody]ForgetPasswordModel model)
        {
          var user=  await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new Response (){ Status="Error",Message="this email not valid" });
            }
            else
            {
                EmailHelper.SendEmail(model.Email  );
                return Ok();
            }
        }



        // reset password


     
    }
}
