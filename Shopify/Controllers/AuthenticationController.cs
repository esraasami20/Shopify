using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet]
        public ActionResult get()
        {
            return Ok("Hi");
        }
        public ActionResult geet()
        {
            return Ok("Hi");
        }

        public ActionResult abcdef()
        {
            return Ok("Hi amr  bye");
        }
    }
}
