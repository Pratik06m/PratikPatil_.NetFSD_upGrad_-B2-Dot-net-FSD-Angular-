using DAL.DataAccess;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EMSDbContext _context;

        public UserRepository(EMSDbContext context)
        {
            _context = context;
        }

        public async Task<UserInfo?> GetByEmailAsync(string email)
        {
            return await _context.UserInfos.FindAsync(email);
        }

        public async Task<IEnumerable<UserInfo>> GetAllAsync()
        {
            return await _context.UserInfos.ToListAsync();
        }

        public async Task<bool> AddAsync(UserInfo user)
        {
            if (await EmailExistsAsync(user.EmailId))
                return false;
            _context.UserInfos.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UserInfo user)
        {
            var existing = await _context.UserInfos.FindAsync(user.EmailId);
            if (existing == null) return false;
            existing.UserName = user.UserName;
            existing.Password = user.Password;
            existing.Role = user.Role;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string email)
        {
            var user = await _context.UserInfos.FindAsync(email);
            if (user == null) return false;
            _context.UserInfos.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserInfo?> AuthenticateAsync(string email, string password)
        {
            return await _context.UserInfos
                .FirstOrDefaultAsync(u => u.EmailId == email && u.Password == password);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.UserInfos.AnyAsync(u => u.EmailId == email);
        }
    }
}
