using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAccess
{
    public class EMSDbContext : DbContext
    {
        public EMSDbContext(DbContextOptions<EMSDbContext> options) : base(options) { }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<SpeakersDetails> SpeakersDetails { get; set; }
        public DbSet<SessionInfo> SessionInfos { get; set; }
        public DbSet<ParticipantEventDetails> ParticipantEventDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserInfo configuration
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(u => u.EmailId);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Role).IsRequired();
                entity.Property(u => u.Password).IsRequired().HasMaxLength(20);
            });

            // EventDetails configuration
            modelBuilder.Entity<EventDetails>(entity =>
            {
                entity.HasKey(e => e.EventId);
                entity.Property(e => e.EventName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.EventCategory).IsRequired().HasMaxLength(50);
                entity.Property(e => e.EventDate).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasDefaultValue("Active");
            });

            // SpeakersDetails configuration
            modelBuilder.Entity<SpeakersDetails>(entity =>
            {
                entity.HasKey(s => s.SpeakerId);
                entity.Property(s => s.SpeakerName).IsRequired().HasMaxLength(50);
            });

            // SessionInfo configuration
            modelBuilder.Entity<SessionInfo>(entity =>
            {
                entity.HasKey(s => s.SessionId);
                entity.Property(s => s.SessionTitle).IsRequired().HasMaxLength(50);

                // SessionInfo -> EventDetails (many-to-one)
                entity.HasOne(s => s.Event)
                      .WithMany(e => e.Sessions)
                      .HasForeignKey(s => s.EventId)
                      .OnDelete(DeleteBehavior.Cascade);

                // SessionInfo -> SpeakersDetails (many-to-one, optional)
                entity.HasOne(s => s.Speaker)
                      .WithMany(sp => sp.Sessions)
                      .HasForeignKey(s => s.SpeakerId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .IsRequired(false);
            });

            // ParticipantEventDetails configuration
            modelBuilder.Entity<ParticipantEventDetails>(entity =>
            {
                entity.HasKey(p => p.ID);

                // ParticipantEventDetails -> UserInfo
                entity.HasOne(p => p.Participant)
                      .WithMany(u => u.ParticipantEventDetails)
                      .HasForeignKey(p => p.ParticipantEmailId)
                      .OnDelete(DeleteBehavior.Cascade);

                // ParticipantEventDetails -> EventDetails
                entity.HasOne(p => p.Event)
                      .WithMany(e => e.ParticipantEventDetails)
                      .HasForeignKey(p => p.EventId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Unique constraint: one registration per participant per event
                entity.HasIndex(p => new { p.ParticipantEmailId, p.EventId }).IsUnique();
            });

            // Seed Admin user
            modelBuilder.Entity<UserInfo>().HasData(new UserInfo
            {
                EmailId = "admin@upgrad.com",
                UserName = "Admin",
                Role = "Admin",
                Password = "Admin@123"
            });
        }
    }
}
