using DAL.DataAccess;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ParticipantEventRepository : IParticipantEventRepository
    {
        private readonly EMSDbContext _context;

        public ParticipantEventRepository(EMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParticipantEventDetails>> GetByParticipantAsync(string email)
        {
            return await _context.ParticipantEventDetails
                .Where(p => p.ParticipantEmailId == email)
                .Include(p => p.Event)
                    .ThenInclude(e => e!.Sessions)
                .ToListAsync();
        }

        public async Task<IEnumerable<ParticipantEventDetails>> GetByEventAsync(Guid eventId)
        {
            return await _context.ParticipantEventDetails
                .Where(p => p.EventId == eventId)
                .Include(p => p.Participant)
                .ToListAsync();
        }

        public async Task<ParticipantEventDetails?> GetByIdAsync(Guid id)
        {
            return await _context.ParticipantEventDetails
                .Include(p => p.Event)
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task<bool> RegisterAsync(ParticipantEventDetails registration)
        {
            if (await IsRegisteredAsync(registration.ParticipantEmailId, registration.EventId))
                return false;
            registration.ID = Guid.NewGuid();
            _context.ParticipantEventDetails.Add(registration);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnregisterAsync(Guid id)
        {
            var reg = await _context.ParticipantEventDetails.FindAsync(id);
            if (reg == null) return false;
            _context.ParticipantEventDetails.Remove(reg);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAttendanceAsync(Guid id, bool isAttended)
        {
            var reg = await _context.ParticipantEventDetails.FindAsync(id);
            if (reg == null) return false;
            reg.IsAttended = isAttended;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsRegisteredAsync(string email, Guid eventId)
        {
            return await _context.ParticipantEventDetails
                .AnyAsync(p => p.ParticipantEmailId == email && p.EventId == eventId);
        }
    }
}
