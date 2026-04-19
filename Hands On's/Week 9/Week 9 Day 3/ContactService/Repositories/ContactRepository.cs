using ContactService.Data;
using ContactService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _context;
        public ContactRepository(ContactDbContext context) { _context = context; }

        public async Task<List<Contact>> GetAllAsync() => await _context.Contacts.OrderBy(x => x.ContactId).ToListAsync();
        public async Task<Contact?> GetByIdAsync(int id) => await _context.Contacts.FirstOrDefaultAsync(x => x.ContactId == id);

        public async Task<Contact> AddAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> UpdateAsync(int id, Contact contact)
        {
            var existing = await _context.Contacts.FindAsync(id);
            if (existing == null) return false;
            existing.Name = contact.Name;
            existing.Email = contact.Email;
            existing.Phone = contact.Phone;
            existing.CategoryId = contact.CategoryId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Contacts.FindAsync(id);
            if (existing == null) return false;
            _context.Contacts.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
