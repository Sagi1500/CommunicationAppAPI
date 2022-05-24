using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunicationApi.Data;
using Domain;
using CommunicationApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace CommunicationApi.Controllers
{
   
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsServices _service;

        public ContactsController(ContactsServices service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var user = GetLoggedInUser();
            if (user == null)
            {
                return Unauthorized();
            }
            var res = await _service.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetContact(String? Id)
        {
            var res = await _service.GetContact(Id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }


        [HttpPost]
        public async Task<IActionResult> AddContact([Bind("Id,Name,Server")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await _service.AddNewContact(contact);
                    if (res == false)
                    {
                        return NotFound();
                    }
                    return Ok();
                }
                catch (Exception e)
                {
                    return NotFound();
                }
            }
            return BadRequest();

        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([Bind("Id,Name,Server")] Contact contact)
        {
            if (ModelState.IsValid)
            {
             
                var res = await _service.EditContact(contact);
                if (res == true)
                {
                    return NoContent();
                }

            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            var res = await _service.DeleteContact(id);
            if (res == true) {
                return NoContent();
            }
            return NotFound();
        }

        private string? GetLoggedInUser()
        {
            var userId = User.FindFirst("Id")?.Value;
            return userId;
        }

    }


}




/*
// GET: Contacts
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Contacts.Include(c => c.User);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Server,Last,Lastdate,UserId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contact.UserId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contact.UserId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Server,Last,Lastdate,UserId")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", contact.UserId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'ApplicationContext.Contacts'  is null.");
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(string id)
        {
          return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
*/