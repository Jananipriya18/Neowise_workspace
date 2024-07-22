using dotnetapp.Controllers;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;


namespace dotnetapp.Tests
{
    [TestFixture]
    public class BookingControllerTests
    {
        private ApplicationDbContext _context;
        private BookingController _controller;

        [SetUp]
        public void Setup()
        {
            // Set up the test database context
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();

            _controller = new BookingController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the test database context
            _context.Database.EnsureDeleted();
            // _context.Dispose();
        }

[Test]
public void TourEnrollmentForm_Post_Method_ValidHistoricalTourID_RedirectsToEnrollmentConfirmation()
{
    // Arrange
    var historicalTour = new HistoricalTour
    {
        HistoricalTourID = 100,
        TourName = "Historical Rome Tour",
        StartTime = "10:00 AM",
        EndTime = "12:00 PM",
        Capacity = 5,
        Location = "Rome",
        Description = "Explore the ancient city of Rome"
    };
    _context.HistoricalTours.Add(historicalTour);
    _context.SaveChanges();

    var participant = new Participant
    {
        ParticipantID = 1,
        Name = "Jane Doe",
        Email = "jane@example.com",
        PhoneNumber = "123-456-7890"
    };

    // Act
    var result = _controller.TourEnrollmentForm(historicalTour.HistoricalTourID, participant) as RedirectToActionResult;

    // Assert
    Assert.NotNull(result);
    Assert.AreEqual("EnrollmentConfirmation", result.ActionName); // Ensure the correct action is redirected to
    Assert.AreEqual(participant.ParticipantID, result.RouteValues["participantId"]); // Ensure the route values are correct
}


// This test checks if an invalid HistoricalTourID returns the NotFoundResult
[Test]
public void TourEnrollmentForm_Get_Method_InvalidHistoricalTourID_ReturnsNotFound()
{
    // Arrange
    var invalidHistoricalTourID = 999; // An ID that does not exist in the database

    // Act
    var result = _controller.TourEnrollmentForm(invalidHistoricalTourID) as NotFoundResult;

    // Assert
    Assert.IsNotNull(result);
}

// Test if TourEnrollmentForm action with valid data creates a participant and redirects to EnrollmentConfirmation
[Test]
public void TourEnrollmentForm_Post_Method_ValidData_CreatesParticipantAndRedirects()
{
    // Arrange
    var tour = new HistoricalTour { HistoricalTourID = 100, TourName = "Historical Tour", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 1,Location = "Rome", Description = "Explore the ancient city of Rome" };
    _context.HistoricalTours.Add(tour);
    _context.SaveChanges();

    var participant = new Participant { Name = "John Doe", Email = "john@example.com", PhoneNumber = "123-456-7890", HistoricalTourID = tour.HistoricalTourID };

    // Act
    var result = _controller.TourEnrollmentForm(tour.HistoricalTourID, participant) as RedirectToActionResult;

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual("EnrollmentConfirmation", result.ActionName);

    var createdParticipant = _context.Participants.FirstOrDefault(p => p.Email == "john@example.com");
    Assert.IsNotNull(createdParticipant);
    Assert.AreEqual("John Doe", createdParticipant.Name);
}



// Test if TourEnrollmentForm action with valid data creates a participant
[Test]
public void TourEnrollmentForm_Post_Method_ValidData_CreatesParticipant()
{
    // Arrange
    var tour = new HistoricalTour { HistoricalTourID = 100, TourName = "Historical Tour", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 1 , Location = "Rome", Description = "Explore the ancient city of Rome"};
    _context.HistoricalTours.Add(tour);
    _context.SaveChanges();

    // Act
    var result = _controller.TourEnrollmentForm(tour.HistoricalTourID, new Participant { Name = "John Doe", Email = "john@example.com", PhoneNumber = "123-456-7890" }) as RedirectToActionResult;

    // Assert
    // Check if the participant was created and added to the database
    var participant = _context.Participants.SingleOrDefault(p => p.HistoricalTourID == tour.HistoricalTourID);
    Assert.IsNotNull(participant);
    Assert.AreEqual("John Doe", participant.Name);
    Assert.AreEqual("john@example.com", participant.Email);
    Assert.AreEqual("123-456-7890", participant.PhoneNumber);
}




// Test if TourEnrollmentForm action throws HistoricalTourBookingException after reaching capacity 0
[Test]
public void TourEnrollmentForm_Post_Method_TourFull_ThrowsException()
{
    // Arrange
    var tour = new HistoricalTour { HistoricalTourID = 100, TourName = "Historical Tour", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 0 , Location = "Rome", Description = "Explore the ancient city of Rome" };
    _context.HistoricalTours.Add(tour);
    _context.SaveChanges();

    // Act and Assert
    Assert.Throws<HistoricalTourBookingException>(() =>
    {
        // Act
        _controller.TourEnrollmentForm(tour.HistoricalTourID, new Participant { Name = "John Doe", Email = "john@example.com", PhoneNumber = "123-456-7890" });
    });
}

// Test if TourEnrollmentForm action throws HistoricalTourBookingException with correct message after reaching capacity 0
[Test]
public void TourEnrollmentForm_TourFull_Post_Method_ThrowsException_with_message()
{
    // Arrange
    var tour = new HistoricalTour { HistoricalTourID = 100, TourName = "Historical Tour", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 0, Location = "Rome", Description = "Explore the ancient city of Rome" };
    // Participants = new List<Participant>() };
    _context.HistoricalTours.Add(tour);
    _context.SaveChanges();

    // Act and Assert
    var exception = Assert.Throws<HistoricalTourBookingException>(() =>
    {
        // Act
        _controller.TourEnrollmentForm(tour.HistoricalTourID, new Participant { Name = "John Doe", Email = "john@example.com", PhoneNumber = "123-456-7890" });
    });

    // Assert
    Assert.AreEqual("Maximum Number Reached", exception.Message);
}

    
// This test checks if EnrollmentConfirmation action returns NotFound for a non-existent participant ID
        [Test]
        public void EnrollmentConfirmation_Get_Method_NonexistentParticipantId_ReturnsNotFound()
        {
            // Arrange
            var participantId = 1;

            // Act
            var result = _controller.EnrollmentConfirmation(participantId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        // This test checks the existence of the Participant class
        [Test]
        public void ParticipantClassExists()
        {
            // Arrange
            var participant = new Participant();

            // Assert
            Assert.IsNotNull(participant);
        }

        // This test checks the existence of the Participant class
        [Test]
        public void ParticipantExists()
        {
            // Arrange
            var classEntity = new Participant();

            // Assert
            Assert.IsNotNull(classEntity);
        }
 
 //This test check the exists of ApplicationDbContext class has DbSet of Participants
 [Test]
        public void ApplicationDbContextContainsDbSetParticipantProperty()
        {

            var propertyInfo = _context.GetType().GetProperty("Participants");
        
            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<Participant>), propertyInfo.PropertyType);
                   
        }
        // This test checks the StartTime of Participant property is string
       [Test]
        public void HistoricalTour_Properties_HistoricalTourID_ReturnExpectedDataTypes()
        {
            HistoricalTour classEntity = new HistoricalTour();
            Assert.That(classEntity.HistoricalTourID, Is.TypeOf<int>());
        }

