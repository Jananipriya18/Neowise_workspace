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
    public class BookLoansControllerTests
    {
        private HttpClient _httpClient;
        private Assembly _assembly;

        private BookLoan _testBookLoan;
        private Author _testAuthor;

        [SetUp]
        public async Task Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080"); // Base URL of your API
        }

        private async Task<BookLoan> CreateTestBookLoan()
        {
            var newBookLoan = new BookLoan
            {
                BookTitle = "Test BookTitle",
                LoanDate = "2024-11-9",
                ReturnDate = "Test ReturnDate"
            };

            var json = JsonConvert.SerializeObject(newBookLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/BookLoan", content);
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<BookLoan>(await response.Content.ReadAsStringAsync());
        }

        [Test]
        public async Task CreateTestAuthor_ReturnsCreatedAuthor()
        {
            // Arrange
            var newAuthor = new Author
            {
                Name = "Test Name",
                Biography = "Test Biography" // Format to match the string format in the model
                // Initialize other properties if needed
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

        [Test]
        public async Task CreateTestBookLoan_ReturnsCreatedBookLoan()
        {
            // Arrange
            int validAuthorId = await CreateTestAuthorAndGetId(); // Dynamically get a valid AuthorId

            var newBookLoan = new BookLoan
            {
                 BookTitle = "Test BookTitle",
                LoanDate = "2024-11-9",
                ReturnDate = "Test ReturnDate",
                AuthorId = validAuthorId // Use the valid AuthorId obtained from the helper method
            };

            var json = JsonConvert.SerializeObject(newBookLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/BookLoan", content);
            response.EnsureSuccessStatusCode();

            // Assert
            var createdBookLoanJson = await response.Content.ReadAsStringAsync();
            var createdBookLoan = JsonConvert.DeserializeObject<BookLoan>(createdBookLoanJson);

            Assert.IsNotNull(createdBookLoan);
            Assert.AreEqual(newBookLoan.BookTitle, createdBookLoan.BookTitle);
            Assert.AreEqual(newBookLoan.LoanDate, createdBookLoan.LoanDate);
            Assert.AreEqual(newBookLoan.ReturnDate, createdBookLoan.ReturnDate);
            Assert.AreEqual(newBookLoan.AuthorId, createdBookLoan.AuthorId); // Ensure AuthorId matches
        }



        [Test]
        public async Task Test_GetAllBookLoans_ReturnsListOfBookLoans()
        {
            var response = await _httpClient.GetAsync("api/BookLoan");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var bookLoans = JsonConvert.DeserializeObject<BookLoan[]>(content);

            Assert.IsNotNull(bookLoans);
            Assert.IsTrue(bookLoans.Length > 0);
        }

        [Test]
        public async Task Test_GetAuthors_ReturnsListOfAuthors()
        {
            var response = await _httpClient.GetAsync("api/Author");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<Author[]>(content);

            Assert.IsNotNull(orders);
            Assert.IsTrue(orders.Length > 0);
        }


        [Test]
        public async Task Test_GetBookLoanById_InvalidId_ReturnsNotFound()
        {
            var response = await _httpClient.GetAsync($"api/BookLoan/999");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Test_GetAuthorId_InvalidId_ReturnsNotFound()
        {
            var response = await _httpClient.GetAsync($"api/Author/999");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        private async Task<int> CreateTestAuthorAndGetId()
        {
            var newAuthor = new Author
            {
                CustomerName = "Test Customer",
                AuthorDate = "2024-10-24" // Use a valid format
            };

            var json = JsonConvert.SerializeObject(newAuthor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Author", content);
            response.EnsureSuccessStatusCode();

            var createdAuthorJson = await response.Content.ReadAsStringAsync();
            var createdAuthor = JsonConvert.DeserializeObject<Author>(createdAuthorJson);

            return createdAuthor.AuthorId; // Return the ID of the created Author
        }

        [Test]
        public async Task Test_AddBookLoan_ReturnsCreatedResponse()
        {
            // Arrange
            int validAuthorId = await CreateTestAuthorAndGetId(); // Use the helper method to get a valid AuthorId

            var newBookLoan = new BookLoan
            {
                 BookTitle = "Test BookTitle",
                LoanDate = "2024-11-9",
                ReturnDate = "Test ReturnDate",
                AuthorId = validAuthorId// Use the valid AuthorId obtained from the helper method
            };

            var json = JsonConvert.SerializeObject(newBookLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/BookLoan", content);
            response.EnsureSuccessStatusCode();

            // Assert
            var createdBookLoanJson = await response.Content.ReadAsStringAsync();
            var createdBookLoan = JsonConvert.DeserializeObject<BookLoan>(createdBookLoanJson);

            Assert.IsNotNull(createdBookLoan);
            Assert.AreEqual(newBookLoan.Artist, createdBookLoan.Artist);
            Assert.AreEqual(newBookLoan.AuthorId, createdBookLoan.AuthorId); // Ensure AuthorId matches
        }


        [Test]
        public async Task Test_AddAuthor_ReturnsCreatedResponse()
        {
            // Arrange
            var newAuthor = new Author
            {
                CustomerName = "Test Customer",
                AuthorDate = "2024-20-24" // Ensure the date format matches your model's expectations
                // Initialize other properties if needed
            };

            var json = JsonConvert.SerializeObject(newAuthor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Author", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Ensure the response status is 200-299

            var createdAuthor = JsonConvert.DeserializeObject<Author>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(createdAuthor, "The created order is null.");
            Assert.AreEqual(newAuthor.CustomerName, createdAuthor.CustomerName, "Customer names do not match.");
            Assert.AreEqual(newAuthor.AuthorDate, createdAuthor.AuthorDate, "Author dates do not match.");
            // Add additional assertions as needed
        }


        [TearDown]
        public async Task Cleanup()
        {
            if (_testBookLoan != null)
            {
                var response = await _httpClient.DeleteAsync($"api/BookLoan/{_testBookLoan.BookLoanId}");
                if (response.StatusCode != HttpStatusCode.NotFound)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            _httpClient.Dispose();
        }
    }
}