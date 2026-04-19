using Week9_Day2_ContactManagementAPI.Models;

namespace Week9_Day2_ContactManagementAPI.Services
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllContactsAsync();
        Task<Contact?> GetContactByIdAsync(int id);
        Task<PagedResponse<List<Contact>>> GetPagedContactsAsync(int pageNumber, int pageSize);
    }
}
