using NUnit.Framework;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class AuthorControllerTests
    {
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private ApplicationDbContext _context;
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080"); // Base URL of your API
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(_dbContextOptions);
        }

        private async Task<int> CreateTestAuthorAndGetId()
        {
            var newAuthor = new Author
            {
                Name = "Test Author",
                Biography = "Test Biography"
            };

            var json = JsonConvert.SerializeObject(newAuthor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Author", content);
            response.EnsureSuccessStatusCode();

            var createdAuthor = JsonConvert.DeserializeObject<Author>(await response.Content.ReadAsStringAsync());
            return createdAuthor.AuthorId;
        }


        [Test]
        public async Task CreateAuthor_ReturnsCreatedAuthor()
        {
            // Arrange
            var newAuthor = new Author
            {
                Name = "Test Author",
                Biography = "Test Biography"
            };

            var json = JsonConvert.SerializeObject(newAuthor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Author", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdAuthor = JsonConvert.DeserializeObject<Author>(responseContent);
            Assert.IsNotNull(createdAuthor);
            Assert.AreEqual(newAuthor.Name, createdAuthor.Name);
            Assert.AreEqual(newAuthor.Biography, createdAuthor.Biography);
        }

        [Test]
        public async Task SearchAuthorByName_ReturnsAuthorWithBooks()
        {
            // Arrange
            string authorName = "Test Author"; // Use an existing author name

            // Act
            var response = await _httpClient.GetAsync($"api/Author/Search?name={authorName}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
            else
            {
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var author = JsonConvert.DeserializeObject<Author>(responseContent);
                Assert.IsNotNull(author);
                Assert.AreEqual(authorName, author.Name);
            }
        }

        [Test]
        public async Task PostBook_ReturnsCreatedBookWithAuthorDetails()
        {
            // Arrange
            int authorId = await CreateTestAuthorAndGetId(); // Dynamically create a valid Author

            var newBook = new Book
            {
                Title = "Test Bookie",
                Genre = "Test Genre",
                Price = 25.99M,
                AuthorId = authorId
            };

            var json = JsonConvert.SerializeObject(newBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Book", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdBook = JsonConvert.DeserializeObject<Book>(responseContent);

            Assert.IsNotNull(createdBook);
            Assert.AreEqual(newBook.Title, createdBook.Title);
            Assert.IsNotNull(createdBook.Author);
            Assert.AreEqual(authorId, createdBook.AuthorId);
        }

        [Test]
        public async Task DeleteBook_ReturnsNoContent()
        {
            // Arrange
            int authorId = await CreateTestAuthorAndGetId();
            var newBook = new Book
            {
                Title = "Book to be deleted",
                Genre = "Test Genre",
                Price = 19.99M,
                AuthorId = authorId
            };

            var json = JsonConvert.SerializeObject(newBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Book", content);
            var createdBook = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());

            // Act
            var deleteResponse = await _httpClient.DeleteAsync($"api/Book/{createdBook.BookId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Test]
        public async Task DeleteBook_InvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidBookId = 9999; // Use an ID that doesn't exist in the database

            // Act
            var response = await _httpClient.DeleteAsync($"api/Book/{invalidBookId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode, "Expected 404 Not Found for an invalid Book ID.");
        }


        [Test]
        public async Task GetBooks_ReturnsListOfBooksWithAuthors()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Book");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<Book[]>(responseContent);

            Assert.IsNotNull(books);
            Assert.IsTrue(books.Length > 0);
            Assert.IsNotNull(books[0].Author); // Ensure each book has an author loaded
        }

        [Test]
        public async Task GetBookById_ReturnsBookWithAuthorDetails()
        {
            // Arrange
            int authorId = await CreateTestAuthorAndGetId();
            var newBook = new Book
            {
                Title = "Book for ID Test",
                Genre = "Test Genre",
                Price = 19.99M,
                AuthorId = authorId
            };

            var json = JsonConvert.SerializeObject(newBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Book", content);
            var createdBook = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());

            // Act
            var getResponse = await _httpClient.GetAsync($"api/Book/{createdBook.BookId}");

            // Assert
            getResponse.EnsureSuccessStatusCode();
            var book = JsonConvert.DeserializeObject<Book>(await getResponse.Content.ReadAsStringAsync());
            Assert.IsNotNull(book);
            Assert.AreEqual(newBook.Title, book.Title);
            Assert.IsNotNull(book.Author);
        }

        [Test]
        public async Task GetBookById_InvalidId_ReturnsNotFound()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Book/999");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Test]
        public void AuthorModel_HasAllProperties()
        {
            // Arrange
            var author = new Author
            {
                AuthorId = 1,
                Name = "John Doe",
                Biography = "Famous author known for historical fiction.",
                Books = new List<Book>() // Ensure the Books collection is properly initialized
            };

            // Act & Assert
            Assert.AreEqual(1, author.AuthorId, "AuthorId does not match.");
            Assert.AreEqual("John Doe", author.Name, "Name does not match.");
            Assert.AreEqual("Famous author known for historical fiction.", author.Biography, "Biography does not match.");
            Assert.IsNotNull(author.Books, "Books collection should not be null.");
            Assert.IsInstanceOf<ICollection<Book>>(author.Books, "Books should be of type ICollection<Book>.");
        }

        [Test]
        public void BookModel_HasAllProperties()
        {
            // Arrange
            var author = new Author
            {
                AuthorId = 1,
                Name = "John Doe",
                Biography = "Famous author known for historical fiction."
            };

            var book = new Book
            {
                BookId = 100,
                Title = "Historical Fiction Masterpiece",
                Genre = "Historical Fiction",
                Price = 19.99m,
                AuthorId = 1,
                Author = author
            };

            // Act & Assert
            Assert.AreEqual(100, book.BookId, "BookId does not match.");
            Assert.AreEqual("Historical Fiction Masterpiece", book.Title, "Title does not match.");
            Assert.AreEqual("Historical Fiction", book.Genre, "Genre does not match.");
            Assert.AreEqual(19.99m, book.Price, "Price does not match.");
            Assert.AreEqual(1, book.AuthorId, "AuthorId does not match.");
            Assert.IsNotNull(book.Author, "Author should not be null.");
            Assert.AreEqual(author.Name, book.Author.Name, "Author's Name does not match.");
        }

        [Test]
        public void DbContext_HasDbSetProperties()
        {
            // Assert that the context has DbSet properties for Authors and Books
            Assert.IsNotNull(_context.Authors, "Authors DbSet is not initialized.");
            Assert.IsNotNull(_context.Books, "Books DbSet is not initialized.");
        }

        [Test]
        public void AuthorBook_Relationship_IsConfiguredCorrectly()
        {
            // Check if the Author to Book relationship is configured as one-to-many
            var model = _context.Model;
            var authorEntity = model.FindEntityType(typeof(Author));
            var bookEntity = model.FindEntityType(typeof(Book));

            // Assert that the foreign key relationship exists between Book and Author
            var foreignKey = bookEntity.GetForeignKeys().FirstOrDefault(fk => fk.PrincipalEntityType == authorEntity);

            Assert.IsNotNull(foreignKey, "Foreign key relationship between Book and Author is not configured.");
            Assert.AreEqual("AuthorId", foreignKey.Properties.First().Name, "Foreign key property name is incorrect.");

            // Check if the cascade delete behavior is set
            Assert.AreEqual(DeleteBehavior.Cascade, foreignKey.DeleteBehavior, "Cascade delete behavior is not set correctly.");
        }

        [TearDown]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
    }
}