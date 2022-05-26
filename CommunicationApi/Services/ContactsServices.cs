using Microsoft.EntityFrameworkCore;
using CommunicationApi.Data;
using Domain;

namespace CommunicationApi.Services
{
    public class ContactsServices
    {

        private ApplicationContext _context;

        public ContactsServices(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        
        public async Task<IEnumerable<Contact>?> GetAll(string UserId)
        {
            if (_context.Contacts == null || UserId == null)
            {
                return null;
            }
            return await _context.Contacts.Where(c => c.UserId == UserId).ToListAsync();
        }


        public async Task<Contact?> GetContact(string? UserId, string? Id)
        {
            if (Id == null || _context.Contacts == null || UserId == null || Id == null)
            {
                return null;
            }
            var res = await _context.Contacts.Where(c => c.Id == Id && c.UserId==UserId).FirstOrDefaultAsync();
            if (res == null)
            {
                return null;
            }
            return res;
        }

        public async Task<bool> AddNewContact(Contact contact)
        {
            if (contact == null || _context.Contacts == null)
            {
                return false;
            }
            if (ContactExist(contact))
            {
                return false;
            }
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> EditContact(Contact newContact)
        {
            
            var contacts = await GetContact(newContact.UserId, newContact.Id);
            if (contacts == null)
            {
                return false;
            }
            contacts.Name = newContact.Name;
            contacts.Server = newContact.Server;
            _context.Contacts.Update(contacts);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteContact (string UserId, string id)
        {
            var contacts =  await GetContact(UserId,id);
            if (contacts == null)
            {
                return false;
            }
            _context.Contacts.Remove(contacts);
            await _context.SaveChangesAsync();
            return true;
        }


        public bool ContactExist(Contact contact)
        {
            return (_context.Contacts?.Any(c => c.Id == contact.Id && c.UserId== contact.UserId)).GetValueOrDefault();
        }
    }
}