       // This test checks the StartTime of HistoricalTour property is string
        [Test]
        public void HistoricalTour_Properties_StartTime_ReturnExpectedDataTypes()
        {
            // Arrange
            HistoricalTour classEntity = new HistoricalTour { StartTime = "10:00 AM" };

            // Assert
            Assert.That(classEntity.StartTime, Is.TypeOf<string>());
        }

        // This test checks the EndTime of HistoricalTour property is string
        [Test]
        public void HistoricalTour_Properties_EndTime_ReturnExpectedDataTypes()
        {
            // Arrange
            HistoricalTour classEntity = new HistoricalTour { EndTime = "12:00 PM" };

            // Assert
            Assert.That(classEntity.EndTime, Is.TypeOf<string>());
        }

        // This test checks the Capacity of HistoricalTour property is int
        [Test]
        public void HistoricalTour_Properties_Capacity_ReturnExpectedDataTypes()
        {
            HistoricalTour classEntity = new HistoricalTour();
            Assert.That(classEntity.Capacity, Is.TypeOf<int>());
        }

        // This test checks the expected value of ParticipantID
        [Test]
        public void Participant_Properties_ParticipantID_ReturnExpectedValues()
        {
            // Arrange
            int expectedParticipantID = 100;

            Participant classEntity = new Participant
            {
                ParticipantID = expectedParticipantID
            };
            Assert.AreEqual(expectedParticipantID, classEntity.ParticipantID);
        }

