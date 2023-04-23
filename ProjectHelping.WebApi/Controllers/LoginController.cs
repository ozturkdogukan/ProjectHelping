using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectHelping.Business.FluentValidation;
using ProjectHelping.Data.Models;
using ProjectHelping.DataAccess.UnitOfWork;
using ProjectHelping.WebApi.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User Not Found.");
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] User userLogin)
        {
            // Fluentvalidation kurulumunu test etmek için  yazıldı register metodu tamamlanacak..                  
            UserValidator userValidator = new UserValidator();
            var result = userValidator.Validate(userLogin);
            if (result.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    if (uow.GetRepository<User>().Any(x => x.Email.Equals(userLogin.Email) || x.Username.Equals(userLogin.Username)))
                    {
                        return BadRequest("Bu email veya kullanıcı adı zaten kayıtlı.");
                    }
                    else
                    {

                        userLogin.Password = Extensions.Extensions.MD5Sifrele(userLogin.Password.Trim());
                        uow.GetRepository<User>().Add(userLogin);
                        if (uow.SaveChanges() > 0)
                        {
                            return Ok("Kayıt başarılı..");
                        }
                    }

                }
            }
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Givenname),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(UserLoginDto userLogin)
        {
            // Kullanıcı veri tabanından çekilecek. Eğer kullanıcı varsa kullanıcının bilgileri dönülecek.
            UnitOfWork uow = new UnitOfWork();
            var userPassword = Extensions.Extensions.MD5Sifrele(userLogin.Password.Trim());
            var user = uow.GetRepository<User>().GetAll(x => x.Username.ToUpper().Equals(userLogin.Username.ToUpper()) && x.Password.Equals(userPassword))?.FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
