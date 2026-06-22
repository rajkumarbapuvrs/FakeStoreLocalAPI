
using FakeStoreLocalAPI.DataBase;
using FakeStoreLocalAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FakeStoreLocalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private readonly IConfiguration _configuration;
        FakeStoreDBContext fakeStoreDBContext;
       
        public Login(IConfiguration configuration, FakeStoreDBContext fakeStoreDBContext)
        {
            _configuration = configuration;
            this.fakeStoreDBContext = fakeStoreDBContext;
        }

        [HttpPost]
        public IActionResult UserLogin(UserLogin model)
        {
            var existingUser = fakeStoreDBContext.UserLogin.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser == null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,model.Email),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return Ok(new
            {
                id = existingUser.Id,
                email = model.Email,
                name = existingUser.FullName,
                isEmployee = existingUser.IsEmployee,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost]
        [Route("register")]
        public IActionResult UserRegistration(UserLogin model)
        {
            var existingUser = fakeStoreDBContext.UserLogin.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                return BadRequest();
            }
            fakeStoreDBContext.UserLogin.Add(model);
            int ret = fakeStoreDBContext.SaveChanges();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier,model.FullName),
                    new Claim(ClaimTypes.Role,"Admin")
                };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return Ok(new
            {
                id = model.Id,
                email = model.Email,
                name = model.FullName,
                isEmployee = model.IsEmployee,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
