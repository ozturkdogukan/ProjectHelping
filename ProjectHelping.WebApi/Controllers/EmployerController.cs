using Microsoft.AspNetCore.Mvc;
using ProjectHelping.Business.Dto;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;
using ProjectHelping.Utils.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectHelping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        // GET: api/<DeveloperController>
        [HttpGet]
        public IActionResult GetAll()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return Ok(uow.GetRepository<Employer>().GetAll().ToList());
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public Employer Get(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var employer = uow.GetRepository<Employer>().Get(x => x.Id.Equals(id));
                employer.Password = "";
                return employer;
            }
        }

        [HttpGet("GetEmployerAdverts/{id}")]
        public List<AdvertDto> GetEmployerAdverts(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<AdvertDto> results = new List<AdvertDto>();
                var employers = uow.GetRepository<Employer>().GetAll().ToList();
                var adverts = uow.GetRepository<Advert>().GetAll(x => x.EmployerId.Equals(id))?.ToList();
                var subProjects = uow.GetRepository<SubProject>().GetAll().ToList();
                foreach (var item in adverts)
                {
                    var advertsDto = ObjectMapper.Map<AdvertDto>(item);
                    advertsDto.SubProject = subProjects.Where(x => x.Id.Equals(item.SubProjectId))?.FirstOrDefault();
                    advertsDto.Employer = employers.Where(x => x.Id.Equals(item.EmployerId))?.FirstOrDefault();
                    results.Add(advertsDto);
                }
                return results;
            }
        }

        [HttpGet("GetEmployerProjects/{id}")]
        public List<Project> GetEmployerProjects(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var employer = uow.GetRepository<Employer>().Get(x => x.Id.Equals(id));
                if (employer is null)
                {
                    return null;
                }
                var projects = uow.GetRepository<Project>().GetAll(x => x.EmployerId.Equals(id))?.ToList();
                return projects;
            }
        }


        [HttpGet("GetEmployerSubProjects/{id}")]
        public List<SubProject> GetEmployerSubProjects(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var employer = uow.GetRepository<Employer>().Get(x => x.Id.Equals(id));
                if (employer is null)
                {
                    return null;
                }
                var projects = uow.GetRepository<Project>().GetAll(x => x.EmployerId.Equals(id))?.Select(x => x.Id)?.ToArray();
                var subProjects = uow.GetRepository<SubProject>().GetAll(x => projects.Contains(x.ProjectId))?.ToList();
                return subProjects;
            }
        }



        // PUT api/<DeveloperController>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Employer employer)
        {
            if (employer == null)
            {
                return BadRequest();
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                var dev = uow.GetRepository<Employer>().Get(x => x.Id.Equals(employer.Id));
                if (dev == null)
                {
                    return NotFound();
                }
                employer.Password = dev.Password;
                Extensions.ObjectMapper.Map(dev, employer);
                uow.GetRepository<Employer>().Update(dev);
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
                var emp = uow.GetRepository<Employer>().Get(x => x.Id.Equals(id));
                if (emp == null)
                {
                    return NotFound();
                }
                uow.GetRepository<Employer>().Delete(emp);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }

        }
    }
}
