using Microsoft.AspNetCore.SignalR;
using Domain;
using CommunicationApi.Services;

namespace CommunicationApi.Hubs
{
    public class AppHub : Hub
    {
        private readonly AppHubServices _service;

        //AppHub Constructor 
        public AppHub(AppHubServices services)
        {
            _service = services;
        }

        public async Task LogIn(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        //Sending a message to another user. ReceiveMessage should be the method in Client.
        public async Task SendMessage(string content, string userId, string contactId)
        {
            if (content == null || userId == null || contactId == null) { return; }
            Message message = new Message() { ContactId = contactId, Content = content, UserId = userId };
            await Clients.Group(userId).SendAsync("ReceiveMessage", message);
        }



        // Current user add another user as his contact. Adding the new contact to the contact List.
        //public async Task AddContact(Contact contact)
        //{
        //    // Finding the sender and sending the message.
        //    if (contact.UserId != null && contact.Id != null)
        //    {
        //        if (_dictionary.TryGetValue(Context.ConnectionId, out string? UserId))
        //        {
        //            await Clients.Client(contact.Id).SendAsync("ContactAdded", contact);
        //        }
        //    }
        //}
    }
}


