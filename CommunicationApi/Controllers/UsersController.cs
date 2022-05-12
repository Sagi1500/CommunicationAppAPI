using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;

namespace CommunicationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public readonly UsersContext _entities;
        public UsersController(UsersContext config)
        {
            _entities = config;
        }

        [HttpPost]
        public async Task<List<Users>> Post(string id, string password)
        {
            Users user = new Users();
            user.id = id;
            user.password = password;
            _entities.Users.Add(user);
            await _entities.SaveChangesAsync();
                 return await _entities.Users.ToListAsync();
           
        }
    }
}