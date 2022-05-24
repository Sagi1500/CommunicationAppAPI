using Microsoft.AspNetCore.Mvc;
using Domain;
using CommunicationApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace CommunicationApi.Controllers
{
    [Authorize]
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
            if (res == true)
            {
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

