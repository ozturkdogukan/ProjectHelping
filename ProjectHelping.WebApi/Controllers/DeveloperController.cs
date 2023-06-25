using Microsoft.AspNetCore.Mvc;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectHelping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {

        [HttpPost("AddSkill")]
        public IActionResult AddSkill(Skill skills)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                skills.Id = System.Guid.NewGuid().ToString();
                uow.GetRepository<Skill>().Add(skills);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("GetSkills/{id}")]
        public List<Skill> GetSkills(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var developer = uow.GetRepository<Skill>().GetAll(x => x.UserId.Equals(id))?.ToList();
                return developer;
            }
        }

        [HttpPost("AddExperience")]
        public IActionResult AddExperience(Experience experience)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                experience.Id = System.Guid.NewGuid().ToString();
                uow.GetRepository<Experience>().Add(experience);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("GetExperiences/{id}")]
        public List<Experience> GetExperiences(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var developer = uow.GetRepository<Experience>().GetAll(x => x.UserId.Equals(id)).ToList();
                return developer;
            }
        }


        [HttpPost("AddEducation")]
        public IActionResult AddEducation(Education education)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                education.Id = System.Guid.NewGuid().ToString();
                uow.GetRepository<Education>().Add(education);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("GetEducations/{id}")]
        public List<Education> GetEducations(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var developer = uow.GetRepository<Education>().GetAll(x => x.UserId.Equals(id)).ToList();
                return developer;
            }
        }



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
        public Developer Get(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var developer = uow.GetRepository<Developer>().Get(x => x.Id.Equals(id));
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
        public IActionResult Delete(string id)
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
