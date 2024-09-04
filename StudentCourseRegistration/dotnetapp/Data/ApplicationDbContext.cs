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

        // DbSets for MusicRecord and Order entities
        public DbSet<MusicRecord> MusicRecords { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<MusicRecord>()
                .HasOne(mr => mr.Order)
                .WithMany(o => o.MusicRecords)
                .HasForeignKey(mr => mr.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }

    }
}
