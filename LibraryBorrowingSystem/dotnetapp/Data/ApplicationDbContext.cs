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

        public DbSet<BookLoan> BookLoans { get; set; }  
        public DbSet<LibraryManager> LibraryManagers { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.LibraryManager)
                .WithMany(lm => lm.BookLoans)
                .HasForeignKey(bl => bl.LibraryManagerId)
                .OnDelete(DeleteBehavior.Cascade);  

            base.OnModelCreating(modelBuilder);
        }
    }
}
