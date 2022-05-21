using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunicationApi.Data;
using Domain;


namespace CommunicationApi.Services
{
    public class UsersServices
    {

        private ApplicationContext _context;

        public UsersServices()
        {
            _context = new ApplicationContext();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            if (_context.Users ==  null)
            {
                return null;
            }
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(String? Id)
        {
            if (Id == null || _context.Users == null)
            {
                return null;
            }
            var res = await _context.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();
            if (res == null)
            {
                return null;
            }
            return res;
        }


        public async Task<bool> AddNewUser(User user)
        {
            if (user == null || _context.Users == null)
            {
                return false;
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;  
        }


        public async Task<bool> DeleteUser(String Id)
        {
            if (Id == null || _context.Users == null)
            {
                return false;
            }
            var u = await _context.Users.FindAsync(Id);
            if (u != null)
            {
                 _context.Users.Remove(u);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
