using DAL.Models;

namespace DAL.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<EventDetails>> GetAllAsync();
        Task<IEnumerable<EventDetails>> GetActiveAsync();
        Task<EventDetails?> GetByIdAsync(Guid id);
        Task<EventDetails?> GetWithSessionsAsync(Guid id);
        Task<IEnumerable<EventDetails>> GetByCategoryAsync(string category);
        Task<bool> AddAsync(EventDetails eventDetails);
        Task<bool> UpdateAsync(EventDetails eventDetails);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ToggleStatusAsync(Guid id);
        Task<IEnumerable<string>> GetCategoriesAsync();
    }
}
