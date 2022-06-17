using CommunicationApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace CommunicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {

        private readonly MessagesServices _messagesService;
        private readonly UsersServices _userService;
        private readonly ContactsServices _contactsService;
        private static Dictionary<string, string> _firebaseTokens;

        // Constructor.
        public TransferController(MessagesServices messagesService, UsersServices usersServices, ContactsServices contactsServices)
        {
            _messagesService = messagesService;
            _userService = usersServices;
            _contactsService = contactsServices;
            initializeDict();
        }


        static void initializeDict()
        {
            if (_firebaseTokens == null)
            {
                _firebaseTokens = new Dictionary<string, string>(); 

                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("private_key.json")
                });

                if (!_firebaseTokens.ContainsKey("a1"))
                {
                    _firebaseTokens.Add("a1", "f8Y5bZ2APZI:APA91bFT1eLsrRXWmRUAdbrgLdwTxY2QW7NXEoOu7jLHyUyk9J7XuIaifX5tm-J4JrcbPCD4-5XXFX86JSAgTD-MudyI3hjWK6pmke1kmS9EB7jBDHQnD26uwn0BFGPZXV6604whrzau");
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([Bind("From,To,Content")] Transfer transfer)
        {
            // checking if the logged user is equal to from is invitaion.
            if (transfer.To == null ||
                transfer.From == null ||
                transfer.Content == null ||
                 _userService.UserExists(transfer.To) == false)
            {
                return BadRequest();
            }

            // create contact.
            Domain.Message message = InitializeMessage(transfer.From, transfer.To, transfer.Content);

            // Adding the contact to the logged in server.
            var res = await _messagesService.CreateNewMessage(message);
            if (res == false)
            {
                return BadRequest();
            }

            // update the last message on database.
            res = await _contactsService.UpdateContact(message);
            if (res == false)
            {
                return BadRequest();
            }
            
            if (_firebaseTokens.ContainsKey(transfer.To))
            {
              
                var registrationTokens = _firebaseTokens[transfer.To];

                var fbMessage = new FirebaseAdmin.Messaging.Message()
                {
                    Data = new Dictionary<string, string>()
                    {
                        { "myData", "1337" }
                    },
                    Token = registrationTokens,
                    Notification = new Notification()
                    {
                        Title = "Message from " + transfer.From,
                        Body = transfer.Content,
                    }
                };

                string Response = await FirebaseMessaging.DefaultInstance.SendAsync(fbMessage);
            }

            return Created("/api/Contacts/"+ transfer.From + "/Messages/",message);
        }

        // The function recieves the invitation information and the logged in user and create contact.
        private Domain.Message InitializeMessage(string ContactId, string UserId, string Content)
        {
            Domain.Message newMessage = new Domain.Message();
            newMessage.ContactId = ContactId;
            newMessage.UserId = UserId;
            newMessage.Content = Content;
            newMessage.Sent = false;
            newMessage.Created = DateTime.Now;
            return newMessage;
        }

    }
}
