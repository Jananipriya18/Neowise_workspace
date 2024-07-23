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
            modelBuilder.Entity<VRExperience>().HasData(
                new VRExperience { VRExperienceID = 1, ExperienceName = "Virtual Space Exploration", StartTime = new DateTime(2023, 1, 1, 10, 0, 0), EndTime = new DateTime(2023, 1, 1, 12, 0, 0), MaxCapacity = 10, Location = "Virtual", Description = "Explore the wonders of space in a fully immersive virtual reality experience." },
                new VRExperience { VRExperienceID = 2, ExperienceName = "Historical VR Tour", StartTime = new DateTime(2023, 1, 11, 10, 0, 0), EndTime = new DateTime(2023, 1, 11, 13, 0, 0), MaxCapacity = 10, Location = "Virtual", Description = "Take a tour through history with this engaging VR experience." },
                new VRExperience { VRExperienceID = 3, ExperienceName = "Underwater Adventure", StartTime = new DateTime(2023, 2, 1, 14, 0, 0), EndTime = new DateTime(2023, 2, 1, 16, 0, 0), MaxCapacity = 10, Location = "Virtual", Description = "Dive into the depths of the ocean and explore underwater life like never before." },
                new VRExperience { VRExperienceID = 4, ExperienceName = "Mountain Climbing Expedition", StartTime = new DateTime(2023, 2, 15, 9, 0, 0), EndTime = new DateTime(2023, 2, 15, 11, 0, 0), MaxCapacity = 10, Location = "Virtual", Description = "Experience the thrill of mountain climbing from the safety of your home." },
                new VRExperience { VRExperienceID = 5, ExperienceName = "Ancient Rome Tour", StartTime = new DateTime(2023, 3, 1, 10, 0, 0), EndTime = new DateTime(2023, 3, 1, 12, 0, 0), MaxCapacity = 10, Location = "Virtual", Description = "Travel back in time to ancient Rome and witness its grandeur and history." }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
