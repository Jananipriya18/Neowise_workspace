using Microsoft.EntityFrameworkCore;
namespace dotnetapp.Models
{
    public class FreelancerProjectDbContext : DbContext
    {
        public FreelancerProjectDbContext(DbContextOptions<FreelancerProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between Freelancer and Project
            modelBuilder.Entity<Freelancer>()
                .HasMany(f => f.Projects)
                .WithOne(p => p.Freelancer)
                .HasForeignKey(p => p.FreelancerID);
        }
    }
}
