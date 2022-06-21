using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CommunicationApi.Services;

namespace CommunicationApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class NotificationTokenController : ControllerBase
    {

        private UsersServices _usersServices;
        private Dictionary<String, String> _firebaseTokens;

        public NotificationTokenController()
        {
            _firebaseTokens = NotificationTokenServices.FirebaseTokens;
        }

        [HttpPost]
        public async Task<IActionResult> SaveToken ([FromBody]string Token) {
            var username = GetLoggedInUser();
            if (username != null)
            {
                if (_firebaseTokens.ContainsKey(username))
                {
                    _firebaseTokens[username] = Token;
                    return Ok();
                }
                _firebaseTokens.Add(username, Token);
            }
            return Ok();
        }

        // This function returns the logged user id.
        private string? GetLoggedInUser()
        {
            var userId = User.FindFirst("Id")?.Value;
            return userId;
        }
    }
}
