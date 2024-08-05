namespace dotnetapp.Models
{
    public class AppDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes it to the base class constructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Define DbSet properties to represent the collections of Book and LibraryCard entities in the database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {s
            // Configuring the relationship between Book and LibraryCard
            // A Book has one LibraryCard
            // A LibraryCard can have many Books
            // The foreign key in the Book entity is LibraryCardId

            // Seeding initial data into the LibraryCard table
            // Unique identifier for the library card
            // Card number
            // Name of the cardholder
            // Expiry date of the library card
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
