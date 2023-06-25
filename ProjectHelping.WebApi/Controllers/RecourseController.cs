using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using ProjectHelping.Business.Dto;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;

namespace ProjectHelping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecourseController : ControllerBase
    {
        [HttpGet]
        public List<RecourseDetailDto> GetAll()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<RecourseDetailDto> result = new List<RecourseDetailDto>();
                var recourses = uow.GetRepository<Recourse>().GetAll()?.ToList();
                var developerIds = recourses.Select(x => x.DeveloperId);
                var employerIds = recourses.Select(x => x.EmployerId);
                var subprojectIds = recourses.Select(x => x.SubProjectId);
                var advertIds = recourses.Select(x => x.AdvertId);
                var developers = uow.GetRepository<Developer>().GetAll(x => developerIds.Contains(x.Id)).ToList();
                var employers = uow.GetRepository<Employer>().GetAll(x => employerIds.Contains(x.Id)).ToList();
                var subprojects = uow.GetRepository<SubProject>().GetAll(x => subprojectIds.Contains(x.Id)).ToList();
                var adverts = uow.GetRepository<Advert>().GetAll(x => advertIds.Contains(x.Id)).ToList();
                foreach (var item in recourses)
                {
                    RecourseDetailDto recourseDetailDto = new RecourseDetailDto();
                    recourseDetailDto.Id = item.Id;
                    recourseDetailDto.SubProject = subprojects.Where(x => x.Id.Equals(item.SubProjectId))?.FirstOrDefault();
                    recourseDetailDto.Advert = adverts.Where(x => x.Id.Equals(item.AdvertId))?.FirstOrDefault();
                    recourseDetailDto.Developer = developers.Where(x => x.Id.Equals(item.DeveloperId))?.FirstOrDefault();
                    recourseDetailDto.Employer = employers.Where(x => x.Id.Equals(item.EmployerId))?.FirstOrDefault();
                    recourseDetailDto.Status = item.Status;
                    recourseDetailDto.Desc = item.Desc;
                    result.Add(recourseDetailDto);
                }

                return result;
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public RecourseDetailDto Get(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var recourse = uow.GetRepository<Recourse>().Get(x => x.Id.Equals(id));
                var developer = uow.GetRepository<Developer>().Get(x => x.Id.Equals(recourse.DeveloperId));
                var employer = uow.GetRepository<Employer>().Get(x => x.Id.Equals(recourse.EmployerId));
                var subproject = uow.GetRepository<SubProject>().Get(x => x.Id.Equals(recourse.SubProjectId));
                var advert = uow.GetRepository<Advert>().Get(x => x.Id.Equals(recourse.AdvertId));
                RecourseDetailDto recourseDetailDto = new RecourseDetailDto();
                recourseDetailDto.Id = recourse.Id;
                recourseDetailDto.SubProject = subproject;
                recourseDetailDto.Developer = developer;
                recourseDetailDto.Employer = employer;
                recourseDetailDto.Advert = advert;
                recourseDetailDto.Status = recourse.Status;
                recourseDetailDto.Desc = recourse.Desc;
                return recourseDetailDto;
            }
        }

        [HttpGet("GetDeveloperRecourse/{id}")]
        public List<RecourseDetailDto> GetDeveloperRecourse(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<RecourseDetailDto> result = new List<RecourseDetailDto>();
                var recourses = uow.GetRepository<Recourse>().GetAll(x => x.DeveloperId.Equals(id))?.ToList();
                var developerIds = recourses.Select(x => x.DeveloperId);
                var employerIds = recourses.Select(x => x.EmployerId);
                var subprojectIds = recourses.Select(x => x.SubProjectId);
                var advertIds = recourses.Select(x => x.AdvertId);
                var developers = uow.GetRepository<Developer>().GetAll(x => developerIds.Contains(x.Id)).ToList();
                var employers = uow.GetRepository<Employer>().GetAll(x => employerIds.Contains(x.Id)).ToList();
                var subprojects = uow.GetRepository<SubProject>().GetAll(x => subprojectIds.Contains(x.Id)).ToList();
                var adverts = uow.GetRepository<Advert>().GetAll(x => advertIds.Contains(x.Id)).ToList();
                foreach (var item in recourses)
                {
                    RecourseDetailDto recourseDetailDto = new RecourseDetailDto();
                    recourseDetailDto.Id = item.Id;
                    recourseDetailDto.SubProject = subprojects.Where(x => x.Id.Equals(item.SubProjectId))?.FirstOrDefault();
                    recourseDetailDto.Advert = adverts.Where(x => x.Id.Equals(item.AdvertId))?.FirstOrDefault();
                    recourseDetailDto.Developer = developers.Where(x => x.Id.Equals(item.DeveloperId))?.FirstOrDefault();
                    recourseDetailDto.Employer = employers.Where(x => x.Id.Equals(item.EmployerId))?.FirstOrDefault();
                    recourseDetailDto.Status = item.Status;
                    recourseDetailDto.Desc = item.Desc;
                    result.Add(recourseDetailDto);
                }

                return result;
            }
        }
        [HttpGet("GetEmployerRecourse/{id}")]
        public List<RecourseDetailDto> GetEmployerRecourse(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<RecourseDetailDto> result = new List<RecourseDetailDto>();
                var recourses = uow.GetRepository<Recourse>().GetAll(x => x.EmployerId.Equals(id))?.ToList();
                var developerIds = recourses.Select(x => x.DeveloperId);
                var employerIds = recourses.Select(x => x.EmployerId);
                var subprojectIds = recourses.Select(x => x.SubProjectId);
                var advertIds = recourses.Select(x => x.AdvertId);
                var developers = uow.GetRepository<Developer>().GetAll(x => developerIds.Contains(x.Id)).ToList();
                var employers = uow.GetRepository<Employer>().GetAll(x => employerIds.Contains(x.Id)).ToList();
                var subprojects = uow.GetRepository<SubProject>().GetAll(x => subprojectIds.Contains(x.Id)).ToList();
                var adverts = uow.GetRepository<Advert>().GetAll(x => advertIds.Contains(x.Id)).ToList();
                foreach (var item in recourses)
                {
                    RecourseDetailDto recourseDetailDto = new RecourseDetailDto();
                    recourseDetailDto.Id = item.Id;
                    recourseDetailDto.SubProject = subprojects.Where(x => x.Id.Equals(item.SubProjectId))?.FirstOrDefault();
                    recourseDetailDto.Advert = adverts.Where(x => x.Id.Equals(item.AdvertId))?.FirstOrDefault();
                    recourseDetailDto.Developer = developers.Where(x => x.Id.Equals(item.DeveloperId))?.FirstOrDefault();
                    recourseDetailDto.Employer = employers.Where(x => x.Id.Equals(item.EmployerId))?.FirstOrDefault();
                    recourseDetailDto.Status = item.Status;
                    recourseDetailDto.Desc = item.Desc;
                    result.Add(recourseDetailDto);
                }

                return result;
            }
        }

        [HttpGet("GetAdvertRecourse/{id}")]
        public List<RecourseDetailDto> GetAdvertRecourse(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<RecourseDetailDto> result = new List<RecourseDetailDto>();
                var recourses = uow.GetRepository<Recourse>().GetAll(x => x.AdvertId.Equals(id))?.ToList();
                var developerIds = recourses.Select(x => x.DeveloperId);
                var employerIds = recourses.Select(x => x.EmployerId);
                var subprojectIds = recourses.Select(x => x.SubProjectId);
                var advertIds = recourses.Select(x => x.AdvertId);
                var developers = uow.GetRepository<Developer>().GetAll(x => developerIds.Contains(x.Id)).ToList();
                var employers = uow.GetRepository<Employer>().GetAll(x => employerIds.Contains(x.Id)).ToList();
                var subprojects = uow.GetRepository<SubProject>().GetAll(x => subprojectIds.Contains(x.Id)).ToList();
                var adverts = uow.GetRepository<Advert>().GetAll(x => advertIds.Contains(x.Id)).ToList();
                foreach (var item in recourses)
                {
                    RecourseDetailDto recourseDetailDto = new RecourseDetailDto();
                    recourseDetailDto.Id = item.Id;
                    recourseDetailDto.SubProject = subprojects.Where(x => x.Id.Equals(item.SubProjectId))?.FirstOrDefault();
                    recourseDetailDto.Advert = adverts.Where(x => x.Id.Equals(item.AdvertId))?.FirstOrDefault();
                    recourseDetailDto.Developer = developers.Where(x => x.Id.Equals(item.DeveloperId))?.FirstOrDefault();
                    recourseDetailDto.Employer = employers.Where(x => x.Id.Equals(item.EmployerId))?.FirstOrDefault();
                    recourseDetailDto.Status = item.Status;
                    recourseDetailDto.Desc = item.Desc;
                    result.Add(recourseDetailDto);
                }

                return result;
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
        [HttpPost("Update")]
        public IActionResult Update([FromBody] RecourseDto relation)
        {
            if (relation == null)
            {
                return BadRequest();
            }
            using (UnitOfWork uow = new UnitOfWork())
            {
                var pro = uow.GetRepository<Recourse>().Get(x => x.Id.Equals(relation.Id));
                if (pro == null)
                {
                    return NotFound();
                }
                pro.Status = relation.Status;
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
