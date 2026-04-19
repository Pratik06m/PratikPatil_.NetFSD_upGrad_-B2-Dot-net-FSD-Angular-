using CategoryService.Models;
using CategoryService.Repositories;

namespace CategoryService.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository) { _repository = repository; }
        public Task<List<Category>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Category?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Category> AddAsync(Category category) => _repository.AddAsync(category);
        public Task<bool> UpdateAsync(int id, Category category) => _repository.UpdateAsync(id, category);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
