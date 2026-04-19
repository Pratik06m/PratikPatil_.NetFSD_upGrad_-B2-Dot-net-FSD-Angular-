using DAL.DataAccess;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly EMSDbContext _context;

        public SessionRepository(EMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SessionInfo>> GetAllAsync()
        {
            return await _context.SessionInfos
                .Include(s => s.Event)
                .Include(s => s.Speaker)
                .ToListAsync();
        }

        public async Task<IEnumerable<SessionInfo>> GetByEventIdAsync(Guid eventId)
        {
            return await _context.SessionInfos
                .Where(s => s.EventId == eventId)
                .Include(s => s.Speaker)
                .OrderBy(s => s.SessionStart)
                .ToListAsync();
        }

        public async Task<SessionInfo?> GetByIdAsync(Guid id)
        {
            return await _context.SessionInfos
                .Include(s => s.Event)
                .Include(s => s.Speaker)
                .FirstOrDefaultAsync(s => s.SessionId == id);
        }

        public async Task<bool> AddAsync(SessionInfo session)
        {
            session.SessionId = Guid.NewGuid();
            _context.SessionInfos.Add(session);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(SessionInfo session)
        {
            var existing = await _context.SessionInfos.FindAsync(session.SessionId);
            if (existing == null) return false;
            existing.SessionTitle = session.SessionTitle;
            existing.Description = session.Description;
            existing.SessionStart = session.SessionStart;
            existing.SessionEnd = session.SessionEnd;
            existing.SessionUrl = session.SessionUrl;
            existing.SpeakerId = session.SpeakerId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var session = await _context.SessionInfos.FindAsync(id);
            if (session == null) return false;
            _context.SessionInfos.Remove(session);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignSpeakerAsync(Guid sessionId, Guid speakerId)
        {
            var session = await _context.SessionInfos.FindAsync(sessionId);
            if (session == null) return false;
            session.SpeakerId = speakerId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
