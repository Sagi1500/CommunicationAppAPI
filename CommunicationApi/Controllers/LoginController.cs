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
    public class LoginController : ControllerBase
    {
        private readonly UsersServices _service;
        private IConfiguration _configuration;
        public LoginController(UsersServices service, IConfiguration confing)
        {
            _service = service;
            _configuration = confing;
        }

       
        [HttpPost]
        public async Task<IActionResult> PostLogin([Bind("Id,Password")] User user)
        {
            //[Bind("Id,Password")] User user
            //User user = new User();
            //user.Id = Id;
            //user.Password = password;
            //checking if the user is already exists
            if (!_service.UserExists(user.Id))
            {
                return BadRequest();
            }

            // Find the user in the user table.
            var res = await _service.GetUser(user.Id);

            // cheking if the password is correct and generate JWT token.
            if (res != null && res.Password == user.Password)
            {
                var claims = new[]
                  {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["JWTParams:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                    new Claim("Id",user.Id)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["JWTParams:Issuer"],
                    _configuration["JWTParams:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(20),
                    signingCredentials: signIn);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            // if the password is not correct returns bad requrse
            return BadRequest();
        }
    }
}
