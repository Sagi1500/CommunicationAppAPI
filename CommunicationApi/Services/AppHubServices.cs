using Microsoft.AspNetCore.SignalR;
using CommunicationApi.Data;
using CommunicationApi.Hubs;
using Domain;

namespace CommunicationApi.Services
{
    public class AppHubServices
    {

        private ApplicationContext _context;
        
        
        // Constructor.
        public AppHubServices(ApplicationContext applicationContext)
        {
            _context = applicationContext;

        }
       

    }
}
