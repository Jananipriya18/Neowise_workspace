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
using System.Linq;
using System.Collections.Generic;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class EventAttendeeControllerTests
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

        private async Task<int> CreateTestAttendeeAndGetId(int eventId)
        {
            var newAttendee = new Attendee
            {
                Name = "Test Attendee",
                Email = "attendee@example.com",
                EventId = eventId
            };

            var json = JsonConvert.SerializeObject(newAttendee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Attendee", content);
            response.EnsureSuccessStatusCode();

            var createdAttendee = JsonConvert.DeserializeObject<Attendee>(await response.Content.ReadAsStringAsync());
            return createdAttendee.AttendeeId;
        }

        [Test]
        public async Task CreateEvent_ReturnsCreatedEvent()
        {
            // Arrange
            var newEvent = new Event
            {
                Name = "Test Event",
                EventDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Location = "Dallas"
            };

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Event", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdEvent = JsonConvert.DeserializeObject<Event>(responseContent);

            Assert.IsNotNull(createdEvent);
            Assert.AreEqual(newEvent.Name, createdEvent.Name);
            Assert.AreEqual(newEvent.EventDate, createdEvent.EventDate);
            Assert.AreEqual(newEvent.Location, createdEvent.Location);
        }

        [Test]
        public async Task CreateAttendee_ReturnsCreatedAttendeeWithEventDetails()
        {
            // Arrange
            int eventId = await CreateTestEventAndGetId(); // Dynamically create a valid Event

            var newAttendee = new Attendee
            {
                Name = "Test Attendee",
                Email = "attendee@example.com",
                EventId = eventId
            };

            var json = JsonConvert.SerializeObject(newAttendee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Attendee", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdAttendee = JsonConvert.DeserializeObject<Attendee>(responseContent);

            Assert.IsNotNull(createdAttendee);
            Assert.AreEqual(newAttendee.Name, createdAttendee.Name);
            Assert.AreEqual(newAttendee.Email, createdAttendee.Email);
            Assert.AreEqual(eventId, createdAttendee.EventId);
            Assert.IsNotNull(createdAttendee.Event);
            Assert.AreEqual(eventId, createdAttendee.Event.EventId);
        }

        [Test]
        public async Task GetEventsSortedByNameDesc_ReturnsSortedEvents()
        {
            // Arrange
            await CreateTestEventAndGetId(); // Create events to be sorted

            // Act
            var response = await _httpClient.GetAsync("api/Event/SortedByNameDesc");

            // Assert
            response.EnsureSuccessStatusCode();
            var events = JsonConvert.DeserializeObject<Event[]>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(events);
            Assert.AreEqual(events.OrderByDescending(e => e.Name).ToList(), events);
        }

       [Test]
public async Task GetEventById_ReturnsEvent()
{
    // Arrange
    int eventId = await CreateTestEventAndGetId(); // Create a test event and retrieve its ID
    Console.WriteLine($"Created Event ID: {eventId}");

    // Act
    var response = await _httpClient.GetAsync($"api/Event/{eventId}");
    Console.WriteLine($"Response Status Code: {response.StatusCode}");

    // Assert the response status code is OK (200)
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

    // Log response content
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Response Content: {content}");

    // Deserialize the response content into an Event object
    var eventModel = JsonConvert.DeserializeObject<Event>(content);

    // Ensure the event is not null and that the returned event ID matches the one we created
    Assert.IsNotNull(eventModel, "Event should not be null.");
    Assert.AreEqual(eventId, eventModel.EventId, "Event ID should match.");

    // // Ensure that the other fields are correctly returned as per the API response
    // Assert.AreEqual("string", eventModel.Name, "Event name should be 'string'.");
    // Assert.AreEqual("string", eventModel.EventDate, "Event date should be 'string'.");
    // Assert.AreEqual("Dallas", eventModel.Location, "Location should be 'Dallas'.");
}

        [Test]
        public async Task UpdateAttendee_ReturnsNoContent()
        {
            // Arrange
            int eventId = await CreateTestEventAndGetId();
            int attendeeId = await CreateTestAttendeeAndGetId(eventId);

            var updatedAttendee = new Attendee
            {
                AttendeeId = attendeeId,
                Name = "Updated Attendee",
                Email = "updated@example.com",
                EventId = eventId
            };

            var json = JsonConvert.SerializeObject(updatedAttendee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync($"api/Attendee/{attendeeId}", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task UpdateAttendee_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var updatedAttendee = new Attendee
            {
                AttendeeId = 9999, // Non-existent ID
                Name = "John Doe Updated",
                Email = "john.doe.updated@example.com"
            };

            var json = JsonConvert.SerializeObject(updatedAttendee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync($"api/Attendee/{updatedAttendee.AttendeeId}", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
    public async Task GetAttendees_ReturnsListOfAttendeesWithEvents()
    {
        // Act
        var response = await _httpClient.GetAsync("api/Attendee");

        // Assert
        response.EnsureSuccessStatusCode();
        var attendees = JsonConvert.DeserializeObject<Attendee[]>(await response.Content.ReadAsStringAsync());

        // Ensure the deserialized attendees array is not null
        Assert.IsNotNull(attendees);
        
        // Ensure that the array contains one or more attendees
        Assert.IsTrue(attendees.Length > 0);
            Assert.IsNotNull(attendees[0].Event); // Ensure each event has attendees loaded
        }

        [Test]
        public async Task GetEventById_InvalidId_ReturnsNotFound()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Event/999");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void EventModel_HasAllProperties()
        {
            // Arrange
            var eventInstance = new Event
            {
                EventId = 1,
                Name = "Sample Event",
                EventDate = "2024-09-15",
                Location = "New York",
                Attendees = new List<Attendee>() // Ensure the Attendees collection is properly initialized
            };

            // Act & Assert
            Assert.AreEqual(1, eventInstance.EventId, "EventId does not match.");
            Assert.AreEqual("Sample Event", eventInstance.Name, "Name does not match.");
            Assert.AreEqual("2024-09-15", eventInstance.EventDate, "EventDate does not match.");
            Assert.AreEqual("New York", eventInstance.Location, "Location does not match.");
            Assert.IsNotNull(eventInstance.Attendees, "Attendees collection should not be null.");
            Assert.IsInstanceOf<ICollection<Attendee>>(eventInstance.Attendees, "Attendees should be of type ICollection<Attendee>.");
        }

        [Test]
        public void AttendeeModel_HasAllProperties()
        {
            // Arrange
            var eventInstance = new Event
            {
                EventId = 1,
                Name = "Sample Event",
                EventDate = "2024-09-15",
                Location = "New York"
            };

            var attendee = new Attendee
            {
                AttendeeId = 100,
                Name = "John Doe",
                Email = "john.doe@example.com",
                EventId = 1,
                Event = eventInstance
            };

            // Act & Assert
            Assert.AreEqual(100, attendee.AttendeeId, "AttendeeId does not match.");
            Assert.AreEqual("John Doe", attendee.Name, "Name does not match.");
            Assert.AreEqual("john.doe@example.com", attendee.Email, "Email does not match.");
            Assert.AreEqual(1, attendee.EventId, "EventId does not match.");
            Assert.IsNotNull(attendee.Event, "Event should not be null.");
            Assert.AreEqual(eventInstance.Name, attendee.Event.Name, "Event's Name does not match.");
        }

        [Test]
        public void DbContext_HasDbSetProperties()
        {
            // Assert that the context has DbSet properties for Events and Attendees
            Assert.IsNotNull(_context.Events, "Events DbSet is not initialized.");
            Assert.IsNotNull(_context.Attendees, "Attendees DbSet is not initialized.");
        }


        [Test]
        public void EventAttendee_Relationship_IsConfiguredCorrectly()
        {
            // Check if the Event to Attendee relationship is configured as one-to-many
            var model = _context.Model;
            var eventEntity = model.FindEntityType(typeof(Event));
            var attendeeEntity = model.FindEntityType(typeof(Attendee));

            // Assert that the foreign key relationship exists between Attendee and Event
            var foreignKey = attendeeEntity.GetForeignKeys().FirstOrDefault(fk => fk.PrincipalEntityType == eventEntity);

            Assert.IsNotNull(foreignKey, "Foreign key relationship between Attendee and Event is not configured.");
            Assert.AreEqual("EventId", foreignKey.Properties.First().Name, "Foreign key property name is incorrect.");

            // Check if the cascade delete behavior is set
            Assert.AreEqual(DeleteBehavior.Cascade, foreignKey.DeleteBehavior, "Cascade delete behavior is not set correctly.");
        }


        [Test]
        public async Task CreateEvent_ThrowsLocationException_ForInvalidLocation()
        {
            // Arrange
            var newEvent = new Event
            {
                Name = "Test Event",
                EventDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Location = "InvalidLocation" // Invalid location
            };

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Event", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains("Location 'InvalidLocation' is not allowed."), "Expected error message not found in the response.");
        }

        [Test]
        public async Task CreateEvent_ThrowsLocationException_ForOtherLocation()
        {
            // Arrange
            var newEvent = new Event
            {
                Name = "Test Event",
                EventDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Location = "Coimbatore" // Coimbatore location
            };

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Event", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains("Location 'Coimbatore' is not allowed."), "Expected error message not found in the response.");
        }

        private async Task<int> CreateTestEventAndGetId()
        {
            var newEvent = new Event
            {
                Name = "Test Event",
                EventDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Location = "Dallas" // Valid location
            };

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Event", content);
            response.EnsureSuccessStatusCode();

            var createdEvent = JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync());
            return createdEvent.EventId;
        }

        [TearDown]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
    }
}
