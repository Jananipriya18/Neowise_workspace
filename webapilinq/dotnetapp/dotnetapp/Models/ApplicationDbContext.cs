using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models{
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Customer)
            .WithMany(c => c.Movies)
            .HasForeignKey(m => m.CustomerId);

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
            });
        }
    }
}
