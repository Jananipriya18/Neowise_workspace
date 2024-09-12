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

        private async Task<int> CreateTestLibraryAndGetId()
        {
            var newLibrary = new Library
            {
                Name = "Test Library",
                Address = "123 Test St"
            };

            var json = JsonConvert.SerializeObject(newLibrary);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Library", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdLibrary = JsonConvert.DeserializeObject<Library>(responseContent);

            return createdLibrary.LibraryId;
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
        public async Task SearchLibraryManagerByName_InvalidName_ReturnsBadRequest()
        {
            // Act
            var response = await _httpClient.GetAsync("api/LibraryManager/Search?name=");

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

      [Test]
public async Task PostBookLoan_ReturnsCreatedBookLoanWithLibraryDetails()
{
    // Arrange
    int libraryId = await CreateTestLibraryAndGetId(); // Dynamically create a valid Library

    var newBookLoan = new BookLoan
    {
        BookTitle = "Test Book Loan",
        LoanDate = DateTime.Now.ToString("yyyy-MM-dd"),  // Convert DateTime to string
        ReturnDate = null,
        LoanAmount = 5,
        LibraryManagerId = libraryId // Use LibraryManagerId
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
    Assert.AreEqual(newBookLoan.LibraryManagerId, createdBookLoan.LibraryManagerId); // Check correct library is associated
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
        public void LibraryModel_HasAllProperties()
        {
            // Arrange
            var library = new LibraryManager
            {
                LibraryManagerId = 1,
                Name = "Main Library",
                ContactInfo = "9876543210",
                BookLoans = new List<BookLoan>() // Ensure the BookLoans collection is properly initialized
            };

            // Act & Assert
            Assert.AreEqual(1, library.LibraryManagerId, "LibraryManagerId does not match.");
            Assert.AreEqual("Main Library", library.Name, "Name does not match.");
            Assert.AreEqual("9876543210", library.ContactInfo, "ContactInfo does not match.");
            Assert.IsNotNull(library.BookLoans, "BookLoans collection should not be null.");
            Assert.IsInstanceOf<ICollection<BookLoan>>(library.BookLoans, "BookLoans should be of type ICollection<BookLoan>.");
        }

        [Test]
        public void BookLoanModel_HasAllProperties()
        {
            // Arrange
            var library = new LibraryManager
            {
                LibraryManagerId = 1,
                Name = "Main Library",
                ContactInfo = "9876543210"
            };

            var bookLoan = new BookLoan
            {
                BookLoanId = 100,
                BookTitle = "Test Book Loan",
                LoanDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ReturnDate = null,
                LoanAmount = 3,
                LibraryManagerId = 1,
                LibraryManager = library
            };

            // Act & Assert
            Assert.AreEqual(100, bookLoan.BookLoanId, "BookLoanId does not match.");
            Assert.AreEqual("Test Book Loan", bookLoan.BookTitle, "BookTitle does not match.");
            Assert.AreEqual(3, bookLoan.LoanAmount, "LoanAmount does not match.");
            Assert.AreEqual(1, bookLoan.LibraryManagerId, "LibraryManagerId does not match.");
            Assert.IsNotNull(bookLoan.LibraryManager, "LibraryManager should not be null.");
            Assert.AreEqual(library.Name, bookLoan.LibraryManager.Name, "LibraryManager's Name does not match.");
        }

        [Test]
        public void DbContext_HasDbSetProperties()
        {
            // Assert that the context has DbSet properties for Libraries and BookLoans
            Assert.IsNotNull(_context.LibraryManagers, "Libraries DbSet is not initialized.");
            Assert.IsNotNull(_context.BookLoans, "BookLoans DbSet is not initialized.");
        }

        [Test]
        public void LibraryManagerBookLoan_Relationship_IsConfiguredCorrectly()
        {
            // Check if the LibraryManager to BookLoan relationship is configured as one-to-many
            var model = _context.Model;
            var libraryEntity = model.FindEntityType(typeof(LibraryManager));
            var bookLoanEntity = model.FindEntityType(typeof(BookLoan));

            // Assert that the foreign key relationship exists between BookLoan and LibraryManager
            var foreignKey = bookLoanEntity.GetForeignKeys().FirstOrDefault(fk => fk.PrincipalEntityType == libraryEntity);

            Assert.IsNotNull(foreignKey, "Foreign key relationship between BookLoan and LibraryManager is not configured.");
            Assert.AreEqual("LibraryManagerId", foreignKey.Properties.First().Name, "Foreign key property name is incorrect.");

            // Check if the cascade delete behavior is set
            Assert.AreEqual(DeleteBehavior.Cascade, foreignKey.DeleteBehavior, "Cascade delete behavior is not set correctly.");
        }

        [Test]
        public async Task PostBookLoan_ThrowsLoanAmountException_ForNegativeLoanAmount()
        {
            // Arrange
            var newBookLoan = new BookLoan
            {
                BookTitle = "Test Book Loan",
                LoanDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ReturnDate = null,
                LoanAmount = -1,  // Invalid negative loan amount
                LibraryManagerId = 1
            };

            var json = JsonConvert.SerializeObject(newBookLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/BookLoan", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains("LoanAmount cannot be negative."), "Expected error message not found in the response.");
        }

        [Test]
        public async Task PostBookLoan_ThrowsLoanAmountException_ForZeroLoanAmount()
        {
            // Arrange
            var newBookLoan = new BookLoan
            {
                BookTitle = "Test Book Loan",
                LoanDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ReturnDate = null,
                LoanAmount = 0,  // Invalid zero loan amount
                LibraryManagerId = 1
            };

            var json = JsonConvert.SerializeObject(newBookLoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/BookLoan", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains("LoanAmount cannot be zero."), "Expected error message not found in the response.");
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}

