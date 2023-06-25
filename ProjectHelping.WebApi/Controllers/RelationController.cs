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
    public class RelationController : ControllerBase
    {// GET: api/<DeveloperController>
        [HttpGet]
        public IActionResult GetAll(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return Ok(uow.GetRepository<Relation>().GetAll(x => x.SlaveId.Equals(id)).ToList());
            }
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public List<CommentDto> GetComments(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<CommentDto> commentDtos = new List<CommentDto>();
                var relations = uow.GetRepository<Relation>().GetAll(x => x.MasterId.Equals(id)).ToList();
                var developers = uow.GetRepository<Developer>().GetAll().ToList();
                var employers = uow.GetRepository<Employer>().GetAll().ToList();

                foreach (var item in relations)
                {
                    var commentDto = ObjectMapper.Map<CommentDto>(item);
                    if (employers.Any(x => x.Id.Equals(item.SlaveId)))
                    {
                        var employer = employers.Where(x => x.Id.Equals(item.SlaveId))?.FirstOrDefault();
                        commentDto.Name = employer.Name;
                        commentDto.Surname = employer.Surname;
                        commentDto.Email = employer.Email;
                    }
                    else
                    {
                        var developer = developers.Where(x => x.Id.Equals(item.SlaveId))?.FirstOrDefault();
                        commentDto.Name = developer.Name;
                        commentDto.Surname = developer.Surname;
                        commentDto.Email = developer.Email;
                    }
                    commentDtos.Add(commentDto);
                }
                return commentDtos;
            }
        }

        [HttpPost("AddRelation")]
        public IActionResult AddRelation(Relation relation)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                relation.Id = System.Guid.NewGuid().ToString();
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
        public IActionResult Delete(string id)
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
