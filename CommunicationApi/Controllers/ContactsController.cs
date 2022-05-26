using Domain;
using CommunicationApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsServices _contactService;
        private readonly UsersServices _userService;

        public ContactsController(ContactsServices contactService, UsersServices usersServices)
        {
            _contactService = contactService;
            _userService = usersServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }
            var res = await _contactService.GetAll(userId);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }
            var res = await _contactService.GetContact(userId, id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }


        [HttpPost]
        public async Task<IActionResult> AddContact([Bind("Id,Name,Server")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Last = null;
                contact.Lastdate = null;
                try
                {
                    var userId = GetLoggedInUser();
                    if (userId == null)
                    {
                        return Unauthorized();
                    }
                    contact.UserId = userId;
                    if (contact.Id != contact.UserId && contact.Id != null && _userService.UserExists(contact.Id) )
                    {
                        var res = await _contactService.AddNewContact(contact);
                        if (res == false)
                        {
                            return BadRequest();
                        }
                        return Ok();
                    }
                }
                catch (Exception e)
                {
                    return NotFound();
                }
            }
            return BadRequest();

        }


        
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(String? id, [Bind("Name,Server")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                // find the logged user.
                var userId = GetLoggedInUser();
                if (userId == null)
                {
                    return Unauthorized();
                }

                // Asking to find a user with same Id and UserId.
                if (userId == id)
                {
                    return BadRequest();
                }

                // update contact values before editing.
                contact.UserId = userId;
                contact.Id = id;
                var res = await _contactService.EditContact(contact);

                // returning value.
                if (res == true)
                {
                    return Ok();
                }

            }
            return NotFound();
        }
        


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }
            var res = await _contactService.DeleteContact(userId, Id);
            if (res == true)
            {
                return Ok();
            }
            return NotFound();
        }

        private string? GetLoggedInUser()
        {
            var userId = User.FindFirst("Id")?.Value;
            return userId;
        }

    }
}

