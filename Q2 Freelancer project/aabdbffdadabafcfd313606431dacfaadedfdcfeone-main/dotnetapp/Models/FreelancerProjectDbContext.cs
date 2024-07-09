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
    }
}
