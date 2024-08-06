using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.MenuItems)
                .WithOne(mi => mi.Restaurant)
                .HasForeignKey(mi => mi.RestaurantId);

            // Seed data
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { Id = 1, Name = "The Gourmet Kitchen", Location = "123 Food Street, Flavor Town", PhoneNumber = "(123) 456-7890" },
                new Restaurant { Id = 2, Name = "Delicious Bites", Location = "456 Yummy Avenue, Taste City", PhoneNumber = "(987) 654-3210" }
            );
        }
    }
}
