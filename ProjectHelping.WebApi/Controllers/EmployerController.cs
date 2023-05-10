using Microsoft.AspNetCore.Mvc;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;

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
        public Employer Get(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var employer = uow.GetRepository<Employer>().Get(x => x.Id.Equals(id));
                employer.Password = "";
                return employer;
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
