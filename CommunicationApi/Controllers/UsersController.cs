using CommunicationApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CommunicationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            _context = context;
        }


        // GET: Users
        [HttpGet]
        public async Task<IEnumerable<Users>> Index()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: Users/Details/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<Users>> Details(string? id)
        {
            return await _context.Users.Where(x => x.id == id).ToListAsync();
        }

        [HttpPost]
        public async void Create([Bind("id,password")] Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}

//using CommunicationApi.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;


//namespace CommunicationApi.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UsersController : ControllerBase
//    {

//        public static List<Users> usersList = new List<Users>();


//        // GET: Users
//        [HttpGet]
//        public IEnumerable<Users> Index()
//        {
//            return usersList;
//        }

//        // GET: Users/Details/5
//        [HttpGet("{id}")]
//        public Users Details(string? id)
//        {
//            return usersList.Where(x => x.id == id).FirstOrDefault();
//        }

//        [HttpPost]
//        public async void Create([Bind("id,password")] Users user)
//        {
//            usersList.Add(user);
//        }
//    }
//}
