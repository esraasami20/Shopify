using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shopify.Helper;
using Shopify.Models;
using Shopify.Repository;
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
        private ManageRoles _manageRoles;
        private CustomerRepo _customerRepo;
        private EmployeeRepo _employeeRepo;
        private SellerRepo _sellerRepo;

        public AuthenticationController(UserManager<ApplicationUser> userManager, EmployeeRepo employeeRepo, CustomerRepo customerRepo, ManageRoles manageRoles,  SellerRepo sellerRepo)
        {
            this.userManager = userManager;
            _customerRepo = customerRepo;
            _manageRoles = manageRoles;
            _employeeRepo = employeeRepo;
            _sellerRepo =  sellerRepo;
        }
     

        // sign up customer 

        [HttpPost]
        [Route("register-customer")]

        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                ApplicationUser user = new ApplicationUser()
                {
                    Fname = model.Fname,
                    Lname = model.Lname,
                    Address = model.Address,
                    Email = model.Email,
                    UserName = model.Username,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

                
                  _customerRepo.AddCustomerId(user.Id);

                 if(! await _manageRoles.AddToCustomerRole(user))
                 {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "customer not added to role" });
                 }

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
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
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                ApplicationUser user = new ApplicationUser()
                {
                    Fname = model.Fname,
                    Lname = model.Lname,
                    Address = model.Address,
                    Email = model.Email,
                    UserName = model.Username,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


                _employeeRepo.AddEmployeeId(user.Id);

                if (!await _manageRoles.AddToEmployeeRole(user))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "customer not added to role" });
                }

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
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
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                ApplicationUser user = new ApplicationUser()
                {
                    Fname = model.Fname,
                    Lname = model.Lname,
                    Address = model.Address,
                    Email = model.Email,
                    UserName = model.Username,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


                _sellerRepo.AddSellerId(user.Id);

                if (!await _manageRoles.AddToSellerRole(user))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "customer not added to role" });
                }

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            return BadRequest(ModelState);
        }


        // login





    }
}
