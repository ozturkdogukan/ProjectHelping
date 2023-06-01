using Microsoft.AspNetCore.Mvc;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectHelping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProjectController : ControllerBase
    {
        // GET: api/<DeveloperController>
        [HttpGet]
        public IActionResult GetAll()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return Ok(uow.GetRepository<SubProject>().GetAll().ToList());
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public SubProject Get(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var project = uow.GetRepository<SubProject>().Get(x => x.Id.Equals(id));
                return project;
            }
        }

        [HttpGet("/GetSubProjectAdverts/{id}")]
        public List<Advert> GetSubProjectAdverts(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var project = uow.GetRepository<SubProject>().Get(x => x.Id.Equals(id));
                var adverts = uow.GetRepository<Advert>().GetAll(x => x.SubProjectId.Equals(id))?.ToList();
                return adverts;
            }
        }

        [HttpPost("AddSubProject")]
        public IActionResult AddProject(SubProject project)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                project.Id = System.Guid.NewGuid().ToString();
                uow.GetRepository<SubProject>().Add(project);
                if (uow.SaveChanges() > 0)
                {
                    return Ok(project);
                }
                return StatusCode(500);
            }
        }

        // PUT api/<DeveloperController>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] SubProject project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                var pro = uow.GetRepository<SubProject>().Get(x => x.Id.Equals(project.Id));
                Extensions.ObjectMapper.Map(pro, project);
                uow.GetRepository<SubProject>().Update(pro);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }

        }

        // DELETE api/<DeveloperController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var pro = uow.GetRepository<SubProject>().Get(x => x.Id.Equals(id));
                if (pro == null)
                {
                    return NotFound();
                }
                uow.GetRepository<SubProject>().Delete(pro);
                var relationList = uow.GetRepository<Relation>().GetAll(x => x.SlaveObject.Equals("SubProject") && x.SlaveId.Equals(id));
                foreach (var item in relationList)
                {
                    uow.GetRepository<Relation>().Delete(item);
                }
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }

        }
    }
}
