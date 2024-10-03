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
    public class ServiceBookingControllerTests
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

        private async Task<int> CreateTestVehicleManagerAndGetId()
        {
            var newVehicleManager = new VehicleManager
            {
                Name = "Test Manager",
                ContactInfo = "9876543210"
            };

            var json = JsonConvert.SerializeObject(newVehicleManager);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/VehicleManager", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdVehicleManager = JsonConvert.DeserializeObject<VehicleManager>(responseContent);

            return createdVehicleManager.VehicleManagerId;
        }

        [Test]
        public async Task CreateServiceBooking_ReturnsCreatedServiceBooking()
        {
            int managerId = await CreateTestVehicleManagerAndGetId();

            var newServiceBooking = new ServiceBooking
            {
                VehicleType = "Sedan",
                ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ServiceCost = 200,
                VehicleManagerId = managerId
            };

            var json = JsonConvert.SerializeObject(newServiceBooking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/ServiceBooking", content);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdServiceBooking = JsonConvert.DeserializeObject<ServiceBooking>(responseContent);

            Assert.IsNotNull(createdServiceBooking);
            Assert.AreEqual(newServiceBooking.VehicleType, createdServiceBooking.VehicleType);
            Assert.IsNotNull(createdServiceBooking.VehicleManager);
            Assert.AreEqual(managerId, createdServiceBooking.VehicleManager.VehicleManagerId);
        }

       [Test]
        public async Task SearchVehicleManagerByName_InvalidName_ReturnsBadRequest()
        {
            var response = await _httpClient.GetAsync("api/VehicleManager/Search?name=");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

    [Test]
        public async Task PostServiceBooking_ReturnsCreatedBookingWithManagerDetails()
        {
            int managerId = await CreateTestVehicleManagerAndGetId();

            var newServiceBooking = new ServiceBooking
            {
                VehicleType = "SUV",
                ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ServiceCost = 150,
                VehicleManagerId = managerId
            };

            var json = JsonConvert.SerializeObject(newServiceBooking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/ServiceBooking", content);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdServiceBooking = JsonConvert.DeserializeObject<ServiceBooking>(responseContent);

            Assert.IsNotNull(createdServiceBooking);
            Assert.AreEqual(newServiceBooking.VehicleType, createdServiceBooking.VehicleType);
            Assert.IsNotNull(createdServiceBooking.VehicleManager);
            Assert.AreEqual(managerId, createdServiceBooking.VehicleManagerId);
        }


[Test]
        public async Task DeleteServiceBooking_ReturnsNoContent()
        {
            int managerId = await CreateTestVehicleManagerAndGetId();

            var newServiceBooking = new ServiceBooking
            {
                VehicleType = "Truck",
                ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ServiceCost = 300,
                VehicleManagerId = managerId
            };

            var json = JsonConvert.SerializeObject(newServiceBooking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var postResponse = await _httpClient.PostAsync("api/ServiceBooking", content);
            postResponse.EnsureSuccessStatusCode();

            var createdServiceBooking = JsonConvert.DeserializeObject<ServiceBooking>(await postResponse.Content.ReadAsStringAsync());

            var deleteResponse = await _httpClient.DeleteAsync($"api/ServiceBooking/{createdServiceBooking.ServiceBookingId}");

            Assert.AreEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            var getResponse = await _httpClient.GetAsync($"api/ServiceBooking/{createdServiceBooking.ServiceBookingId}");
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }

        [Test]
        public async Task DeleteServiceBooking_InvalidId_ReturnsNotFound()
        {
            var response = await _httpClient.DeleteAsync("api/ServiceBooking/9999");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Test]
        public async Task GetServiceBookings_ReturnsListOfServiceBookingsWithManagers()
        {
            var response = await _httpClient.GetAsync("api/ServiceBooking");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<ServiceBooking[]>(responseContent);

            Assert.IsNotNull(bookings);
            Assert.IsTrue(bookings.Length > 0);
            Assert.IsNotNull(bookings[0].VehicleManager);
        }

       [Test]
public async Task GetServiceBookingById_ReturnsServiceBookingWithVehicleManagerDetails()
{
    // Arrange
    var newVehicleManagerId = await CreateTestVehicleManagerAndGetId();
    var newServiceBooking = new ServiceBooking
    {
        VehicleType = "Car",
        ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
        ServiceCost = 250,
        VehicleManagerId = newVehicleManagerId
    };

    var json = JsonConvert.SerializeObject(newServiceBooking);
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    var response = await _httpClient.PostAsync("api/ServiceBooking", content);
    var createdServiceBooking = JsonConvert.DeserializeObject<ServiceBooking>(await response.Content.ReadAsStringAsync());

    // Act
    var getResponse = await _httpClient.GetAsync($"api/ServiceBooking/{createdServiceBooking.ServiceBookingId}");

    // Assert
    getResponse.EnsureSuccessStatusCode();
    var serviceBooking = JsonConvert.DeserializeObject<ServiceBooking>(await getResponse.Content.ReadAsStringAsync());
    Assert.IsNotNull(serviceBooking);
    Assert.AreEqual(newServiceBooking.VehicleType, serviceBooking.VehicleType);
    Assert.IsNotNull(serviceBooking.VehicleManager);
}


    [Test]
public async Task GetServiceBookingById_InvalidId_ReturnsNotFound()
{
    // Act
    var response = await _httpClient.GetAsync("api/ServiceBooking/9999");

    // Assert
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
}


     [Test]
public void VehicleManagerModel_HasAllProperties()
{
    // Arrange
    var vehicleManager = new VehicleManager
    {
        VehicleManagerId = 1,
        Name = "John Doe",
        ContactInfo = "9876543210",
        ServiceBookings = new List<ServiceBooking>()
    };

    // Act & Assert
    Assert.AreEqual(1, vehicleManager.VehicleManagerId, "VehicleManagerId does not match.");
    Assert.AreEqual("John Doe", vehicleManager.Name, "Name does not match.");
    Assert.AreEqual("9876543210", vehicleManager.ContactInfo, "ContactInfo does not match.");
    Assert.IsNotNull(vehicleManager.ServiceBookings, "ServiceBookings collection should not be null.");
    Assert.IsInstanceOf<ICollection<ServiceBooking>>(vehicleManager.ServiceBookings, "ServiceBookings should be of type ICollection<ServiceBooking>.");
}

       [Test]
public void ServiceBookingModel_HasAllProperties()
{
    // Arrange
    var vehicleManager = new VehicleManager
    {
        VehicleManagerId = 1,
        Name = "John Doe",
        ContactInfo = "9876543210"
    };

    var serviceBooking = new ServiceBooking
    {
        ServiceBookingId = 100,
        VehicleType = "Sedan",
        ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
        ServiceCost = 500,
        VehicleManagerId = 1,
        VehicleManager = vehicleManager
    };

    // Act & Assert
    Assert.AreEqual(100, serviceBooking.ServiceBookingId, "ServiceBookingId does not match.");
    Assert.AreEqual("Sedan", serviceBooking.VehicleType, "VehicleType does not match.");
    Assert.AreEqual(500, serviceBooking.ServiceCost, "ServiceCost does not match.");
    Assert.AreEqual(1, serviceBooking.VehicleManagerId, "VehicleManagerId does not match.");
    Assert.IsNotNull(serviceBooking.VehicleManager, "VehicleManager should not be null.");
    Assert.AreEqual(vehicleManager.Name, serviceBooking.VehicleManager.Name, "VehicleManager's Name does not match.");
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
                ReturnDate = "2024-09-12",
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
            Assert.IsTrue(responseContent.Contains("Loan Amount must be at least 1."), "Expected error message not found in the response.");
        }

        [Test]
        public async Task PostBookLoan_ThrowsLoanAmountException_ForZeroLoanAmount()
        {
            // Arrange
            var newBookLoan = new BookLoan
            {
                BookTitle = "Test Book Loan",
                LoanDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ReturnDate = "20-09-2024",
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
            Assert.IsTrue(responseContent.Contains("Loan Amount must be at least 1."), "Expected error message not found in the response.");
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}

