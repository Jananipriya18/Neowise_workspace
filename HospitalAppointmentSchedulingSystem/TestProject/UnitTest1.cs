using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using dotnetapp.Data;
using dotnetapp.Models;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace dotnetapp.Tests
{
     [TestFixture]
    public class CustomerSpiceControllerTests
    {
        private HttpClient _httpClient;
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080"); // Update to your API's base URL
            _context = new ApplicationDbContext(); // Consider configuring in-memory database
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
                CustomerId = customerId
            };

            var json = JsonConvert.SerializeObject(newSpice);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Spice", content);
            response.EnsureSuccessStatusCode();

            var createdSpice = JsonConvert.DeserializeObject<Spice>(await response.Content.ReadAsStringAsync());
            return createdSpice.SpiceId;
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
            int customerId = await CreateTestCustomerAndGetId(); // Create a customer dynamically

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
        }

        [Test]
        public async Task GetCustomers_ReturnsListOfCustomers()
        {
            // Arrange
            await CreateTestCustomerAndGetId(); // Create a customer

            // Act
            var response = await _httpClient.GetAsync("api/Customer");

            // Assert
            response.EnsureSuccessStatusCode();
            var customers = JsonConvert.DeserializeObject<Customer[]>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Length > 0); // Ensure at least one customer exists
        }

        [Test]
        public async Task GetSpices_ReturnsListOfSpicesWithCustomers()
        {
            // Arrange
            int customerId = await CreateTestCustomerAndGetId(); // Create a customer
            await CreateTestSpiceAndGetId(customerId); // Create a spice

            // Act
            var response = await _httpClient.GetAsync("api/Spice");

            // Assert
            response.EnsureSuccessStatusCode();
            var spices = JsonConvert.DeserializeObject<Spice[]>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(spices);
            Assert.IsTrue(spices.Length > 0); // Ensure at least one spice exists

            // Ensure each spice has an associated customer
            foreach (var spice in spices)
            {
                Assert.IsNotNull(spice.Customer, "Customer should not be null for each spice.");
            }
        }

        [Test]
        public async Task UpdateCustomer_ReturnsOkWithUpdatedCustomer()
        {
            // Arrange
            int customerId = await CreateTestCustomerAndGetId(); // Create a customer
            var updatedCustomer = new Customer
            {
                CustomerId = customerId,
                FullName = "Updated Customer",
                ContactNumber = "987-654-3210",
                Address = "456 Another St"
            };

            var json = JsonConvert.SerializeObject(updatedCustomer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync($"api/Customer/{customerId}", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var modifiedCustomer = JsonConvert.DeserializeObject<Customer>(responseContent);

            Assert.IsNotNull(modifiedCustomer);
            Assert.AreEqual(updatedCustomer.FullName, modifiedCustomer.FullName);
            Assert.AreEqual(updatedCustomer.ContactNumber, modifiedCustomer.ContactNumber);
            Assert.AreEqual(updatedCustomer.Address, modifiedCustomer.Address);
        }

        [Test]
        public async Task UpdateSpice_ReturnsOkWithUpdatedSpice()
        {
            // Arrange
            int customerId = await CreateTestCustomerAndGetId(); // Create a customer
            int spiceId = await CreateTestSpiceAndGetId(customerId); // Create a spice

            var updatedSpice = new Spice
            {
                SpiceId = spiceId,
                Name = "Updated Cinnamon",
                OriginCountry = "Updated Sri Lanka",
                FlavorProfile = "Updated Sweet and Spicy",
                StockQuantity = 150,
                CustomerId = customerId
            };

            var json = JsonConvert.SerializeObject(updatedSpice);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync($"api/Spice/{spiceId}", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var modifiedSpice = JsonConvert.DeserializeObject<Spice>(responseContent);

            Assert.IsNotNull(modifiedSpice);
            Assert.AreEqual(updatedSpice.Name, modifiedSpice.Name);
            Assert.AreEqual(updatedSpice.OriginCountry, modifiedSpice.OriginCountry);
            Assert.AreEqual(updatedSpice.FlavorProfile, modifiedSpice.FlavorProfile);
            Assert.AreEqual(updatedSpice.StockQuantity, modifiedSpice.StockQuantity);
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
        public async Task GetSpiceById_ReturnsSpice()
        {
            // Arrange
            int customerId = await CreateTestCustomerAndGetId(); // Create a customer
            int spiceId = await CreateTestSpiceAndGetId(customerId); // Create a spice

            // Act
            var response = await _httpClient.GetAsync($"api/Spice/{spiceId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var spice = JsonConvert.DeserializeObject<Spice>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(spice);
            Assert.AreEqual(spiceId, spice.SpiceId);
        }

        [Test]
        public async Task DeleteCustomer_ReturnsNoContent()
        {
            // Arrange
            int customerId = await CreateTestCustomerAndGetId(); // Create a customer

            // Act
            var response = await _httpClient.DeleteAsync($"api/Customer/{customerId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task DeleteSpice_ReturnsNoContent()
        {
            // Arrange
            int customerId = await CreateTestCustomerAndGetId(); // Create a customer
            int spiceId = await CreateTestSpiceAndGetId(customerId); // Create a spice

            // Act
            var response = await _httpClient.DeleteAsync($"api/Spice/{spiceId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public void SpiceModel_HasAllProperties()
        {
            // Arrange
            var customerInstance = new Customer
            {
                CustomerId = 1,
                FullName = "Sample Customer",
                ContactNumber = "987-654-3210",
                Address = "123 Main St"
            };

            var spice = new Spice
            {
                SpiceId = 100,
                Name = "Cinnamon",
                OriginCountry = "Sri Lanka",
                FlavorProfile = "Sweet and Spicy",
                StockQuantity = 150,
                CustomerId = 1,
                Customer = customerInstance
            };

            // Act & Assert
            Assert.AreEqual(100, spice.SpiceId, "SpiceId does not match.");
            Assert.AreEqual("Cinnamon", spice.Name, "Name does not match.");
            Assert.AreEqual("Sri Lanka", spice.OriginCountry, "OriginCountry does not match.");
            Assert.AreEqual("Sweet and Spicy", spice.FlavorProfile, "FlavorProfile does not match.");
            Assert.AreEqual(150, spice.StockQuantity, "StockQuantity does not match.");
            Assert.AreEqual(1, spice.CustomerId, "CustomerId does not match.");
            Assert.IsNotNull(spice.Customer, "Customer should not be null.");
            Assert.AreEqual(customerInstance.FullName, spice.Customer.FullName, "Customer's Name does not match.");
        }

        [Test]
        public void DbContext_HasDbSetProperties()
        {
            // Assert that the context has DbSet properties for Customers and Spices
            Assert.IsNotNull(_context.Customers, "Customers DbSet is not initialized.");
            Assert.IsNotNull(_context.Spices, "Spices DbSet is not initialized.");
        }

        [Test]
        public void CustomerSpice_Relationship_IsConfiguredCorrectly()
        {
            // Check if the Customer to Spice relationship is configured as one-to-many
            var model = _context.Model;
            var customerEntity = model.FindEntityType(typeof(Customer));
            var spiceEntity = model.FindEntityType(typeof(Spice));

            // Assert that the foreign key relationship exists between Spice and Customer
            var foreignKey = spiceEntity.GetForeignKeys().FirstOrDefault(fk => fk.PrincipalEntityType == customerEntity);

            Assert.IsNotNull(foreignKey, "Foreign key relationship between Spice and Customer is not configured.");
            Assert.AreEqual("CustomerId", foreignKey.Properties.First().Name, "Foreign key property name is incorrect.");

            // Check if the cascade delete behavior is set
            Assert.AreEqual(DeleteBehavior.Cascade, foreignKey.DeleteBehavior, "Cascade delete behavior is not set correctly.");
        }


        [TearDown]
        public void TearDown()
        {
            // If using an in-memory database, no cleanup is necessary.
            _httpClient.Dispose();
        }
    }
}
    