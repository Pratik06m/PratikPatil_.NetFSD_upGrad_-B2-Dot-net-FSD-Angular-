using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week9_Day1_ContactManagementApi.Data;
using Week9_Day1_ContactManagementApi.DTOs;
using Week9_Day1_ContactManagementApi.Models;

namespace Week9_Day1_ContactManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAll()
        {
            return Ok(await _context.Contacts.OrderBy(x => x.ContactId).ToListAsync());
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Contact>> GetById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return NotFound("Contact not found.");

            return Ok(contact);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ContactCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contact = new Contact
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = contact.ContactId }, contact);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, ContactUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return NotFound("Contact not found.");

            contact.Name = dto.Name;
            contact.Email = dto.Email;
            contact.Phone = dto.Phone;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Contact updated successfully." });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return NotFound("Contact not found.");

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Contact deleted successfully." });
        }
    }
}
