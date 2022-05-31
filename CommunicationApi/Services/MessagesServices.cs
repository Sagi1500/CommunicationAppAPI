using Microsoft.EntityFrameworkCore;
using CommunicationApi.Data;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationApi.Services
{
    public class MessagesServices
    {

        private ApplicationContext _context;

        public MessagesServices(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

       
        public async Task<IEnumerable<Message>?> GetAllMessages(string? ContactId, string? UserId)
        {
            if (_context.Users == null || ContactId == null || UserId == null)
            {
                return null;
            }

            return await _context.Messages.Where(m => m.ContactId == ContactId && m.UserId == UserId).ToListAsync();
        }

        public async Task<bool> CreateNewMessage(Message message)
        {
            if (message == null || _context.Messages == null)
            {
                return false;
            }
            if (await MessageExist(message) == true)
            {
                return false;
            }
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<Message?> GetMessageById(Message message)
        {
            if (message == null || _context.Messages == null || await MessageExist(message) == false)
            {
                return null;
            }
            
            var res = await _context.Messages.Where(m => m.Id == message.Id && m.ContactId == message.ContactId
            && m.UserId == message.UserId).FirstOrDefaultAsync();
            return res;
        }


        public async Task<bool> EditMessage(Message newMessage)
        {

            if (newMessage == null || _context.Messages == null)
            {
                return false;
            }
            var m = await GetMessageById(newMessage);
            if (m != null)
            {
                m.Content = newMessage.Content;
                _context.Messages.Update(m);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveAllMessages(string UserId, string Id)
        {
            var messages = await GetAllMessages(Id, UserId);
            if (messages != null) {
                foreach (Message m in messages)
                {
                    _context.Messages.Remove(m);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteMessage(Message message)
        {
            if (message == null || _context.Messages == null)
            {
                return false;
            }
            var m = await GetMessageById(message);
            if (m != null)
            {
                _context.Messages.Remove(m);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


      

        public async Task<bool> MessageExist(Message message)
        {
            if (message != null)
            {
                var res = await _context.Messages.Where(m => m.ContactId == message.ContactId && m.UserId == message.UserId).ToListAsync();
                return res.Any(m => m.Id == message.Id);
            }
            return false;
        }
    }
}
