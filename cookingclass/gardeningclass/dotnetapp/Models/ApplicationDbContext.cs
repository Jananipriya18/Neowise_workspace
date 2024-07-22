using Microsoft.EntityFrameworkCore;
using System;

namespace dotnetapp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships between entities
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassID);

            // Seed data for Classes
            modelBuilder.Entity<Class>().HasData(
                new Class { ClassID = 1, ClassName = "Italian Cooking Basics", StartTime = "2023-01-01 10:00 AM", EndTime = "2023-01-01 12:00 PM", Capacity = 8 },
                new Class { ClassID = 2, ClassName = "French Pastry Making", StartTime = "2023-01-11 10:00 AM", EndTime = "2023-01-11 01:00 PM", Capacity = 8 },
                new Class { ClassID = 3, ClassName = "Sushi Rolling Techniques", StartTime = "2023-01-21 10:00 AM", EndTime = "2023-01-21 12:00 PM", Capacity = 8 },
                new Class { ClassID = 4, ClassName = "Indian Curry Mastery", StartTime = "2023-01-31 10:00 AM", EndTime = "2023-01-31 12:00 PM", Capacity = 8 },
                new Class { ClassID = 5, ClassName = "Mexican Street Food", StartTime = "2023-02-10 10:00 AM", EndTime = "2023-02-10 12:00 PM", Capacity = 8 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
