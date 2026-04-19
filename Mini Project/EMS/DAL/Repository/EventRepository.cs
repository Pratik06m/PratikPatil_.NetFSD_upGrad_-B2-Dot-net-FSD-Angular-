using DAL.DataAccess;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly EMSDbContext _context;

        public EventRepository(EMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventDetails>> GetAllAsync()
        {
            return await _context.EventDetails
                .Include(e => e.Sessions)
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<EventDetails>> GetActiveAsync()
        {
            return await _context.EventDetails
                .Where(e => e.Status == "Active")
                .Include(e => e.Sessions)
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<EventDetails?> GetByIdAsync(Guid id)
        {
            return await _context.EventDetails.FindAsync(id);
        }

        public async Task<EventDetails?> GetWithSessionsAsync(Guid id)
        {
            return await _context.EventDetails
                .Include(e => e.Sessions)
                    .ThenInclude(s => s.Speaker)
                .FirstOrDefaultAsync(e => e.EventId == id);
        }

        public async Task<IEnumerable<EventDetails>> GetByCategoryAsync(string category)
        {
            return await _context.EventDetails
                .Where(e => e.EventCategory == category && e.Status == "Active")
                .Include(e => e.Sessions)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(EventDetails eventDetails)
        {
            eventDetails.EventId = Guid.NewGuid();
            _context.EventDetails.Add(eventDetails);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(EventDetails eventDetails)
        {
            var existing = await _context.EventDetails.FindAsync(eventDetails.EventId);
            if (existing == null) return false;
            existing.EventName = eventDetails.EventName;
            existing.EventCategory = eventDetails.EventCategory;
            existing.EventDate = eventDetails.EventDate;
            existing.Description = eventDetails.Description;
            existing.Status = eventDetails.Status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var ev = await _context.EventDetails.FindAsync(id);
            if (ev == null) return false;
            _context.EventDetails.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleStatusAsync(Guid id)
        {
            var ev = await _context.EventDetails.FindAsync(id);
            if (ev == null) return false;
            ev.Status = ev.Status == "Active" ? "In-Active" : "Active";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            return await _context.EventDetails
                .Where(e => e.Status == "Active")
                .Select(e => e.EventCategory)
                .Distinct()
                .ToListAsync();
        }
    }
}
