using DAL.Models;

namespace DAL.Repository
{
    public interface ISessionRepository
    {
        Task<IEnumerable<SessionInfo>> GetAllAsync();
        Task<IEnumerable<SessionInfo>> GetByEventIdAsync(Guid eventId);
        Task<SessionInfo?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(SessionInfo session);
        Task<bool> UpdateAsync(SessionInfo session);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AssignSpeakerAsync(Guid sessionId, Guid speakerId);
    }
}
