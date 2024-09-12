using NUnit.Framework;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class BookLoanControllerTests
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

        private async Task<int> CreateTestLibraryManagerAndGetId()
        {
            var newLibraryManager = new LibraryManager
            {
                Name = "Test Library Manager",
                ContactInfo = "testmanager@example.com"
            };

            var json = JsonConvert.SerializeObject(newLibraryManager);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/LibraryManager", content);
            response.EnsureSuccessStatusCode();

            var createdLibraryManager = JsonConvert.DeserializeObject<LibraryManager>(await response.Content.ReadAsStringAsync());
            return createdLibraryManager.LibraryManagerId;
        }

        [Test]
        public async Task CreateBookLoan_ReturnsCreatedBookLoan()
        {
            // Arrange
            var newLibraryManagerId = await CreateTestLibraryManagerAndGetId(); // Dynamically create a valid LibraryManager

            var newBookLoan = new BookLoan
            {
                BookTitle = "Test Book Loan",
                LoanDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ReturnDate = null,
                LoanAmount = 5, // Valid loan amount
                LibraryManagerId = newLibraryManagerId
            };

            var json = JsonConvert.SerializeObject(newBookLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/BookLoan", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdBookLoan = JsonConvert.DeserializeObject<BookLoan>(responseContent);
            Assert.IsNotNull(createdBookLoan);
            Assert.AreEqual(newBookLoan.BookTitle, createdBookLoan.BookTitle);
            Assert.AreEqual(newBookLoan.LoanAmount, createdBookLoan.LoanAmount);
            Assert.IsNotNull(createdBookLoan.LibraryManager);
        }

        [Test]
        public async Task PostBookLoan_ReturnsCreatedBookLoanWithLibraryDetails()
        {
            // Arrange
            int libraryId = await CreateTestLibraryAndGetId(); // Dynamically create a valid Library

            var newBookLoan = new BookLoan
            {
                BookTitle = "Test Book Loan",
                LoanDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ReturnDate = null,
                LoanAmount = 5,
                LibraryId = libraryId
            };

            var json = JsonConvert.SerializeObject(newBookLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/BookLoan", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdBookLoan = JsonConvert.DeserializeObject<BookLoan>(responseContent);

            Assert.IsNotNull(createdBookLoan);
            Assert.AreEqual(newBookLoan.BookTitle, createdBookLoan.BookTitle);
            Assert.AreEqual(newBookLoan.LoanAmount, createdBookLoan.LoanAmount);
            Assert.IsNotNull(createdBookLoan.Library); // Ensure Library is populated
            Assert.AreEqual(libraryId, createdBookLoan.Library.LibraryId); // Check correct library is associated
        }


        [Test]
        public async Task DeleteBookLoan_ReturnsNoContent()
        {
            // Arrange
            var newLibraryManagerId = await CreateTestLibraryManagerAndGetId(); // Create a valid LibraryManager

            var newBookLoan = new BookLoan
            {
                BookTitle = "Book Loan to be deleted",
                LoanDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ReturnDate = null,
                LoanAmount = 1, // Valid loan amount
                LibraryManagerId = newLibraryManagerId
            };

            var json = JsonConvert.SerializeObject(newBookLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/BookLoan", content);
            var createdBookLoan = JsonConvert.DeserializeObject<BookLoan>(await response.Content.ReadAsStringAsync());

            // Act
            var deleteResponse = await _httpClient.DeleteAsync($"api/BookLoan/{createdBookLoan.BookLoanId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Test]
        public async Task DeleteBookLoan_InvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidBookLoanId = 9999; // Use an ID that doesn't exist in the database

            // Act
            var response = await _httpClient.DeleteAsync($"api/BookLoan/{invalidBookLoanId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode, "Expected 404 Not Found for an invalid BookLoan ID.");
        }

        [Test]
        public async Task GetBookLoans_ReturnsListOfBookLoansWithLibraryManagers()
        {
            // Act
            var response = await _httpClient.GetAsync("api/BookLoan");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var bookLoans = JsonConvert.DeserializeObject<BookLoan[]>(responseContent);

            Assert.IsNotNull(bookLoans);
            Assert.IsTrue(bookLoans.Length > 0);
            Assert.IsNotNull(bookLoans[0].LibraryManager); // Ensure each book loan has a library manager loaded
        }

        [Test]
        public async Task GetBookLoanById_ReturnsBookLoanWithLibraryManagerDetails()
        {
            // Arrange
            var newLibraryManagerId = await CreateTestLibraryManagerAndGetId();
            var newBookLoan = new BookLoan
            {
                BookTitle = "Book Loan for ID Test",
                LoanDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ReturnDate = null,
                LoanAmount = 1,
                LibraryManagerId = newLibraryManagerId
            };

            var json = JsonConvert.SerializeObject(newBookLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/BookLoan", content);
            var createdBookLoan = JsonConvert.DeserializeObject<BookLoan>(await response.Content.ReadAsStringAsync());

            // Act
            var getResponse = await _httpClient.GetAsync($"api/BookLoan/{createdBookLoan.BookLoanId}");

            // Assert
            getResponse.EnsureSuccessStatusCode();
            var bookLoan = JsonConvert.DeserializeObject<BookLoan>(await getResponse.Content.ReadAsStringAsync());
            Assert.IsNotNull(bookLoan);
            Assert.AreEqual(newBookLoan.BookTitle, bookLoan.BookTitle);
            Assert.IsNotNull(bookLoan.LibraryManager);
        }

        [Test]
        public async Task GetBookLoanById_InvalidId_ReturnsNotFound()
        {
            // Act
            var response = await _httpClient.GetAsync("api/BookLoan/999");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Test]
        public async Task SearchLibraryManagerByName_InvalidName_ReturnsBadRequest()
        {
            // Act
            var response = await _httpClient.GetAsync("api/LibraryManager/Search?name=");

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

         [Test]
        public async Task CreateBookLoan_ThrowsBookLoanException_ForInvalidLoanAmount()
        {
            // Arrange
            var newBookLoan = new BookLoan
            {
                BookTitle = "Test Book Loan",
                LoanDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ReturnDate = null,
                LoanAmount = 0, // Invalid loan amount
                LibraryManagerId = 1
            };

            var json = JsonConvert.SerializeObject(newBookLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/BookLoan", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains("Loan Amount must be at least 1."), "Expected error message not found in the response.");
        }

        [TearDown]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
    }
}
