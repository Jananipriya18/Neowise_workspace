// using Microsoft.EntityFrameworkCore;
// using System;

// namespace dotnetapp.Models
// {
//     public class ApplicationDbContext : DbContext
//     {
//         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//         {
//         }

//         public DbSet<Class> Classes { get; set; }
//         public DbSet<Student> Students { get; set; }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             // Configure the relationships between entities
//             modelBuilder.Entity<Student>()
//                 .HasOne(s => s.Class)
//                 .WithMany(c => c.Students)
//                 .HasForeignKey(s => s.ClassID);

//             // Seed data for Classes
//             modelBuilder.Entity<Class>().HasData(
//                 new Class { ClassID = 1, ClassName = "Italian Cooking Basics", StartTime = "2023-01-01 10:00 AM", EndTime = "2023-01-01 12:00 PM", Capacity = 8 },
//                 new Class { ClassID = 2, ClassName = "French Pastry Making", StartTime = "2023-01-11 10:00 AM", EndTime = "2023-01-11 01:00 PM", Capacity = 8 },
//                 new Class { ClassID = 3, ClassName = "Sushi Rolling Techniques", StartTime = "2023-01-21 10:00 AM", EndTime = "2023-01-21 12:00 PM", Capacity = 8 },
//                 new Class { ClassID = 4, ClassName = "Indian Curry Mastery", StartTime = "2023-01-31 10:00 AM", EndTime = "2023-01-31 12:00 PM", Capacity = 8 },
//                 new Class { ClassID = 5, ClassName = "Mexican Street Food", StartTime = "2023-02-10 10:00 AM", EndTime = "2023-02-10 12:00 PM", Capacity = 8 }
//             );

//             base.OnModelCreating(modelBuilder);
//         }
//     }
// }


using Microsoft.EntityFrameworkCore;
using System;

namespace dotnetapp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<HistoricalTour> HistoricalTours { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships between entities
            modelBuilder.Entity<Participant>()
                .HasOne(p => p.HistoricalTour)
                .WithMany(t => t.Participants)
                .HasForeignKey(p => p.HistoricalTourID);

            // Seed data for HistoricalTours
            modelBuilder.Entity<HistoricalTour>().HasData(
                new HistoricalTour { HistoricalTourID = 1, TourName = "Ancient Rome Tour", StartTime = "2023-01-01 10:00 AM", EndTime = "2023-01-01 12:00 PM", Capacity = 20, Location = "Rome, Italy", Description = "Explore the ancient ruins of Rome including the Colosseum, Roman Forum, and Palatine Hill." },
                new HistoricalTour { HistoricalTourID = 2, TourName = "Egyptian Pyramids Adventure", StartTime = "2023-01-11 10:00 AM", EndTime = "2023-01-11 01:00 PM", Capacity = 15, Location = "Giza, Egypt", Description = "Discover the mysteries of the Great Pyramids and the Sphinx." },
                new HistoricalTour { HistoricalTourID = 3, TourName = "Medieval Castles Tour", StartTime = "2023-01-21 10:00 AM", EndTime = "2023-01-21 12:00 PM", Capacity = 25, Location = "Various Locations, England", Description = "Visit some of England's most iconic medieval castles and learn about their history." },
                new HistoricalTour { HistoricalTourID = 4, TourName = "Renaissance Florence Tour", StartTime = "2023-01-31 10:00 AM", EndTime = "2023-01-31 12:00 PM", Capacity = 20, Location = "Florence, Italy", Description = "Explore the art and architecture of Renaissance Florence, including the Duomo and Uffizi Gallery." },
                new HistoricalTour { HistoricalTourID = 5, TourName = "American Revolution Tour", StartTime = "2023-02-10 10:00 AM", EndTime = "2023-02-10 12:00 PM", Capacity = 30, Location = "Boston, USA", Description = "Learn about the events leading up to the American Revolution, including visits to historic sites like the Boston Tea Party Ships and Museum." }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
