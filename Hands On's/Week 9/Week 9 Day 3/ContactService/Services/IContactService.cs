using ContactService.Models;

namespace ContactService.Services
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(int id);
        Task<Contact> AddAsync(Contact contact);
        Task<bool> UpdateAsync(int id, Contact contact);
        Task<bool> DeleteAsync(int id);
    }
}
