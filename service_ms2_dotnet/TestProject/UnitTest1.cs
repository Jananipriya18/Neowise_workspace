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

        private async Task<int> CreateTestServiceCenterAndGetId()
        {
            var newServiceCenter = new ServiceCenter
            {
                Name = "Test Manager",
                Location = "Test Location",
                ContactInfo = "9876543210"
            };

            var json = JsonConvert.SerializeObject(newServiceCenter);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/ServiceCenter", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdServiceCenter = JsonConvert.DeserializeObject<ServiceCenter>(responseContent);

            return createdServiceCenter.ServiceCenterId;
        }

        [Test]
public async Task CreateServiceBooking_ReturnsCreatedServiceBooking()
{
    int serviceCenterId = await CreateTestServiceCenterAndGetId(); // Change this method name appropriately

    var newServiceBooking = new ServiceBooking
    {
        VehicleModel = "Sedan", // Updated from VehicleType to VehicleModel
        ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
        Status = "pending",
        ServiceCost = 200,
        ServiceCenterId = serviceCenterId // Updated to use ServiceCenterId
    };

    var json = JsonConvert.SerializeObject(newServiceBooking);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await _httpClient.PostAsync("api/ServiceBooking", content);

    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

    var responseContent = await response.Content.ReadAsStringAsync();
    var createdServiceBooking = JsonConvert.DeserializeObject<ServiceBooking>(responseContent);

    Assert.IsNotNull(createdServiceBooking);
    Assert.AreEqual(newServiceBooking.VehicleModel, createdServiceBooking.VehicleModel); // Updated property name
    Assert.IsNotNull(createdServiceBooking.ServiceCenter); // Updated property name
    Assert.AreEqual(serviceCenterId, createdServiceBooking.ServiceCenter.ServiceCenterId); // Updated property name
}
       [Test]
        public async Task SearchServiceCenterByName_InvalidName_ReturnsBadRequest()
        {
            var response = await _httpClient.GetAsync("api/ServiceCenter/Search?name=");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

    [Test]
        public async Task PostServiceBooking_ReturnsCreatedBookingWithManagerDetails()
        {
            int managerId = await CreateTestServiceCenterAndGetId();

            var newServiceBooking = new ServiceBooking
            {
                VehicleModel = "SUV",
                ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
                 Status = "pending",
                ServiceCost = 150,
                ServiceCenterId = managerId
            };

            var json = JsonConvert.SerializeObject(newServiceBooking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/ServiceBooking", content);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdServiceBooking = JsonConvert.DeserializeObject<ServiceBooking>(responseContent);

            Assert.IsNotNull(createdServiceBooking);
            Assert.AreEqual(newServiceBooking.VehicleModel, createdServiceBooking.VehicleModel);
            Assert.IsNotNull(createdServiceBooking.ServiceCenter);
            Assert.AreEqual(managerId, createdServiceBooking.ServiceCenterId);
        }


[Test]
        public async Task DeleteServiceBooking_ReturnsNoContent()
        {
            int managerId = await CreateTestServiceCenterAndGetId();

            var newServiceBooking = new ServiceBooking
            {
                VehicleModel = "Truck",
                ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Status = "pending",
                ServiceCost = 300,
                ServiceCenterId = managerId
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
            Assert.IsNotNull(bookings[0].ServiceCenter);
        }

       [Test]
public async Task GetServiceBookingById_ReturnsServiceBookingWithServiceCenterDetails()
{
    // Arrange
    var newServiceCenterId = await CreateTestServiceCenterAndGetId();
    var newServiceBooking = new ServiceBooking
    {
        VehicleModel = "Car",
        ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
        Status = "pending",
        ServiceCost = 250,
        ServiceCenterId = newServiceCenterId
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
    Assert.AreEqual(newServiceBooking.VehicleModel, serviceBooking.VehicleModel);
    Assert.IsNotNull(serviceBooking.ServiceCenter);
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
public void ServiceCenterModel_HasAllProperties()
{
    // Arrange
    var vehicleManager = new ServiceCenter
    {
        ServiceCenterId = 1,
        Name = "John Doe",
        ContactInfo = "9876543210",
        ServiceBookings = new List<ServiceBooking>()
    };

    // Act & Assert
    Assert.AreEqual(1, vehicleManager.ServiceCenterId, "ServiceCenterId does not match.");
    Assert.AreEqual("John Doe", vehicleManager.Name, "Name does not match.");
    Assert.AreEqual("9876543210", vehicleManager.ContactInfo, "ContactInfo does not match.");
    Assert.IsNotNull(vehicleManager.ServiceBookings, "ServiceBookings collection should not be null.");
    Assert.IsInstanceOf<ICollection<ServiceBooking>>(vehicleManager.ServiceBookings, "ServiceBookings should be of type ICollection<ServiceBooking>.");
}

       [Test]
public void ServiceBookingModel_HasAllProperties()
{
    // Arrange
    var vehicleManager = new ServiceCenter
    {
        ServiceCenterId = 1,
        Name = "John Doe",
        ContactInfo = "9876543210"
    };

    var serviceBooking = new ServiceBooking
    {
        ServiceBookingId = 100,
        VehicleModel = "Sedan",
        ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
        ServiceCost = 500,
        ServiceCenterId = 1,
        ServiceCenter = vehicleManager
    };

    // Act & Assert
    Assert.AreEqual(100, serviceBooking.ServiceBookingId, "ServiceBookingId does not match.");
    Assert.AreEqual("Sedan", serviceBooking.VehicleModel, "VehicleModel does not match.");
    Assert.AreEqual(500, serviceBooking.ServiceCost, "ServiceCost does not match.");
    Assert.AreEqual(1, serviceBooking.ServiceCenterId, "ServiceCenterId does not match.");
    Assert.IsNotNull(serviceBooking.ServiceCenter, "ServiceCenter should not be null.");
    Assert.AreEqual(vehicleManager.Name, serviceBooking.ServiceCenter.Name, "ServiceCenter's Name does not match.");
}


      [Test]
public void DbContext_HasDbSetProperties()
{
    // Assert that the context has DbSet properties for ServiceCenters and ServiceBookings
    Assert.IsNotNull(_context.ServiceCenters, "ServiceCenters DbSet is not initialized.");
    Assert.IsNotNull(_context.ServiceBookings, "ServiceBookings DbSet is not initialized.");
}
     [Test]
        public void ServiceCenterServiceBooking_Relationship_IsConfiguredCorrectly()
        {
            // Check if the ServiceCenter to ServiceBooking relationship is configured as one-to-many
            var model = _context.Model;
            var vehicleManagerEntity = model.FindEntityType(typeof(ServiceCenter));
            var serviceBookingEntity = model.FindEntityType(typeof(ServiceBooking));

            // Assert that the foreign key relationship exists between ServiceBooking and ServiceCenter
            var foreignKey = serviceBookingEntity.GetForeignKeys().FirstOrDefault(fk => fk.PrincipalEntityType == vehicleManagerEntity);

            Assert.IsNotNull(foreignKey, "Foreign key relationship between ServiceBooking and ServiceCenter is not configured.");
            Assert.AreEqual("ServiceCenterId", foreignKey.Properties.First().Name, "Foreign key property name is incorrect.");

            // Check if the cascade delete behavior is set
            Assert.AreEqual(DeleteBehavior.ClientSetNull, foreignKey.DeleteBehavior, "Expected delete behavior is not set correctly.");
        }

      [Test]
public async Task PostServiceBooking_ThrowsServiceCostException_ForNegativeServiceCost()
{
    // Arrange
    var newServiceBooking = new ServiceBooking
    {
        VehicleModel = "Bike", // Updated from VehicleType to VehicleModel
        ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
         Status = "pending",
        ServiceCost = -100,  // Invalid negative service cost
        ServiceCenterId = 1  // Updated to use ServiceCenterId
    };

    var json = JsonConvert.SerializeObject(newServiceBooking);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    // Act
    var response = await _httpClient.PostAsync("api/ServiceBooking", content);

    // Assert
    Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception
    var responseContent = await response.Content.ReadAsStringAsync();
    Assert.IsTrue(responseContent.Contains("Service Cost must be at least 1."), "Expected error message not found in the response."); // Updated message
}
[Test]
public async Task PostServiceBooking_ThrowsServiceCostException_ForZeroServiceCost()
{
    // Arrange
    var newServiceBooking = new ServiceBooking
    {
        VehicleModel = "Bike",
        ServiceDate = DateTime.Now.ToString("yyyy-MM-dd"),
         Status = "pending",
        ServiceCost = 0,  // Invalid zero service cost
        ServiceCenterId = 1
    };

    var json = JsonConvert.SerializeObject(newServiceBooking);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    // Act
    var response = await _httpClient.PostAsync("api/ServiceBooking", content);

    // Assert
    Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception
    var responseContent = await response.Content.ReadAsStringAsync();
    Assert.IsTrue(responseContent.Contains("Service Cost must be at least 1."), "Expected error message not found in the response.");
}

[TearDown]
public void Cleanup()
{
    _context.Dispose();
}
    }
}