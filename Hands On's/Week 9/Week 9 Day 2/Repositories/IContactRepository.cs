using Week9_Day2_ContactManagementAPI.Models;

namespace Week9_Day2_ContactManagementAPI.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(int id);
        Task<int> CountAsync();
        Task<List<Contact>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
