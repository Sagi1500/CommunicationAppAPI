using CommunicationApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace CommunicationApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {

        private readonly ContactsServices _contactService;
        private readonly UsersServices _userService;

        // Constructor.
        public InvitationsController(ContactsServices contactService, UsersServices usersServices)
        {
            _contactService = contactService;
            _userService = usersServices;
        }

        [HttpPost]
        public async Task<IActionResult> Post([Bind("From,To,Server")] Invitation invitation)
        {
             // checking if the logged user is equal to from is invitaion.
            if (invitation.To == null || invitation.From == null || invitation.Server == null || _userService.UserExists(invitation.From) == false
                || _userService.UserExists(invitation.To) == false)
            {
                return BadRequest();
            }

            // create contact.
            Contact contact = InitializeConteact(invitation.From, invitation.To, invitation.Server);

            // Adding the contact to the logged in server.
            var res = await _contactService.AddNewContact(contact);
            if (res == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        // The function recieves the invitation information and the logged in user and create contact.
        private Contact InitializeConteact(string Id, string UserId, string Server)
        {
            Contact newContact = new Contact();
            newContact.Id = Id;
            newContact.UserId = UserId;
            newContact.Server = Server;
            return newContact;
        }

    }
}
