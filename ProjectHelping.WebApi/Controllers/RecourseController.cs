using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;

namespace ProjectHelping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecourseController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return Ok(uow.GetRepository<Recourse>().GetAll().ToList());
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public Recourse Get(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var project = uow.GetRepository<Recourse>().Get(x => x.Id.Equals(id));
                return project;
            }
        }

        [HttpGet("GetDeveloperRecourse/{id}")]
        public List<Recourse> GetDeveloperRecourse(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var recourses = uow.GetRepository<Recourse>().GetAll(x => x.DeveloperId.Equals(id))?.ToList();
                return recourses;
            }
        }
        [HttpGet("GetEmployerRecourse/{id}")]
        public List<Recourse> GetEmployerRecourse(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var recourses = uow.GetRepository<Recourse>().GetAll(x => x.EmployerId.Equals(id))?.ToList();
                return recourses;
            }
        }

        [HttpPost("AddRecourse")]
        public IActionResult AddRecourse(Recourse relation)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                relation.Id = System.Guid.NewGuid().ToString();
                uow.GetRepository<Recourse>().Add(relation);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
        }

        // PUT api/<DeveloperController>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Recourse relation)
        {
            if (relation == null)
            {
                return BadRequest();
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                var pro = uow.GetRepository<Recourse>().Get(x => x.Id.Equals(relation.Id));
                Extensions.ObjectMapper.Map(pro, relation);
                uow.GetRepository<Recourse>().Update(pro);
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
                var pro = uow.GetRepository<Recourse>().Get(x => x.Id.Equals(id));
                if (pro == null)
                {
                    return NotFound();
                }
                uow.GetRepository<Recourse>().Delete(pro);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }

        }
    }
}
