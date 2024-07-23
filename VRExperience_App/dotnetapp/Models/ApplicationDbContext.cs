using Microsoft.EntityFrameworkCore;
using System;

namespace dotnetapp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<VRExperience> VRExperiences { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships between entities
            modelBuilder.Entity<Attendee>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Attendees)
                .HasForeignKey(s => s.VRExperienceID);

            // Seed data for Classes
            modelBuilder.Entity<Class>().HasData(
                new Class { VRExperienceID = 1, ExperienceName = "Italian Cooking Basics", StartTime = "2023-01-01 10:00 AM", EndTime = "2023-01-01 12:00 PM", MaxCapacity = 8 },
                new Class { VRExperienceID = 2, ExperienceName = "French Pastry Making", StartTime = "2023-01-11 10:00 AM", EndTime = "2023-01-11 01:00 PM", MaxCapacity = 8 },
                new Class { VRExperienceID = 3, ExperienceName = "Sushi Rolling Techniques", StartTime = "2023-01-21 10:00 AM", EndTime = "2023-01-21 12:00 PM", MaxCapacity = 8 },
                new Class { VRExperienceID = 4, ExperienceName = "Indian Curry Mastery", StartTime = "2023-01-31 10:00 AM", EndTime = "2023-01-31 12:00 PM", MaxCapacity = 8 },
                new Class { VRExperienceID = 5, ExperienceName = "Mexican Street Food", StartTime = "2023-02-10 10:00 AM", EndTime = "2023-02-10 12:00 PM", MaxCapacity = 8 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
