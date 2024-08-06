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
                .HasForeignKey(b => b.playlistId);

            modelBuilder.Entity<Playlist>().HasData(
                new Playlist
                {
                    Id = 1,
                    Name = "John Doe",
                    Description = "description 1"
                },
                new Playlist
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Description = "description 2"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
