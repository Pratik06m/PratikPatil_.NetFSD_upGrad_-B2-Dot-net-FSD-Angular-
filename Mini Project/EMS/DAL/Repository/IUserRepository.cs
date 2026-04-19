using DAL.Models;

namespace DAL.Repository
{
    public interface IUserRepository
    {
        Task<UserInfo?> GetByEmailAsync(string email);
        Task<IEnumerable<UserInfo>> GetAllAsync();
        Task<bool> AddAsync(UserInfo user);
        Task<bool> UpdateAsync(UserInfo user);
        Task<bool> DeleteAsync(string email);
        Task<UserInfo?> AuthenticateAsync(string email, string password);
        Task<bool> EmailExistsAsync(string email);
    }
}
