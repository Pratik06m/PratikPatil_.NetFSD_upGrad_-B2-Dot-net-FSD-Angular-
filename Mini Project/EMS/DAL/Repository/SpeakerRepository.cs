using DAL.DataAccess;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly EMSDbContext _context;

        public SpeakerRepository(EMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SpeakersDetails>> GetAllAsync()
        {
            return await _context.SpeakersDetails.OrderBy(s => s.SpeakerName).ToListAsync();
        }

        public async Task<SpeakersDetails?> GetByIdAsync(Guid id)
        {
            return await _context.SpeakersDetails.FindAsync(id);
        }

        public async Task<bool> AddAsync(SpeakersDetails speaker)
        {
            speaker.SpeakerId = Guid.NewGuid();
            _context.SpeakersDetails.Add(speaker);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var speaker = await _context.SpeakersDetails.FindAsync(id);
            if (speaker == null) return false;
            _context.SpeakersDetails.Remove(speaker);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
