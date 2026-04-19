using CategoryService.Data;
using CategoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDbContext _context;
        public CategoryRepository(CategoryDbContext context) { _context = context; }

        public async Task<List<Category>> GetAllAsync() => await _context.Categories.OrderBy(x => x.CategoryId).ToListAsync();
        public async Task<Category?> GetByIdAsync(int id) => await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(int id, Category category)
        {
            var existing = await _context.Categories.FindAsync(id);
            if (existing == null) return false;
            existing.CategoryName = category.CategoryName;
            existing.Description = category.Description;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Categories.FindAsync(id);
            if (existing == null) return false;
            _context.Categories.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
