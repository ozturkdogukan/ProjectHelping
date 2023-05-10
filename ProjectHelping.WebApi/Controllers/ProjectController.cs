using Microsoft.AspNetCore.Mvc;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectHelping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        // GET: api/<DeveloperController>
        [HttpGet]
        public IActionResult GetAll()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return Ok(uow.GetRepository<Project>().GetAll().ToList());
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public Project Get(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var project = uow.GetRepository<Project>().Get(x => x.Id.Equals(id));
                return project;
            }
        }

        [HttpPost("AddProject")]
        public IActionResult AddProject(Project project)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.GetRepository<Project>().Add(project);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
        }

        // PUT api/<DeveloperController>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                var pro = uow.GetRepository<Project>().Get(x => x.Id.Equals(project.Id));
                Extensions.ObjectMapper.Map(pro, project);
                uow.GetRepository<Project>().Update(pro);
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
                var pro = uow.GetRepository<Project>().Get(x => x.Id.Equals(id));
                if (pro == null)
                {
                    return NotFound();
                }
                uow.GetRepository<Project>().Delete(pro);
                var subProjectList = uow.GetRepository<SubProject>().GetAll(x => x.ProjectId.Equals(pro.Id));
                var subId = subProjectList.Select(x => x.Id);
                var relationList = uow.GetRepository<Relation>().GetAll(x => x.SlaveObject.Equals("SubProject") && subId.Contains(x.SlaveId));
                foreach (var item in subProjectList)
                {
                    uow.GetRepository<SubProject>().Delete(item);
                }

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
