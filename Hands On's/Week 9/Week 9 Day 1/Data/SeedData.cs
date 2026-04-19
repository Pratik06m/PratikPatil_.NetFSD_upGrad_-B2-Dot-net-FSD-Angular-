using Microsoft.AspNetCore.Identity;
using Week9_Day1_ContactManagementApi.Models;

namespace Week9_Day1_ContactManagementApi.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var hasher = new PasswordHasher<UserInfo>();

                var admin = new UserInfo { EmailId = "admin@example.com", Role = "Admin" };
                admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");

                var user = new UserInfo { EmailId = "user@example.com", Role = "User" };
                user.PasswordHash = hasher.HashPassword(user, "User@123");

                context.Users.AddRange(admin, user);
            }

            if (!context.Contacts.Any())
            {
                context.Contacts.AddRange(
                    new Contact { Name = "Rahul Sharma", Email = "rahul@example.com", Phone = "9876543210" },
                    new Contact { Name = "Aisha Khan", Email = "aisha@example.com", Phone = "9123456780" }
                );
            }

            context.SaveChanges();
        }
    }
}
