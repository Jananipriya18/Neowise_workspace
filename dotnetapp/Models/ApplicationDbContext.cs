using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace dotnetapp.Models
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Shop> Shops { get; set; }
    }
}
