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
            Message message = new Message() { ContactId = contactId, Content = content, UserId = userId, Sent=false };
            await Clients.Group(contactId).SendAsync("ReceiveMessage", message);
            Message message2 = new Message() { ContactId = contactId, Content = content, UserId = userId, Sent = true };
            await Clients.Group(userId).SendAsync("ReceiveMessage", message2);
        }

        public async Task AddContact(string userId,string userServer,string id, string name, string server)
        {
            if (id == null || name == null || server == null) { return; }
            Contact contact = new Contact() { Id = userId, Name=userId,Server=userServer};
                await Clients.Group(id).SendAsync("ContactAdded", contact);
            Contact contact2 = new Contact() { Id = id, Name = name, Server = server };
            await Clients.Group(userId).SendAsync("ContactAdded", contact2);


        }
    }
}


