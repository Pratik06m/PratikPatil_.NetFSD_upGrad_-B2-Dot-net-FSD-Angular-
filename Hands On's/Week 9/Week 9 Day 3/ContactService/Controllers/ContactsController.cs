using ContactService.Models;
using ContactService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;
        public ContactsController(IContactService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _service.GetByIdAsync(id);
            return contact == null ? NotFound(new { message = $"Contact with ID {id} not found." }) : Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.AddAsync(contact);
            return CreatedAtAction(nameof(GetById), new { id = created.ContactId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Contact contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _service.UpdateAsync(id, contact);
            return updated ? Ok(new { message = "Contact updated successfully." }) : NotFound(new { message = $"Contact with ID {id} not found." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? Ok(new { message = "Contact deleted successfully." }) : NotFound(new { message = $"Contact with ID {id} not found." });
        }
    }
}
