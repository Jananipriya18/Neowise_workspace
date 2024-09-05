using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }  // Represents the Events table
        public DbSet<Attendee> Attendees { get; set; }  // Represents the Attendees table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the one-to-many relationship between Event and Attendee
            modelBuilder.Entity<Attendee>()
                .HasOne(a => a.Event)
                .WithMany(e => e.Attendees)
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete: Deleting an event deletes all associated attendees

            base.OnModelCreating(modelBuilder);
        }
    }
}
