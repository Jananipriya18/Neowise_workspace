using Microsoft.EntityFrameworkCore;

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
                .HasMany(s => s.Playlists)
                .WithMany(p => p.Songs)
                .UsingEntity(j => j.ToTable("PlaylistSongs"));

            modelBuilder.Entity<Playlist>().HasData(
                new Playlist
                {
                    Id = 1,
                    Name = "Top Hits",
                    Description = "The best songs of the year"
                },
                new Playlist
                {
                    Id = 2,
                    Name = "Chill Vibes",
                    Description = "Relaxing and soothing tracks"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
