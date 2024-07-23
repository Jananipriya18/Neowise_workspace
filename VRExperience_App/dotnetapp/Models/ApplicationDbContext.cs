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
                .HasOne(a => a.VRExperience)
                .WithMany(v => v.Attendees)
                .HasForeignKey(a => a.VRExperienceID);

            // Seed data for Classes
            modelBuilder.Entity<VRExperience>().HasData(
                new VRExperience { VRExperienceID = 1, ExperienceName = "Virtual Space Exploration", StartTime = "2023-01-01T10:00:00", EndTime = "2023-01-01T12:00:00", MaxCapacity = 10, Location = "Virtual", Description = "Explore the wonders of space in a fully immersive virtual reality experience." },
                new VRExperience { VRExperienceID = 2, ExperienceName = "Historical VR Tour", StartTime = "2023-01-11T10:00:00", EndTime = "2023-01-11T13:00:00", MaxCapacity = 10, Location = "Virtual", Description = "Take a tour through history with this engaging VR experience." },
                new VRExperience { VRExperienceID = 3, ExperienceName = "Underwater Adventure", StartTime = "2023-02-01T14:00:00", EndTime = "2023-02-01T16:00:00", MaxCapacity = 10, Location = "Virtual", Description = "Dive into the depths of the ocean and explore underwater life like never before." },
                new VRExperience { VRExperienceID = 4, ExperienceName = "Mountain Climbing Expedition", StartTime = "2023-02-15T09:00:00", EndTime = "2023-02-15T11:00:00", MaxCapacity = 10, Location = "Virtual", Description = "Experience the thrill of mountain climbing from the safety of your home." },
                new VRExperience { VRExperienceID = 5, ExperienceName = "Ancient Rome Tour", StartTime = "2023-03-01T10:00:00", EndTime = "2023-03-01T12:00:00", MaxCapacity = 10, Location = "Virtual", Description = "Travel back in time to ancient Rome and witness its grandeur and history." }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
