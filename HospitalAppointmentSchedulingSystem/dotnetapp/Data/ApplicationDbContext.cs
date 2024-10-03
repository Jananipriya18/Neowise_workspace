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

        public DbSet<Doctor> Doctors { get; set; }  // Represents the Doctors table
        public DbSet<Appointment> Appointments { get; set; }  // Represents the Appointments table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the one-to-many relationship between Doctor and Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete: Deleting a doctor deletes all associated appointments

            base.OnModelCreating(modelBuilder);
        }
    }
}