        // This test checks the expected value of StartTime
        [Test]
        public void HistoricalTour_Properties_StartTime_ReturnExpectedValues()
        {
            string expectedStartTime = "10:00 AM";

            HistoricalTour classEntity = new HistoricalTour
            {
                StartTime = expectedStartTime
            };
            Assert.AreEqual(expectedStartTime, classEntity.StartTime);
        }

        // This test checks the expected value of EndTime
        [Test]
        public void HistoricalTour_Properties_EndTime_ReturnExpectedValues()
        {
            string expectedEndTime = "12:00 PM";

            HistoricalTour classEntity = new HistoricalTour
            {
                EndTime = expectedEndTime
            };
            Assert.AreEqual(expectedEndTime, classEntity.EndTime);
        }

        // This test checks the expected value of Capacity
        [Test]
        public void HistoricalTour_Properties_Capacity_ReturnExpectedValues()
        {
            int expectedCapacity = 5;
            HistoricalTour classEntity = new HistoricalTour
            {
                Capacity = expectedCapacity
            };
            Assert.AreEqual(expectedCapacity, classEntity.Capacity);
        }

        // This test checks the expected value of ParticipantID in Participant class is int
        [Test]
        public void Participant_Properties_ParticipantID_ReturnExpectedDataTypes()
        {
            Participant participant = new Participant();
            Assert.That(participant.ParticipantID, Is.TypeOf<int>());
        }

        // This test checks the expected value of Name in Participant class is string
        [Test]
        public void Participant_Properties_Name_ReturnExpectedDataTypes()
        {
            Participant participant = new Participant();
            participant.Name = "";
            Assert.That(participant.Name, Is.TypeOf<string>());
        }

        // This test checks the expected value of Email in Participant class is string
        [Test]
        public void Participant_Properties_Email_ReturnExpectedDataTypes()
        {
            Participant participant = new Participant();
            participant.Email = "";
            Assert.That(participant.Email, Is.TypeOf<string>());
        }

        // This test checks the expected value of HistoricalTourID in Participant class is int
        [Test]
        public void Participant_Properties_HistoricalTourID_ReturnExpectedDataTypes()
        {
            Participant participant = new Participant();
            Assert.That(participant.HistoricalTourID, Is.TypeOf<int>());
        }

        // This test checks the expected value of Email in Participant class is string
        [Test]
        public void Participant_Properties_Email_ReturnExpectedValues()
        {
            string expectedEmail = "john@example.com";

            Participant participant = new Participant
            {
                Email = expectedEmail
            };
            Assert.AreEqual(expectedEmail, participant.Email);
        }

        // This test checks the expected value of PhoneNumber in Participant class is string
        [Test]
        public void Participant_Properties_PhoneNumber_ReturnExpectedValues()
        {
            string expectedPhoneNumber = "9876543210";

            Participant participant = new Participant
            {
                PhoneNumber = expectedPhoneNumber
            };
            Assert.AreEqual(expectedPhoneNumber, participant.PhoneNumber);
        }

        [Test]
        public void Participant_HistoricalTour_Returns_ExpectedValue()
        {
            // Arrange
            var expectedTour = new HistoricalTour
            {
                HistoricalTourID = 100,
                TourName = "Historical Tour",
                StartTime = "10:00 AM",
                EndTime = "12:00 PM",
                Capacity = 10,
                Location = "Rome",
                Description = "Explore the ancient city of Rome"
            };

            var participant = new Participant
            {
                HistoricalTour = expectedTour
            };

            // Act
            var actualTour = participant.HistoricalTour;

            // Assert
            Assert.That(actualTour, Is.EqualTo(expectedTour));
        }


