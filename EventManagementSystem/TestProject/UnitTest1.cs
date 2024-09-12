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

        private async Task<int> CreateTestEventAndGetId()
        {
            var newEvent = new Event
            {
                Name = "Test Event",
                EventDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Location = "Dallas"
            };

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Event", content);
            response.EnsureSuccessStatusCode();

            var createdEvent = JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync());
            return createdEvent.EventId;
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
        public async Task GetAttendeeById_ReturnsAttendee()
        {
            // Arrange
            int eventId = await CreateTestEventAndGetId(); // Create an event
            int attendeeId = await CreateTestAttendeeAndGetId(eventId); // Create an attendee

            // Act
            var response = await _httpClient.GetAsync($"api/Attendee/{attendeeId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var attendee = JsonConvert.DeserializeObject<Attendee>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(attendee);
            Assert.AreEqual(attendeeId, attendee.AttendeeId);
            Assert.IsNotNull(attendee.Event);
            Assert.AreEqual(eventId, attendee.Event.EventId);
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
        public async Task GetEvents_ReturnsListOfEventsWithAttendees()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Event");

            // Assert
            response.EnsureSuccessStatusCode();
            var events = JsonConvert.DeserializeObject<Event[]>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(events);
            Assert.IsTrue(events.Length > 0);
            Assert.IsNotNull(events[0].Attendees); // Ensure each event has attendees loaded
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

        [TearDown]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
    }
}
