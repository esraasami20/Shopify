using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Repository.Interfaces;
using Shopify.Repository;
using Microsoft.EntityFrameworkCore;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ShopifyContext _shopifyContext;
        private readonly UserManager<ApplicationUser> _applicationUser;
        private readonly CustomerRepo _customerRepo;

        public CustomerController(ShopifyContext shopifyContext, UserManager<ApplicationUser> applicationUser, CustomerRepo customerRepo)
        {
            _shopifyContext = shopifyContext;
            _applicationUser = applicationUser;
            _customerRepo = customerRepo;
        }

        //get sellers data
        [HttpGet]
        public ActionResult<List<ApplicationUser>> GetAll()
        {
            return _customerRepo.GetAllCustomers();
        }
        //get customer by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(string id)
        {
            var customer = await _shopifyContext.Customers.Include("ApplicationUser").FirstOrDefaultAsync(s => s.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }
        //edit customer
        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationUser>> EditCustomerAsync(string id, [FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                user.Id = id;
                var result = await _customerRepo.editCustomer(id, user);
                if (result != null)
                    return NoContent();
                return NotFound();
            }

        }
        // delete seller
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            ApplicationUser app = new ApplicationUser();
            var customer = await _shopifyContext.Customers.FindAsync(id);
            var customerInUser = await _shopifyContext.Users.FindAsync(customer.CustomerId);
            if (customer == null)
            {
                return NotFound();
            }
            if (customerInUser != null)
            {
                customerInUser.AdminLocked = true;
                customer.Isdeleted = true;
                await _shopifyContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
