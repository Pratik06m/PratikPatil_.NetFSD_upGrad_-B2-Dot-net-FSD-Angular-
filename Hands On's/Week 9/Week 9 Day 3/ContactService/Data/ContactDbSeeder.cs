using ContactService.Models;

namespace ContactService.Data
{
    public static class ContactDbSeeder
    {
        public static void Seed(ContactDbContext context)
        {
            if (!context.Contacts.Any())
            {
                context.Contacts.AddRange(
                    new Contact { ContactId = 1, Name = "Rahul Sharma", Email = "rahul@example.com", Phone = "9876543210", CategoryId = 1 },
                    new Contact { ContactId = 2, Name = "Aisha Khan", Email = "aisha@example.com", Phone = "9123456780", CategoryId = 2 }
                );
                context.SaveChanges();
            }
        }
    }
}
