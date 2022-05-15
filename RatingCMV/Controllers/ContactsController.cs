using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunicationAppApi;
using Domain;

namespace CommunicationAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsContext _context;

        public ContactsController(ContactsContext context)
        {
            _context = context;
        }

        // GET: Contacts
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return Json(await _context.Contacts.ToListAsync());
        }

        // GET: Contacts/Details/5

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contacts
                .FirstOrDefaultAsync(m => m.id == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return Json(contacts);
        }

     

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
    
        public async Task<IActionResult> Create([Bind("id,name,server")] Contacts contacts)
        {
            if (ModelState.IsValid)
            { 
                _context.Add(contacts);
                await _context.SaveChangesAsync();
                return Created("/api/Contacts/"+contacts.id,contacts);

            }
            return BadRequest();        
        }

      
   

        // PUT: Contacts/Edit/5
        [HttpPut]

        public async Task<IActionResult> Edit([Bind("id,name,server")] Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                _context.Update(contacts);
                await _context.SaveChangesAsync();
                return NoContent();

            }
            return BadRequest();
        }


        // DELETE: Contacts/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var contacts = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contacts);
            await _context.SaveChangesAsync();
            return NoContent();
        }





        private bool ContactsExists(string id)
        {
            return (_context.Contacts?.Any(e => e.id == id)).GetValueOrDefault();
        }
      
    }
}
