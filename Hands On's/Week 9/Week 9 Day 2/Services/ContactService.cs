using Microsoft.Extensions.Caching.Memory;
using Week9_Day2_ContactManagementAPI.Models;
using Week9_Day2_ContactManagementAPI.Repositories;

namespace Week9_Day2_ContactManagementAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IMemoryCache _cache;

        public ContactService(IContactRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<List<Contact>> GetAllContactsAsync()
        {
            string cacheKey = "contact_list";

            if (!_cache.TryGetValue(cacheKey, out List<Contact>? contacts))
            {
                contacts = await _repository.GetAllAsync();

                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                _cache.Set(cacheKey, contacts, options);
                Console.WriteLine("Stored contact list in cache.");
            }
            else
            {
                Console.WriteLine("Fetching contact list from cache...");
            }

            return contacts ?? new List<Contact>();
        }

        public async Task<Contact?> GetContactByIdAsync(int id)
        {
            string cacheKey = $"contact_{id}";

            if (!_cache.TryGetValue(cacheKey, out Contact? contact))
            {
                contact = await _repository.GetByIdAsync(id);

                if (contact != null)
                {
                    _cache.Set(cacheKey, contact, TimeSpan.FromSeconds(60));
                    Console.WriteLine($"Stored contact {id} in cache.");
                }
            }
            else
            {
                Console.WriteLine($"Fetching contact {id} from cache...");
            }

            return contact;
        }

        public async Task<PagedResponse<List<Contact>>> GetPagedContactsAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                pageNumber = 1;

            if (pageSize <= 0)
                pageSize = 5;

            int totalRecords = await _repository.CountAsync();
            var data = await _repository.GetPagedAsync(pageNumber, pageSize);

            return new PagedResponse<List<Contact>>
            {
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Data = data
            };
        }
    }
}
