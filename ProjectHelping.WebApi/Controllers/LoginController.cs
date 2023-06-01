using Azure.Identity;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectHelping.Business.Dto;
using ProjectHelping.Business.FluentValidation;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectHelping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            var user = Authenticate(userLogin);
            if (user)
            {
                var token = Generate(userLogin);
                return Ok(token);
            }
            return Unauthorized("Username or password is wrong.");
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDto register)
        {
            // Fluentvalidation kurulumunu test etmek için  yazıldı register metodu tamamlanacak..                  
            RegisterValidator registerValidator = new RegisterValidator();
            var result = registerValidator.Validate(register);
            if (result.IsValid)
            {
                register.Username = register.Username.Trim();
                register.Password = Extensions.Extensions.MD5Sifrele(register.Password.Trim());
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (uow.GetRepository<Developer>().Any(x => x.Email.Equals(register.Email) || x.Username.Equals(register.Username)) || uow.GetRepository<Employer>().Any(x => x.Email.Equals(register.Email) || x.Username.Equals(register.Username)))
                    {
                        return BadRequest("Bu email veya kullanıcı adı zaten kayıtlı.");
                    }
                    else
                    {
                        if (register.Role.Equals("Developer"))
                        {
                            Developer developer = new Developer();
                            Extensions.ObjectMapper.Map(developer, register);
                            developer.Id = System.Guid.NewGuid().ToString();
                            uow.GetRepository<Developer>().Add(developer);
                        }
                        else if (register.Role.Equals("Employer"))
                        {
                            Employer employer = new Employer();
                            Extensions.ObjectMapper.Map(employer, register);
                            employer.Id = System.Guid.NewGuid().ToString();
                            uow.GetRepository<Employer>().Add(employer);
                        }

                        if (uow.SaveChanges() > 0)
                        {
                            return Ok("Kayıt başarılı..");
                        }
                    }

                }
                return StatusCode(500);
            }
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        private string Generate(UserLoginDto userDto)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Developer developer;
                Employer employer;
                string username = "", email = "", name = "", surname = "", role = "", id = "";

                var userPassword = Extensions.Extensions.MD5Sifrele(userDto.Password.Trim());
                developer = uow.GetRepository<Developer>().Get(x => x.Username.Equals(userDto.Username) && x.Password.Equals(userPassword));
                employer = uow.GetRepository<Employer>().Get(x => x.Username.Equals(userDto.Username) && x.Password.Equals(userPassword));
                if (developer != null)
                {
                    var user = developer;
                    id = user.Id.ToString();
                    username = user.Username;
                    email = user.Email;
                    name = user.Name;
                    surname = user.Surname;
                    role = "Developer";
                }
                else if (employer != null)
                {
                    var user = employer;
                    id = user.Id.ToString();
                    username = user.Username;
                    email = user.Email;
                    name = user.Name;
                    role = "Employer";
                }
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                                {
                new Claim("Username",username),
                new Claim("Id", id.ToString()),
                new Claim("Email",email),
                new Claim("Name",name),
                new Claim("Role",role),
                new Claim("Surname",surname),
            };

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }

        private bool Authenticate(UserLoginDto userLogin)
        {

            // Kullanıcı veri tabanından çekilecek. Eğer kullanıcı varsa kullanıcının bilgileri dönülecek.
            using (UnitOfWork uow = new UnitOfWork())
            {
                var userPassword = Extensions.Extensions.MD5Sifrele(userLogin.Password.Trim());
                var developer = uow.GetRepository<Developer>().Any(x => x.Username.Equals(userLogin.Username) && x.Password.Equals(userPassword));
                var employer = uow.GetRepository<Employer>().Any(x => x.Username.Equals(userLogin.Username) && x.Password.Equals(userPassword));
                if (developer || employer)
                    return true;
            }
            return false;
        }
    }
}

