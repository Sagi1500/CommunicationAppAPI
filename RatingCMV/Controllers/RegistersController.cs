using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace CommunicationAppApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RegistersController : Controller
    {
        private readonly RegistersContext _registersContext;
        private readonly ContactsContext _contactsContext;

        public RegistersController(RegistersContext _registersContext, ContactsContext _contactsContext)
        {
            _registersContext = _registersContext;
            _contactsContext = _contactsContext;    
        }

        // GET: Registers
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Json(await _registersContext.Registers.ToListAsync());
                        
        }

        //// GET: Registers/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _registersContext.Registers == null)
        //    {
        //        return NotFound();
        //    }

        //    var registers = await _registersContext.Registers
        //        .FirstOrDefaultAsync(m => m.id == id);
        //    if (registers == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(registers);
        //}

        //// GET: Registers/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Registers/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("id,password")] Registers registers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _registersContext.Add(registers);
        //        await _registersContext.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(registers);
        //}

        //// GET: Registers/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null || _registersContext.Registers == null)
        //    {
        //        return NotFound();
        //    }

        //    var registers = await _registersContext.Registers.FindAsync(id);
        //    if (registers == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(registers);
        //}

        //// POST: Registers/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("id,password")] Registers registers)
        //{
        //    if (id != registers.id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _registersContext.Update(registers);
        //            await _registersContext.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RegistersExists(registers.id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(registers);
        //}

        //// GET: Registers/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null || _registersContext.Registers == null)
        //    {
        //        return NotFound();
        //    }

        //    var registers = await _registersContext.Registers
        //        .FirstOrDefaultAsync(m => m.id == id);
        //    if (registers == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(registers);
        //}

        //// POST: Registers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    if (_registersContext.Registers == null)
        //    {
        //        return Problem("Entity set 'RegistersContext.Registers'  is null.");
        //    }
        //    var registers = await _registersContext.Registers.FindAsync(id);
        //    if (registers != null)
        //    {
        //        _registersContext.Registers.Remove(registers);
        //    }
            
        //    await _registersContext.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool RegistersExists(string id)
        //{
        //  return (_registersContext.Registers?.Any(e => e.id == id)).GetValueOrDefault();
        //}
    }
}