        [Test]
        public async Task DeleteHistoricalTour_Post_Method_ValidTourId_RemovesTourFromDatabase()
        {
            // Arrange
            var tour = new HistoricalTour 
            { 
                HistoricalTourID = 100, 
                TourName = "Historical Tour", 
                StartTime = "10:00 AM", 
                EndTime = "12:00 PM", 
                Capacity = 5, 
                Location = "Rome", 
                Description = "Explore the ancient city of Rome" 
            };
            _context.HistoricalTours.Add(tour);
            await _context.SaveChangesAsync(); // Ensure the data is saved asynchronously

            var controller = new HistoricalTourController(_context);

            // Act
            var result = await controller.DeleteTourConfirmed(tour.HistoricalTourID) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AvailableTours", result.ActionName);

            // Check if the tour was removed from the database
            var deletedTour = await _context.HistoricalTours.FindAsync(tour.HistoricalTourID);
            Assert.IsNull(deletedTour);
        }


        [Test]
        public async Task AvailableTours_SearchByName_ReturnsMatchingTours()
        {
            // Arrange
            TearDown(); // Ensure the database is reset before the test
            var tourController = new HistoricalTourController(_context);

            // Add historical tours to the database
            _context.HistoricalTours.AddRange(
                new HistoricalTour { HistoricalTourID = 1, TourName = "Ancient Rome", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5, Location = "Rome", Description = "Explore ancient Rome" },
                new HistoricalTour { HistoricalTourID = 2, TourName = "Medieval London", StartTime = "1:00 PM", EndTime = "3:00 PM", Capacity = 5, Location = "London", Description = "Discover medieval London" },
                new HistoricalTour { HistoricalTourID = 3, TourName = "Renaissance Florence", StartTime = "4:00 PM", EndTime = "6:00 PM", Capacity = 5, Location = "Florence", Description = "Experience Renaissance Florence" }
            );
            await _context.SaveChangesAsync();

            // Define the search string
            string searchString = "Ancient";

            // Act
            var result = await tourController.AvailableTours(searchString) as ViewResult;
            var tours = result.Model as List<HistoricalTour>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(1, tours.Count);
            Assert.AreEqual("Ancient Rome", tours.First().TourName);
        }


        [Test]
        public async Task AvailableTours_EmptySearchString_ReturnsAllTours()
        {
            // Arrange
            TearDown(); // Ensure the database is reset before the test
            var tourController = new HistoricalTourController(_context);

            // Add historical tours to the database
            _context.HistoricalTours.AddRange(
                new HistoricalTour { HistoricalTourID = 1, TourName = "Ancient Rome", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5, Location = "Rome", Description = "Explore ancient Rome" },
                new HistoricalTour { HistoricalTourID = 2, TourName = "Medieval London", StartTime = "1:00 PM", EndTime = "3:00 PM", Capacity = 5, Location = "London", Description = "Discover medieval London" },
                new HistoricalTour { HistoricalTourID = 3, TourName = "Renaissance Florence", StartTime = "4:00 PM", EndTime = "6:00 PM", Capacity = 5, Location = "Florence", Description = "Experience Renaissance Florence" }
            );
            await _context.SaveChangesAsync();

            string searchString = string.Empty; // Empty search string

            // Act
            var result = await tourController.AvailableTours(searchString) as ViewResult;
            var tours = result.Model as List<HistoricalTour>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(3, tours.Count); // Check that all tours are returned
        }

        [Test]
        public async Task AvailableTours_NoMatchingTours_ReturnsEmptyList()
        {
            // Arrange
            TearDown(); // Ensure the database is reset before the test
            var tourController = new HistoricalTourController(_context);

            // Add historical tours to the database
            _context.HistoricalTours.AddRange(
                new HistoricalTour { HistoricalTourID = 1, TourName = "Ancient Rome", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5, Location = "Rome", Description = "Explore ancient Rome" },
                new HistoricalTour { HistoricalTourID = 2, TourName = "Medieval London", StartTime = "1:00 PM", EndTime = "3:00 PM", Capacity = 5, Location = "London", Description = "Discover medieval London" },
                new HistoricalTour { HistoricalTourID = 3, TourName = "Renaissance Florence", StartTime = "4:00 PM", EndTime = "6:00 PM", Capacity = 5, Location = "Florence", Description = "Experience Renaissance Florence" }
            );
            await _context.SaveChangesAsync();

            string searchString = "NonExistentTour"; // Search string that does not match any tour

            // Act
            var result = await tourController.AvailableTours(searchString) as ViewResult;
            var tours = result.Model as List<HistoricalTour>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(0, tours.Count); // Verify that the list is empty
        }
    }
}

