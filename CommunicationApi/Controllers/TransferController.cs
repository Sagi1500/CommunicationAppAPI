using CommunicationApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace CommunicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {

        private readonly MessagesServices _messagesService;
        private readonly UsersServices _userService;

        // Constructor.
        public TransferController(MessagesServices messagesService, UsersServices usersServices)
        {
            _messagesService = messagesService;
            _userService = usersServices;
        }


        [HttpPost]
        public async Task<IActionResult> Post([Bind("From,To,Content")] Transfer transfer)
        {
            // checking if the logged user is equal to from is invitaion.
            if (transfer.To == null || transfer.From == null || transfer.Content == null || _userService.UserExists(transfer.From) == false
                || _userService.UserExists(transfer.To) == false)
            {
                return BadRequest();
            }

            // create contact.
            Message message = InitializeMessage(transfer.From, transfer.To, transfer.Content);

            // Adding the contact to the logged in server.
            var res = await _messagesService.CreateNewMessage(message);
            if (res == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        // The function recieves the invitation information and the logged in user and create contact.
        private Message InitializeMessage(string ContactId, string UserId, string Content)
        {
            Message newMessage = new Message();
            newMessage.ContactId = ContactId;
            newMessage.UserId = UserId;
            newMessage.Content = Content;
            newMessage.Sent = false;
            return newMessage;
        }

    }
}
