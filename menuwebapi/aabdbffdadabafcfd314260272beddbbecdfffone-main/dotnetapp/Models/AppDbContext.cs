using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Restaurant)
                .WithMany(lc => lc.Books)
                .HasForeignKey(b => b.RestaurantId);

            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant
                {
                    Id = 1,
                    CardNumber = "LC-12345",
                    MemberName = "John Doe",
                    ExpiryDate = new DateTime(2025, 12, 31)
                },
                new Restaurant
                {
                    Id = 2,
                    CardNumber = "LC-54321",
                    MemberName = "Jane Smith",
                    ExpiryDate = new DateTime(2024, 10, 15)
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
