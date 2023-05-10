using Microsoft.AspNetCore.Mvc;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectHelping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationController : ControllerBase
    {// GET: api/<DeveloperController>
        [HttpGet]
        public IActionResult GetAll()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return Ok(uow.GetRepository<Relation>().GetAll().ToList());
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public Relation Get(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var project = uow.GetRepository<Relation>().Get(x => x.Id.Equals(id));
                return project;
            }
        }

        [HttpPost("AddRelation")]
        public IActionResult AddProject(Relation relation)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.GetRepository<Relation>().Add(relation);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
        }

        // PUT api/<DeveloperController>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Relation relation)
        {
            if (relation == null)
            {
                return BadRequest();
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                var pro = uow.GetRepository<Relation>().Get(x => x.Id.Equals(relation.Id));
                Extensions.ObjectMapper.Map(pro, relation);
                uow.GetRepository<Relation>().Update(pro);
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
                var pro = uow.GetRepository<Relation>().Get(x => x.Id.Equals(id));
                if (pro == null)
                {
                    return NotFound();
                }
                uow.GetRepository<Relation>().Delete(pro);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }

        }
    }
}
