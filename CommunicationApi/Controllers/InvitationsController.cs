using CommunicationApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace CommunicationApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        //private readonly UsersServices _userService;
        private readonly ContactsServices _contactsServices;
        private readonly UsersServices _usersServices;

        // Constructor.
        public InvitationsController(UsersServices userService,ContactsServices contactsServices)
        {
            _contactsServices = contactsServices;
            _usersServices = userService;
        }

        [HttpPost]
          public async Task<IActionResult> AddContact([Bind("From,To,Server")] Invitation invitation)
        {
           
            Contact? contact = InitializeConteact(invitation.From, invitation.To, invitation.Server);
            if (contact != null && invitation.To!= null && _usersServices.UserExists(invitation.To) && invitation.From!=invitation.To)
            {
                    
                await _contactsServices.AddNewContact(contact);
                   
                return Created("/api/Contacts",contact);
            }
            return BadRequest();
        }

        // The function recieves the invitation information and the logged in user and create contact.
        private Contact? InitializeConteact(string? Id, string? UserId, string? Server)
        {
            if (Id == null || UserId == null || Server == null)
            {
                return null;
            }
            Contact newContact = new Contact();
            newContact.Id = Id;
            newContact.Name = Id;
            newContact.UserId = UserId;
            newContact.Server = Server;
            return newContact;
        }

    }
}
