using Microsoft.AspNetCore.SignalR;
using Domain;
using CommunicationApi.Services;

namespace CommunicationApi.Hubs
{
    public class AppHub : Hub
    {
        private readonly AppHubServices _service;

        private readonly IDictionary<string, string> _dictionary;

        //AppHub Constructor 
        public AppHub(IDictionary<string, string> dictionary, AppHubServices services)
        {
            _dictionary = dictionary;
            _service = services;
        }


        // Adding the value to connection dictionary.
        public Task AddUserToConnection(string UserId)
        {
            _dictionary[Context.ConnectionId] = UserId;
            return Task.CompletedTask;
        }


        //Sending a message to another user. ReceiveMessage should be the method in Client.
        public async Task SendMessage(Message message)
        {
            // Finding the sender and sending the message.
            if (_dictionary.TryGetValue(Context.ConnectionId, out string? UserId))
            {
                if (message.UserId != null && message.ContactId != null)
                {
                    await Clients.Client(message.ContactId).SendAsync("ReceiveMessage", message);
                }
            }
        }



        // Current user add another user as his contact. Adding the new contact to the contact List.
        public async Task AddContact(Contact contact)
        {
            // Finding the sender and sending the message.
            if (contact.UserId != null && contact.Id != null)
            {
                if (_dictionary.TryGetValue(Context.ConnectionId, out string? UserId))
                {
                    await Clients.Client(contact.Id).SendAsync("ContactAdded", contact);
                }
            }
        }
    }
}
