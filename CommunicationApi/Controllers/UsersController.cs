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
        private IConfiguration _configuration;

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


        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(String Id)
        {
            var res = await _service.GetUser(Id);
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
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,_configuration["JWTParams:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("Id",user.Id)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:Key"]));
            var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JWTParams:Issuer"],
                _configuration["JWTParams:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: mac);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete (String Id)
        {
            var res = await _service.DeleteUser(Id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}



/*
       // GET: Users
       public async Task<IActionResult> Index()
       {
             return _context.Users != null ? 
                         View(await _context.Users.ToListAsync()) :
                         Problem("Entity set 'ApplicationContext.Users'  is null.");
       }

       // GET: Users/Details/5
       public async Task<IActionResult> Details(string id)
       {
           if (id == null || _context.Users == null)
           {
               return NotFound();
           }

           var user = await _context.Users
               .FirstOrDefaultAsync(m => m.Id == id);
           if (user == null)
           {
               return NotFound();
           }

           return View(user);
       }

       // GET: Users/Create
       public IActionResult Create()
       {
           return View();
       }

       // POST: Users/Create
       // To protect from overposting attacks, enable the specific properties you want to bind to.
       // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Create([Bind("Id,Password")] User user)
       {
           if (ModelState.IsValid)
           {
               _context.Add(user);
               await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));
           }
           return View(user);
       }

       // GET: Users/Edit/5
       public async Task<IActionResult> Edit(string id)
       {
           if (id == null || _context.Users == null)
           {
               return NotFound();
           }

           var user = await _context.Users.FindAsync(id);
           if (user == null)
           {
               return NotFound();
           }
           return View(user);
       }

       // POST: Users/Edit/5
       // To protect from overposting attacks, enable the specific properties you want to bind to.
       // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Edit(string id, [Bind("Id,Password")] User user)
       {
           if (id != user.Id)
           {
               return NotFound();
           }

           if (ModelState.IsValid)
           {
               try
               {
                   _context.Update(user);
                   await _context.SaveChangesAsync();
               }
               catch (DbUpdateConcurrencyException)
               {
                   if (!UserExists(user.Id))
                   {
                       return NotFound();
                   }
                   else
                   {
                       throw;
                   }
               }
               return RedirectToAction(nameof(Index));
           }
           return View(user);
       }

       // GET: Users/Delete/5
       public async Task<IActionResult> Delete(string id)
       {
           if (id == null || _context.Users == null)
           {
               return NotFound();
           }

           var user = await _context.Users
               .FirstOrDefaultAsync(m => m.Id == id);
           if (user == null)
           {
               return NotFound();
           }

           return View(user);
       }

       // POST: Users/Delete/5
       [HttpPost, ActionName("Delete")]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> DeleteConfirmed(string id)
       {
           if (_context.Users == null)
           {
               return Problem("Entity set 'ApplicationContext.Users'  is null.");
           }
           var user = await _context.Users.FindAsync(id);
           if (user != null)
           {
               _context.Users.Remove(user);
           }

           await _context.SaveChangesAsync();
           return RedirectToAction(nameof(Index));
       }

         private bool UserExists(string id)
          {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
       */