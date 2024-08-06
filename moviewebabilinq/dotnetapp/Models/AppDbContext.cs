using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Movies)
                .WithOne(m => m.Customer)
                .HasForeignKey(m => m.CustomerId);

            // Seed data
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Alice Johnson",
                    Email = "alice.johnson@example.com",
                    PhoneNumber = "123-456-7890"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Bob Smith",
                    Email = "bob.smith@example.com",
                    PhoneNumber = "987-654-3210"
                }
            );
        }
    }
}
