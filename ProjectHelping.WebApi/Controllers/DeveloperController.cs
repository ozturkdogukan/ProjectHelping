﻿using Microsoft.AspNetCore.Mvc;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectHelping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        // GET: api/<DeveloperController>
        [HttpGet]
        public IActionResult GetAll()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return Ok(uow.GetRepository<Developer>().GetAll().ToList());
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public Developer Get(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var developer = uow.GetRepository<Developer>().Get(x => x.Id.Equals(id));
                developer.Password = "";
                return developer;
            }
        }

        // PUT api/<DeveloperController>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Developer developer)
        {
            if (developer == null)
            {
                return BadRequest();
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                var dev = uow.GetRepository<Developer>().Get(x => x.Id.Equals(developer.Id));
                Extensions.ObjectMapper.Map(dev, developer);
                uow.GetRepository<Developer>().Update(dev);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }

        }

        // DELETE api/<DeveloperController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var dev = uow.GetRepository<Developer>().Get(x => x.Id.Equals(id));
                if (dev == null)
                {
                    return NotFound();
                }
                uow.GetRepository<Developer>().Delete(dev);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }

        }
    }
}
