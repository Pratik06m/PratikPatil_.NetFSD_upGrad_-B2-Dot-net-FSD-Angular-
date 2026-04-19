using DAL.Models;

namespace DAL.Repository
{
    public interface ISpeakerRepository
    {
        Task<IEnumerable<SpeakersDetails>> GetAllAsync();
        Task<SpeakersDetails?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(SpeakersDetails speaker);
        Task<bool> DeleteAsync(Guid id);
    }
}
