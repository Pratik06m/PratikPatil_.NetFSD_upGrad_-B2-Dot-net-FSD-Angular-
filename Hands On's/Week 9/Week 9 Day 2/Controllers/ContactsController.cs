using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Week9_Day2_ContactManagementAPI.Services;

namespace Week9_Day2_ContactManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        // Problem 1: Caching
        [HttpGet("cached")]
        public async Task<IActionResult> GetAllCachedContacts()
        {
            var contacts = await _service.GetAllContactsAsync();
            return Ok(contacts);
        }

        [HttpGet("cached/{id}")]
        public async Task<IActionResult> GetCachedContactById(int id)
        {
            var contact = await _service.GetContactByIdAsync(id);
            if (contact == null)
                return NotFound(new { message = $"Contact with ID {id} not found." });

            return Ok(contact);
        }

        // Problem 2: Paging
        [HttpGet]
        public async Task<IActionResult> GetPagedContacts(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _service.GetPagedContactsAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }
}
