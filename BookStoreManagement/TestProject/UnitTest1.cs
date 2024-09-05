using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;
using System.Reflection;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class BooksControllerTests
    {
        private HttpClient _httpClient;
        private Assembly _assembly;

        private Book _testBook;
        private Author _testAuthor;

        [SetUp]
        public async Task Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080"); // Base URL of your API
        }

        private async Task<Book> CreateTestBook(int authorId)
        {
            var newBook = new Book
            {
                Title = "Test Title",
                Genre = "Test Genre",
                Price = 19.99M,
                AuthorId = authorId
            };

            var json = JsonConvert.SerializeObject(newBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Book", content);
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
        }

        [Test]
        public async Task CreateTestAuthor_ReturnsCreatedAuthor()
        {
            // Arrange
            var newAuthor = new Author
            {
                Name = "Test Name",
                Biography = "Test Biography"
            };

            var json = JsonConvert.SerializeObject(newAuthor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Author", content);
            response.EnsureSuccessStatusCode();

            // Assert
            var createdAuthorJson = await response.Content.ReadAsStringAsync();
            var createdAuthor = JsonConvert.DeserializeObject<Author>(createdAuthorJson);

            Assert.IsNotNull(createdAuthor);
            Assert.AreEqual(newAuthor.Name, createdAuthor.Name);
            Assert.AreEqual(newAuthor.Biography, createdAuthor.Biography);
        }

        // [Test]
        // public async Task CreateTestBook_ReturnsCreatedBook()
        // {
        //     // Arrange
        //     int validAuthorId = await CreateTestAuthorAndGetId(); // Dynamically get a valid AuthorId

        //     var newBook = new Book
        //     {
        //          Title = "Test Title",
        //         LoanDate = "2024-11-9",
        //         ReturnDate = "Test ReturnDate",
        //         AuthorId = validAuthorId // Use the valid AuthorId obtained from the helper method
        //     };

        //     var json = JsonConvert.SerializeObject(newBook);
        //     var content = new StringContent(json, Encoding.UTF8, "application/json");

        //     // Act
        //     var response = await _httpClient.PostAsync("api/Book", content);
        //     response.EnsureSuccessStatusCode();

        //     // Assert
        //     var createdBookJson = await response.Content.ReadAsStringAsync();
        //     var createdBook = JsonConvert.DeserializeObject<Book>(createdBookJson);

        //     Assert.IsNotNull(createdBook);
        //     Assert.AreEqual(newBook.Title, createdBook.Title);
        //     Assert.AreEqual(newBook.LoanDate, createdBook.LoanDate);
        //     Assert.AreEqual(newBook.ReturnDate, createdBook.ReturnDate);
        //     Assert.AreEqual(newBook.AuthorId, createdBook.AuthorId); // Ensure AuthorId matches
        // }



        // [Test]
        // public async Task Test_GetAllBooks_ReturnsListOfBooks()
        // {
        //     var response = await _httpClient.GetAsync("api/Book");
        //     response.EnsureSuccessStatusCode();

        //     var content = await response.Content.ReadAsStringAsync();
        //     var books = JsonConvert.DeserializeObject<Book[]>(content);

        //     Assert.IsNotNull(books);
        //     Assert.IsTrue(books.Length > 0);
        // }

        // [Test]
        // public async Task Test_GetAuthors_ReturnsListOfAuthors()
        // {
        //     var response = await _httpClient.GetAsync("api/Author");
        //     response.EnsureSuccessStatusCode();

        //     var content = await response.Content.ReadAsStringAsync();
        //     var authors = JsonConvert.DeserializeObject<Author[]>(content);

        //     Assert.IsNotNull(authors);
        //     Assert.IsTrue(authors.Length > 0);
        // }


        // [Test]
        // public async Task Test_GetBookById_InvalidId_ReturnsNotFound()
        // {
        //     var response = await _httpClient.GetAsync($"api/Book/999");

        //     Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        // }

        // [Test]
        // public async Task Test_GetAuthorId_InvalidId_ReturnsNotFound()
        // {
        //     var response = await _httpClient.GetAsync($"api/Author/999");

        //     Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        // }

        // private async Task<int> CreateTestAuthorAndGetId()
        // {
        //     var newAuthor = new Author
        //     {
        //         Name = "Test Name",
        //         Biography = "Biography" // Use a valid format
        //     };

        //     var json = JsonConvert.SerializeObject(newAuthor);
        //     var content = new StringContent(json, Encoding.UTF8, "application/json");

        //     var response = await _httpClient.PostAsync("api/Author", content);
        //     response.EnsureSuccessStatusCode();

        //     var createdAuthorJson = await response.Content.ReadAsStringAsync();
        //     var createdAuthor = JsonConvert.DeserializeObject<Author>(createdAuthorJson);

        //     return createdAuthor.AuthorId; // Return the ID of the created Author
        // }

        // [Test]
        // public async Task Test_AddBook_ReturnsCreatedResponse()
        // {
        //     // Arrange
        //     int validAuthorId = await CreateTestAuthorAndGetId(); // Use the helper method to get a valid AuthorId

        //     var newBook = new Book
        //     {
        //           Title = "Test Title",
        //           Genre = "Test Genre",
        //           Price = "10",
        //         AuthorId = validAuthorId// Use the valid AuthorId obtained from the helper method
        //     };

        //     var json = JsonConvert.SerializeObject(newBook);
        //     var content = new StringContent(json, Encoding.UTF8, "application/json");

        //     // Act
        //     var response = await _httpClient.PostAsync("api/Book", content);
        //     response.EnsureSuccessStatusCode();

        //     // Assert
        //     var createdBookJson = await response.Content.ReadAsStringAsync();
        //     var createdBook = JsonConvert.DeserializeObject<Book>(createdBookJson);

        //     Assert.IsNotNull(createdBook);
        //     Assert.AreEqual(newBook.Title, createdBook.Genre);
        //     Assert.AreEqual(newBook.AuthorId, createdBook.AuthorId); // Ensure AuthorId matches
        // }


        // [Test]
        // public async Task Test_AddAuthor_ReturnsCreatedResponse()
        // {
        //     // Arrange
        //     var newAuthor = new Author
        //     {
        //         Name = "Test Name",
        //         Biography = "Test Biography" // Ensure the date format matches your model's expectations
        //         // Initialize other properties if needed
        //     };

        //     var json = JsonConvert.SerializeObject(newAuthor);
        //     var content = new StringContent(json, Encoding.UTF8, "application/json");

        //     // Act
        //     var response = await _httpClient.PostAsync("api/Author", content);

        //     // Assert
        //     response.EnsureSuccessStatusCode(); // Ensure the response status is 200-299

        //     var createdAuthor = JsonConvert.DeserializeObject<Author>(await response.Content.ReadAsStringAsync());

        //     Assert.IsNotNull(createdAuthor, "The created author is null.");
        //     Assert.AreEqual(newAuthor.Name, createdAuthor.CustomerName, "Author names do not match.");
        //     Assert.AreEqual(newAuthor.Biography, createdAuthor.Biography, "Author Biography do not match.");
        //     // Add additional assertions as needed
        // }


        [TearDown]
        public async Task Cleanup()
        {
            if (_testBook != null)
            {
                var response = await _httpClient.DeleteAsync($"api/Book/{_testBook.BookId}");
                if (response.StatusCode != HttpStatusCode.NotFound)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            _httpClient.Dispose();
        }
    }
}