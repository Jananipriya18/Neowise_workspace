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
    using dotnetapp.Exceptions;

    namespace dotnetapp.Tests
    {
        [TestFixture]
        public class CustomerSpiceControllerTests
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

            private async Task<int> CreateTestCustomerAndGetId()
            {
                var newCustomer = new Customer
                {
                    FullName = "Test Customer",
                    ContactNumber = "123-456-7890",
                    Address = "123 Main St"
                };

                var json = JsonConvert.SerializeObject(newCustomer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Customer", content);
                response.EnsureSuccessStatusCode();

                var createdCustomer = JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
                return createdCustomer.CustomerId;
            }

            private async Task<int> CreateTestSpiceAndGetId(int customerId)
            {
                var newSpice = new Spice
                {
                    Name = "Cinnamon",
                    OriginCountry = "Sri Lanka",
                    FlavorProfile = "Sweet and Spicy",
                    StockQuantity = 100,
                    CustomerId = customerId // Assuming Spice has a CustomerId property
                };

                var json = JsonConvert.SerializeObject(newSpice);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Spice", content);
                response.EnsureSuccessStatusCode();

                var createdSpice = JsonConvert.DeserializeObject<Spice>(await response.Content.ReadAsStringAsync());
                return createdSpice.SpiceId; // Assuming Spice has a SpiceId property
            }

            [Test]
            public async Task CreateCustomer_ReturnsCreatedCustomer()
            {
                // Arrange
                var newCustomer = new Customer
                {
                    FullName = "Test Customer",
                    ContactNumber = "123-456-7890",
                    Address = "123 Main St"
                };

                var json = JsonConvert.SerializeObject(newCustomer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _httpClient.PostAsync("api/Customer", content);

                // Assert
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

                var responseContent = await response.Content.ReadAsStringAsync();
                var createdCustomer = JsonConvert.DeserializeObject<Customer>(responseContent);

                Assert.IsNotNull(createdCustomer);
                Assert.AreEqual(newCustomer.FullName, createdCustomer.FullName);
                Assert.AreEqual(newCustomer.ContactNumber, createdCustomer.ContactNumber);
                Assert.AreEqual(newCustomer.Address, createdCustomer.Address);
            }

            [Test]
            public async Task CreateSpice_ReturnsCreatedSpiceWithCustomerDetails()
            {
                // Arrange
                int customerId = await CreateTestCustomerAndGetId(); // Dynamically create a valid Customer

                var newSpice = new Spice
                {
                    Name = "Cinnamon",
                    OriginCountry = "Sri Lanka",
                    FlavorProfile = "Sweet and Spicy",
                    StockQuantity = 100,
                    CustomerId = customerId
                };

                var json = JsonConvert.SerializeObject(newSpice);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _httpClient.PostAsync("api/Spice", content);

                // Assert
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

                var responseContent = await response.Content.ReadAsStringAsync();
                var createdSpice = JsonConvert.DeserializeObject<Spice>(responseContent);

                Assert.IsNotNull(createdSpice);
                Assert.AreEqual(newSpice.Name, createdSpice.Name);
                Assert.AreEqual(newSpice.OriginCountry, createdSpice.OriginCountry);
                Assert.AreEqual(newSpice.FlavorProfile, createdSpice.FlavorProfile);
                Assert.AreEqual(newSpice.StockQuantity, createdSpice.StockQuantity);
                Assert.AreEqual(customerId, createdSpice.CustomerId);
                Assert.IsNotNull(createdSpice.Customer);
                Assert.AreEqual(customerId, createdSpice.Customer.CustomerId);
            }

            [Test]
            public async Task GetCustomersSortedByName_ReturnsSortedCustomers()
            {
                // Arrange
                await CreateTestCustomerAndGetId(); // Create customers to be sorted

                // Act
                var response = await _httpClient.GetAsync("api/Customer/sortedByName"); // Adjust endpoint as needed

                // Assert
                response.EnsureSuccessStatusCode();
                var customers = JsonConvert.DeserializeObject<Customer[]>(await response.Content.ReadAsStringAsync());
                Assert.IsNotNull(customers);
                Assert.AreEqual(customers.OrderBy(c => c.FullName).ToList(), customers);
            }

            [Test]
            public async Task GetCustomerById_ReturnsCustomer()
            {
                // Arrange
                int customerId = await CreateTestCustomerAndGetId(); // Create a customer

                // Act
                var response = await _httpClient.GetAsync($"api/Customer/{customerId}");

                // Assert
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var customer = JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
                Assert.IsNotNull(customer);
                Assert.AreEqual(customerId, customer.CustomerId);
            }

            [Test]
            public async Task CreateSpice_MissingName_ReturnsBadRequest()
            {
                // Arrange
                var newSpice = new Spice
                {
                    OriginCountry = "India",
                    FlavorProfile = "Spicy",
                    StockQuantity = 10,
                    CustomerId = 1
                };

                var json = JsonConvert.SerializeObject(newSpice);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _httpClient.PostAsync("api/Spice", content);

                // Assert
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }


            [Test]
            public async Task UpdateSpice_ReturnsOkWithUpdatedSpice()
            {
                // Arrange
                int customerId = await CreateTestCustomerAndGetId();
                int spiceId = await CreateTestSpiceAndGetId(customerId); // Create a spice

                var updatedSpice = new Spice
                {
                    SpiceId = spiceId,
                    Name = "Updated Spice",
                    OriginCountry = "Updated Country",
                    FlavorProfile = "Updated Flavor",
                    StockQuantity = 200 // Example updated quantity
                };

                var json = JsonConvert.SerializeObject(updatedSpice);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _httpClient.PutAsync($"api/Spice/{spiceId}", content);

                // Assert
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var responseContent = await response.Content.ReadAsStringAsync();
                var spiceResponse = JsonConvert.DeserializeObject<Spice>(responseContent);
                Assert.AreEqual(updatedSpice.Name, spiceResponse.Name);
                Assert.AreEqual(updatedSpice.OriginCountry, spiceResponse.OriginCountry);
                Assert.AreEqual(updatedSpice.FlavorProfile, spiceResponse.FlavorProfile);
                Assert.AreEqual(updatedSpice.StockQuantity, spiceResponse.StockQuantity);
            }

            [Test]
            public async Task UpdateSpice_InvalidId_ReturnsNotFound()
            {
                // Arrange
                var updatedSpice = new Spice
                {
                    SpiceId = 9999, // Non-existent ID
                    Name = "Spice Name",
                    OriginCountry = "Spice Country",
                    FlavorProfile = "Spice Flavor",
                    StockQuantity = 50
                };

                var json = JsonConvert.SerializeObject(updatedSpice);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _httpClient.PutAsync($"api/Spice/{updatedSpice.SpiceId}", content);

                // Assert
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public async Task GetCustomer_ReturnsCustomer()
            {
                // Arrange
                int customerId = await CreateTestCustomerAndGetId(); // Create a customer

                // Act
                var response = await _httpClient.GetAsync($"api/Customer/{customerId}");

                // Assert
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var customer = JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
                Assert.IsNotNull(customer);
                Assert.AreEqual(customerId, customer.CustomerId);
            }

           [Test]
            public async Task GetSpices_ReturnsAtLeastOneSpiceWithCustomer()
            {
                // Arrange
                int customerId = await CreateTestCustomerAndGetId(); // Dynamically create a valid Customer

                // Create a spice associated with the test customer
                var newSpice = new Spice
                {
                    Name = "Test Spice",
                    OriginCountry = "Test Country",
                    FlavorProfile = "Test Flavor",
                    StockQuantity = 10, // Assume a positive stock quantity
                    CustomerId = customerId // Associate with the created customer
                };

                var json = JsonConvert.SerializeObject(newSpice);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Act: Create the spice
                var createResponse = await _httpClient.PostAsync("api/Spice", content);
                Assert.AreEqual(HttpStatusCode.Created, createResponse.StatusCode); // Assert creation success

                // Act: Now get the spices
                var response = await _httpClient.GetAsync("api/Spice");

                // Assert
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var spices = JsonConvert.DeserializeObject<Spice[]>(responseContent);

                Assert.IsNotNull(spices);
                Assert.IsTrue(spices.Length >= 1, "Expected at least one spice to be returned."); // Ensure at least one spice is returned

                // Validate the customer association of the first spice
                var firstSpice = spices[0];
                // Assert.IsNotNull(firstSpice.Customer, "Customer should not be null for the first spice.");
                // Assert.AreEqual(customerId, firstSpice.CustomerId, "Customer ID should match the test customer.");
            }



            [Test]
            public async Task GetCustomerById_InvalidId_ReturnsNotFound()
            {
                // Act
                var response = await _httpClient.GetAsync("api/Customer/999");

                // Assert
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void CustomerModel_HasAllProperties()
            {
                // Arrange
                var customer = new Customer();

                // Act & Assert
                Assert.IsTrue(typeof(Customer).GetProperties().Any(p => p.Name == "CustomerId"));
                Assert.IsTrue(typeof(Customer).GetProperties().Any(p => p.Name == "FullName"));
                Assert.IsTrue(typeof(Customer).GetProperties().Any(p => p.Name == "ContactNumber"));
                Assert.IsTrue(typeof(Customer).GetProperties().Any(p => p.Name == "Address"));
            }

            [Test]
            public void SpiceModel_HasAllProperties()
            {
                // Arrange
                var spice = new Spice();

                // Act & Assert
                Assert.IsTrue(typeof(Spice).GetProperties().Any(p => p.Name == "SpiceId"));
                Assert.IsTrue(typeof(Spice).GetProperties().Any(p => p.Name == "Name"));
                Assert.IsTrue(typeof(Spice).GetProperties().Any(p => p.Name == "OriginCountry"));
                Assert.IsTrue(typeof(Spice).GetProperties().Any(p => p.Name == "FlavorProfile"));
                Assert.IsTrue(typeof(Spice).GetProperties().Any(p => p.Name == "StockQuantity"));
                Assert.IsTrue(typeof(Spice).GetProperties().Any(p => p.Name == "CustomerId"));
            }

            [Test]
            public async Task CreateSpice_WithZeroStockQuantity_ThrowsException()
            {
                // Arrange
                int customerId = await CreateTestCustomerAndGetId(); // Ensure a valid customer is created

                var newSpice = new Spice
                {
                    Name = "Test Spice Zero",
                    OriginCountry = "India",
                    FlavorProfile = "Spicy",
                    StockQuantity = 0, // Zero stock quantity
                    CustomerId = customerId
                };

                var json = JsonConvert.SerializeObject(newSpice);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _httpClient.PostAsync("api/Spice", content);

                // Assert
                Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode, 
                    "Expected InternalServerError for zero stock quantity.");
                
                var responseContent = await response.Content.ReadAsStringAsync();
                Assert.IsTrue(responseContent.Contains("Stock quantity must be a positive value."), 
                    "Error message for zero stock quantity not found.");
            }

            [Test]
            public async Task CreateSpice_WithNegativeStockQuantity_ThrowsException()
            {
                // Arrange
                int customerId = await CreateTestCustomerAndGetId(); // Ensure a valid customer is created

                var newSpice = new Spice
                {
                    Name = "Test Spice Zero",
                    OriginCountry = "India",
                    FlavorProfile = "Spicy",
                    StockQuantity = -10, // Zero stock quantity
                    CustomerId = customerId
                };

                var json = JsonConvert.SerializeObject(newSpice);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _httpClient.PostAsync("api/Spice", content);

                // Assert
                Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode, 
                    "Expected InternalServerError for zero stock quantity.");
                
                var responseContent = await response.Content.ReadAsStringAsync();
                Assert.IsTrue(responseContent.Contains("Stock quantity must be a positive value."), 
                    "Error message for zero stock quantity not found.");
            }



            [TearDown]
            public void TearDown()
            {
                // Clean up the database after each test
                _context.Database.EnsureDeleted();
                _context.Dispose();
            }
        }
    }
