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

        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>()
                .HasOne(b => b.Playlist)
                .WithMany(lc => lc.Songs)
                .HasForeignKey(b => b.Id);

            modelBuilder.Entity<Playlist>().HasData(
                new Playlist
                {
                    Id = 1,
                    Name = "LC-12345",
                    MemberName = "John Doe",
                    ExpiryDate = new DateTime(2025, 12, 31)
                },
                new Playlist
                {
                    Id = 2,
                    Name = "LC-54321",
                    MemberName = "Jane Smith",
                    ExpiryDate = new DateTime(2024, 10, 15)
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
