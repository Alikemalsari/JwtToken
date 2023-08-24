using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtDemo.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }
        private Users AuthenticateUser(Users user)
        {
            Users _user = null;
            if (user.name == "admin" && user.password == "12345")
            {
                _user = new Users { name = "Giray" };
            }
            return _user;

        }

        private string GenerateToken(Users user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users user)
        {
            //IActionResult response = Unauthorized();
            // var user_ = AuthenticateUser(user);
            // if (user_ != null)
            // {
            //     var token = GenerateToken(user_);
            //     response = Ok(new { token = token });
            // }
            // return response;
            var user_ = AuthenticateUser(user);
            if(user_ != null)
            {
                var token = GenerateToken(user_);
                return Ok(token);
            }
            return NotFound("User not found");
        }
    }
}
