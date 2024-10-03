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

        public DbSet<Customer> Customers { get; set; }  // Represents the Customers table
        public DbSet<Spice> Spices { get; set; }  // Represents the Spices table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the one-to-many relationship between Customer and Spice
            modelBuilder.Entity<Spice>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Spices)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);  // Set foreign key to null if Customer is deleted

            base.OnModelCreating(modelBuilder);
        }
    }
}
