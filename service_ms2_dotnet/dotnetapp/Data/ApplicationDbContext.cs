using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ServiceCenter> ServiceCenters { get; set; }  // Represents the ServiceCenters table
        public DbSet<ServiceBooking> ServiceBookings { get; set; }  // Represents the ServiceBookings table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceBooking>()
                .HasOne(sb => sb.ServiceCenter)
                .WithMany(sc => sc.ServiceBookings)
                .HasForeignKey(sb => sb.ServiceCenterId);
        }
    }
}
