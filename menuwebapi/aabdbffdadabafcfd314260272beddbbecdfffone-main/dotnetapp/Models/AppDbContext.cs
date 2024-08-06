using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItem>()
                .HasOne(r => r.Restaurant)
                .WithMany(m => MenuItems)
                .HasForeignKey(r => r.RestaurantId);

            // Seed data
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { Id = 1, Name = "The Gourmet Kitchen", Location = "123 Food Street, Flavor Town", PhoneNumber = "(123) 456-7890" },
                new Restaurant { Id = 2, Name = "Delicious Bites", Location = "456 Yummy Avenue, Taste City", PhoneNumber = "(987) 654-3210" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
