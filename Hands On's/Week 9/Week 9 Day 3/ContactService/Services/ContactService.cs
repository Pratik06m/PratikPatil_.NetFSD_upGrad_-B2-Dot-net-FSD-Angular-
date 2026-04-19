using ContactService.Models;
using ContactService.Repositories;

namespace ContactService.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        public ContactService(IContactRepository repository) { _repository = repository; }
        public Task<List<Contact>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Contact?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Contact> AddAsync(Contact contact) => _repository.AddAsync(contact);
        public Task<bool> UpdateAsync(int id, Contact contact) => _repository.UpdateAsync(id, contact);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
