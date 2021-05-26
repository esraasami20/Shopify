﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopify.Models;
using Shopify.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        StatusRepo _statusRepo;

        public StatusController(StatusRepo statusRepo)
        {
            _statusRepo = statusRepo;
        }
        //get all status
        [HttpGet]
        public ActionResult<List<Status>> getall()
        {
            return _statusRepo.GetAllStatus();
        }

        // get Status by id
        [HttpGet("{id}")]
        public ActionResult<Status> GetStatus(int id)
        {
            var result = _statusRepo.GetStatus(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        // add status
        [HttpPost]
        public ActionResult<Status> AddStatus([FromBody] Status status)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Status result = _statusRepo.AddStatus(status);

                return Ok(result);
            }

        }
        //edit Status
        [HttpPut("{id}")]
        public ActionResult EditStatus(int id, [FromBody] Status status)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                status.StatusId = id;
                var result = _statusRepo.EditStatus(status);
                if (result != null)
                    return NoContent();
                return NotFound();
            }

        }
        // delete Status
        [HttpDelete("{id}")]
        public ActionResult deleteStatus(int id)
        {
            var result = _statusRepo.DeleteStatus(id);
            if (result != null)
                return NoContent();
            return NotFound();
        }

    }
}
