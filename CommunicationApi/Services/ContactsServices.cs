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

        
        public async Task<IEnumerable<Contact>?> GetAll()
        {
            if (_context.Contacts == null)
            {
                return null;
            }
            return await _context.Contacts.ToListAsync();
        }


        public async Task<Contact?> GetContact(String? Id)
        {
            if (Id == null || _context.Contacts == null)
            {
                return null;
            }
            var res = await _context.Contacts.Where(c => c.Id == Id).FirstOrDefaultAsync();
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
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> EditContact(Contact newContact)
        {
            var contacts = await GetContact(newContact.Id);
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


        public async Task<bool> DeleteContact (String id)
        {
            var contacts =  await GetContact(id);
            if (contacts == null)
            {
                return false;
            }
            _context.Contacts.Remove(contacts);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
