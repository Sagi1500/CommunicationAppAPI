using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;

namespace CommunicationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        public IConfiguration _configuration;
        public RegisterController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        public IActionResult Post (string username,string password)
        {

            if (username == "a1" && password == "a1")
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["JWTParams:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                    new Claim("UserId",username)
                };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
                var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["JWTParams:Issuer"],
                    _configuration["JWTParams:Audience"],
                    claims,
                    expires:DateTime.UtcNow.AddMinutes(20),
                    signingCredentials:mac);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest();   
            }
        }
    }
}