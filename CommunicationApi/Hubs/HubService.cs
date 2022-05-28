using Microsoft.AspNetCore.SignalR;
using Domain;

namespace CommunicationApi.Hubs
{
    public class HubService : Hub
    {

        public async Task MessageRecieved(Message message)
        {
            
        }
        

    }
}
