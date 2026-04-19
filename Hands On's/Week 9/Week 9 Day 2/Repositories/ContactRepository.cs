using Microsoft.EntityFrameworkCore;
using Week9_Day2_ContactManagementAPI.Models;

namespace Week9_Day2_ContactManagementAPI.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _context;

        public ContactRepository(ContactDbContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            Console.WriteLine("Fetching all contacts from simulated DB...");
            return await _context.Contacts.OrderBy(c => c.ContactId).ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            Console.WriteLine($"Fetching contact {id} from simulated DB...");
            return await _context.Contacts.FirstOrDefaultAsync(c => c.ContactId == id);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Contacts.CountAsync();
        }

        public async Task<List<Contact>> GetPagedAsync(int pageNumber, int pageSize)
        {
            Console.WriteLine($"Fetching page {pageNumber} with size {pageSize} from simulated DB...");
            return await _context.Contacts
                .OrderBy(c => c.ContactId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
