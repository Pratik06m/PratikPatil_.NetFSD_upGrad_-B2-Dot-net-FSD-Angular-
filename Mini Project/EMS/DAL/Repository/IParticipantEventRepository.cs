using DAL.Models;

namespace DAL.Repository
{
    public interface IParticipantEventRepository
    {
        Task<IEnumerable<ParticipantEventDetails>> GetByParticipantAsync(string email);
        Task<IEnumerable<ParticipantEventDetails>> GetByEventAsync(Guid eventId);
        Task<ParticipantEventDetails?> GetByIdAsync(Guid id);
        Task<bool> RegisterAsync(ParticipantEventDetails registration);
        Task<bool> UnregisterAsync(Guid id);
        Task<bool> MarkAttendanceAsync(Guid id, bool isAttended);
        Task<bool> IsRegisteredAsync(string email, Guid eventId);
    }
}
