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
    public class AdvertController : ControllerBase
    {
        // GET: api/<DeveloperController>
        [HttpGet("GetAll")]
        public List<AdvertDto> GetAll()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<AdvertDto> resultDto = new List<AdvertDto>();
                var adverts = uow.GetRepository<Advert>().GetAll();
                var subprojects = uow.GetRepository<SubProject>().GetAll().ToList();
                var employers = uow.GetRepository<Employer>().GetAll().ToList();
                foreach (var item in adverts)
                {
                    var employer = employers.Where(x => x.Id.Equals(item.EmployerId))?.FirstOrDefault();
                    var subproject = subprojects.Where(x => x.Id.Equals(item.SubProjectId))?.FirstOrDefault();
                    var advertDto = ObjectMapper.Map<AdvertDto>(item);
                    advertDto.Employer = employer;
                    advertDto.SubProject = subproject;
                    resultDto.Add(advertDto);
                }
                return resultDto;
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public AdvertDto Get(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var project = uow.GetRepository<Advert>().Get(x => x.Id.Equals(id));
                var employer = uow.GetRepository<Employer>().Get(x => x.Id.Equals(project.EmployerId));
                var subproject = uow.GetRepository<SubProject>().Get(x => x.Id.Equals(project.SubProjectId));
                var advertDto = ObjectMapper.Map<AdvertDto>(project);
                advertDto.Employer = employer;
                advertDto.SubProject = subproject;
                return advertDto;
            }
        }

        [HttpGet("GetFav/{id}")]
        public List<AdvertDto> GetFav(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<AdvertDto> resultList = new List<AdvertDto>();
                var subprojects = uow.GetRepository<SubProject>().GetAll().ToList();
                var employers = uow.GetRepository<Employer>().GetAll().ToList();
                var relations = uow.GetRepository<Relation>().GetAll(x => x.SlaveId.Equals(id) && x.RelationType.Equals("favori"));
                var adverts = uow.GetRepository<Advert>().GetAll().ToList();
                foreach (var item in relations)
                {
                    var advert = adverts.Where(x => x.Id.Equals(item.MasterId))?.FirstOrDefault();
                    var advertDto = ObjectMapper.Map<AdvertDto>(advert);
                    var employer = employers.Where(x => x.Id.Equals(advert.EmployerId))?.FirstOrDefault();
                    var subproject = subprojects.Where(x => x.Id.Equals(advert.SubProjectId))?.FirstOrDefault();
                    advertDto.Employer = employer;
                    advertDto.SubProject = subproject;
                    resultList.Add(advertDto);
                }
                return resultList;
            }
        }

        [HttpPost("AddAdvert")]
        public IActionResult AddProject(Advert project)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                project.Id = System.Guid.NewGuid().ToString();
                uow.GetRepository<Advert>().Add(project);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
        }

        // PUT api/<DeveloperController>/5
        [HttpPut("Update")]
        public IActionResult Update([FromBody] Advert project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                var pro = uow.GetRepository<Advert>().Get(x => x.Id.Equals(project.Id));
                Extensions.ObjectMapper.Map(pro, project);
                uow.GetRepository<Advert>().Update(pro);
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
                var pro = uow.GetRepository<Advert>().Get(x => x.Id.Equals(id));
                if (pro == null)
                {
                    return NotFound();
                }
                uow.GetRepository<Advert>().Delete(pro);
                if (uow.SaveChanges() > 0)
                {
                    return Ok();
                }
                return StatusCode(500);
            }

        }
    }
}
