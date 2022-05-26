using Microsoft.AspNetCore.Mvc;
using Domain;
using CommunicationApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CommunicationApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : ControllerBase
    {

        private readonly UsersServices _service;
        public IConfiguration _configuration;

        public UsersController(UsersServices service, IConfiguration confing)
        {
            _service = service;
            _configuration = confing;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _service.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var res = await _service.GetUser(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([Bind("Id,Password")] User user)
        {
            // checking if the user is not exists, 
            if (_service.UserExists(user.Id))
            {
                return BadRequest();
            }

            // adding the user to the Users table.
            try
            {
                var res = await _service.AddNewUser(user);
                if (res == false)
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }


            // generate new JWT token.
            var claims = new[] {
                  new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWTParams:Subject"]),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                  new Claim("Id",user.Id)
                };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
            var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JWTParams:Issuer"],
                _configuration["JWTParams:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: mac);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (String id)
        {
            var res = await _service.DeleteUser(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
