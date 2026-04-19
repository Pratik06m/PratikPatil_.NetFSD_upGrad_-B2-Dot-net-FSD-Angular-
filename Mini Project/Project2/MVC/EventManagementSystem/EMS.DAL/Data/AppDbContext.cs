using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using EMS.DAL.Models;

namespace EMS.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserInfo> Users { get; set; }
        public DbSet<EventDetails> Events { get; set; }
        public DbSet<SessionInfo> Sessions { get; set; }
        public DbSet<SpeakersDetails> Speakers { get; set; }
        public DbSet<ParticipantEventDetails> ParticipantEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example Fluent API
            modelBuilder.Entity<UserInfo>()
                .HasKey(u => u.EmailId);

            modelBuilder.Entity<ParticipantEventDetails>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.ParticipantEmailId);

            modelBuilder.Entity<ParticipantEventDetails>()
                .HasOne(p => p.Event)
                .WithMany()
                .HasForeignKey(p => p.EventId);
        }
    }
}