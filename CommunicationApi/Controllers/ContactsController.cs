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
            // Find the Id of the currently logged in user.
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }

            // Get all contacts from DB.
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
            // Find the Id of the currently logged in user.
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }

            // Get contact by Id from DB.
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
                // For safty initialize values to null.
                contact.Last = null;
                contact.Lastdate = null;
                try
                {
                    // Find the Id of the currently logged in user.
                    var userId = GetLoggedInUser();
                    if (userId == null)
                    {
                        return Unauthorized();
                    }

                    // update UserId property in contact - inserting the logged in user.
                    contact.UserId = userId;

                    // Adding contact to DB.
                    if (contact.Id != contact.UserId && contact.Id != null)
                    {
                        var res = await _contactService.AddNewContact(contact);
                        if (res == false)
                        {
                            return BadRequest();
                        }
                        return Created("/api/Contacts",contact);
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
                // Find the Id of the currently logged in user.
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
                    return NoContent();
                }

            }
            return BadRequest();
        }
        


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            // Find the Id of the currently logged in user.
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }

            //Delete contact from DB.
            var res = await _contactService.DeleteContact(userId, Id);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }

        // This function returns the logged user id.
        private string? GetLoggedInUser()
        {
            var userId = User.FindFirst("Id")?.Value;
            return userId;
        }

    }
}

