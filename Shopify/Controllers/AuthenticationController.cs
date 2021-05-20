using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
using System.Text;
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



        [HttpPost]
        [Route("register-admin")]

        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authentication.RegisterAdminAsync(model);
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
                var result = await _authentication.LoginAsync(model);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
              var result = await _authentication.ForgetPasswordAsync(model);
                if (result.Status == "Success")
                    return Ok();

                return BadRequest(new Response { Status=result.Status,Message=result.Message});
             
            }
        }



        // reset password
        [HttpPost("reset-password")]

        public async Task<ActionResult> ForgetPassword([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _authentication.ResetPasswordAsync(model);
                if (result.Status == "Success")
                    return Ok();

                return BadRequest(new Response { Status = result.Status, Message = result.Message });

            }
        }


    }
}
