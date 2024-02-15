using Catalog.API.Exceptions;
using Catalog.Business.Model;
using Catalog.Business.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService authentication;
        private readonly IConfiguration settings;

        public AuthenticationController(AuthenticationService authentication, IConfiguration settings)
        {
            this.authentication = authentication;
            this.settings = settings;
        }

        // POST api/<AuthenticationController>
        [HttpPost]
        public IActionResult Post([FromBody] Login login)
        {
            if (authentication.Login(login))
            {
                string token = GenerateToken();

                return Ok(token);
            }

            return Unauthorized();
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(settings["Jwt:Issuer"],
              settings["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return token;
        }
    }
}
