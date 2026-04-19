using Microsoft.EntityFrameworkCore;
using Week9_Day1_ContactManagementApi.Models;

namespace Week9_Day1_ContactManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<UserInfo> Users => Set<UserInfo>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(x => x.ContactId);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Email).IsRequired().HasMaxLength(150);
                entity.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(x => x.UserInfoId);
                entity.HasIndex(x => x.EmailId).IsUnique();
                entity.Property(x => x.EmailId).IsRequired().HasMaxLength(150);
                entity.Property(x => x.PasswordHash).IsRequired();
                entity.Property(x => x.Role).IsRequired().HasMaxLength(20);
            });
        }
    }
}
