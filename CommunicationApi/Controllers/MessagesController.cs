using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunicationApi.Data;
using CommunicationApi.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;

namespace CommunicationApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Contacts/{id}/[Controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesServices _messagesService;
        private readonly UsersServices _userService;
        private readonly ContactsServices _contactsServices;

        //Constructor.
        public MessagesController(MessagesServices messageService, UsersServices usersServices, ContactsServices contactService)
        {
            _messagesService = messageService;
            _userService = usersServices;
            _contactsServices = contactService;    
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts(String? id)
        {
            // Find the Id of the currently logged in user.
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }

            // checking if Id is legal.
            if (id == null)
            {
                return BadRequest();
            }

            // checking if user is exists.
            var user = _userService.UserExists(id);
            if (user == false)
            {
                return NotFound();
            }

            // return all the messages.
            var res = await _messagesService.GetAllMessages(id, userId);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Post(String id, [Bind("Content,ContactId")] Message message)
        {
            // Find the Id of the currently logged in user.
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }

            // checking if Id is legal.
            if (id == null)
            {
                return BadRequest();
            }

            // checking if user is exists.
            var user = _userService.UserExists(id);
            if (user == false)
            {
                return NotFound();
            }

            // update message details.
            message.UserId = userId;
            message.ContactId = id;
            message.Sent = true;

            // creating new message.
            var res = await _messagesService.CreateNewMessage(message);
            if (res == false)
            {
                return NotFound();
            }
            return Ok(message);
        }


        [HttpGet("{id2}")]
        public async Task<IActionResult> Get(String id, int id2)
        {

            // Find the Id of the currently logged in user.
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }

            // chekcing that the user is exists.
            if (!_userService.UserExists(id))
            {
                return NotFound();
            }

            // checking that the contact is exists.
            Contact c = new Contact();
            c.UserId = userId;
            c.Id = id;
            if (!_contactsServices.ContactExist(c))
            {
                return NotFound();
            }

            // Creating new message
            Message message = new Message();
            message.Id = id2;
            message.ContactId = id;
            message.UserId = userId;

            // Getting the message.
            var res = await _messagesService.GetMessageById(message);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }



        [HttpPut("{id2}")]
        public async Task<IActionResult> Put(String id, int id2, [Bind("Content")] Message message)
        {
            // Find the Id of the currently logged in user.
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }

            // chekcing that the user is exists.
            if (!_userService.UserExists(id))
            {
                return NotFound();
            }

            // edit values in the new message.
            message.Id = id2;
            message.ContactId = id;
            message.UserId = userId;

            // update the message in DB.
            var update = await _messagesService.EditMessage(message);
            if (update == true)
            {
                return Ok();
            }
            return NotFound();
        }


        [HttpDelete("{id2}")]
        public async Task<IActionResult> Delete (String id, int id2)
        {

            // Find the Id of the currently logged in user.
            var userId = GetLoggedInUser();
            if (userId == null)
            {
                return Unauthorized();
            }

            // chekcing that the user is exists.
            if (!_userService.UserExists(id))
            {
                return NotFound();
            }

            // checking that the contact is exists.
            Contact c = new Contact();
            c.UserId = userId;
            c.Id = id;
            if (!_contactsServices.ContactExist(c))
            {
                return NotFound();
            }

            // Creating new message
            Message message = new Message();
            message.Id = id2;
            message.ContactId = id;
            message.UserId = userId;

            // Getting the message.
            var res = await _messagesService.DeleteMessage(message);
            if (res == false)
            {
                return NotFound();
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