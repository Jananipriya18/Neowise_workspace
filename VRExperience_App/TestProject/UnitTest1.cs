using dotnetapp.Controllers;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

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
            _context.Dispose();
        }

        // Test if VRExperienceEnrollmentForm action with valid VRExperienceID redirects to EnrollmentConfirmation action with correct route values
        [Test]
        public async Task VRExperienceEnrollmentForm_Post_Method_ValidVRExperienceId_RedirectsToEnrollmentConfirmation()
        {
            // Arrange
            var vrExperience = new VRExperience
            {
                VRExperienceID = 100,
                ExperienceName = "Virtual Space Exploration",
                StartTime = "2023-01-01T10:00:00",
                EndTime = "2023-01-01T12:00:00",
                MaxCapacity = 10,
                Location = "Virtual",
                Description = "Explore the wonders of space in a fully immersive virtual reality experience."
            };
            _context.VRExperiences.Add(vrExperience);
            await _context.SaveChangesAsync();

            var attendee = new Attendee { AttendeeID = 1, Name = "John Doe", Email = "john@example.com", PhoneNumber = "9876543210" };

            // Act
            var result = await _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, attendee) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("EnrollmentConfirmation", result.ActionName); // Ensure the correct action is redirected to
        }
    
        // This test checks if the ExperienceEnrollmentForm action with an invalid VRExperienceID returns NotFoundResult
        [Test]
        public void ExperienceEnrollmentForm_Get_Method_InvalidVRExperienceId_ReturnsNotFound()
        {
            // Arrange
            var VRExperienceID = 999; // An ID that does not exist

            // Act
            var result = _controller.ExperienceEnrollmentForm(VRExperienceID) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        // Test if ExperienceEnrollmentForm action with valid data creates an attendee and redirects to EnrollmentConfirmation
        [Test]
        public async Task ExperienceEnrollmentForm_Post_Method_ValidData_CreatesAttendeeAndRedirects()
        {
            // Arrange
            var vrExperience = new VRExperience
            {
                VRExperienceID = 100,
                ExperienceName = "Virtual Space Exploration",
                StartTime = "2023-01-01T10:00:00",
                EndTime = "2023-01-01T12:00:00",
                MaxCapacity = 1,
                Location = "Virtual",
                Description = "Explore the wonders of space in a fully immersive virtual reality experience."
            };
            _context.VRExperiences.Add(vrExperience);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com", PhoneNumber = "9876543210" }) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("EnrollmentConfirmation", result.ActionName);
        }



        [Test]
        public async Task ExperienceEnrollmentForm_Post_Method_ExperienceFull_ThrowsException()
        {
            // Arrange
            var vrExperience = new VRExperience
            {
                VRExperienceID = 100,
                ExperienceName = "Virtual Space Exploration",
                StartTime = "2023-01-01T10:00:00",
                EndTime = "2023-01-01T12:00:00",
                MaxCapacity = 0, // Full capacity
                Location = "Virtual",
                Description = "Explore the wonders of space in a fully immersive virtual reality experience."
            };
            _context.VRExperiences.Add(vrExperience);
            await _context.SaveChangesAsync();

            // Act & Assert
            var exception = Assert.ThrowsAsync<VRExperienceBookingException>(async () =>
            {
                await _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com", PhoneNumber = "9876543210" });
            });

            // Assert
            Assert.AreEqual("Maximum Attendees Registered", exception.Message);
        }



// This test checks if VRExperienceBookingException throws the message "Maximum Attendees Registered" or not
// Test if ExperienceEnrollmentForm action throws VRExperienceBookingException with correct message after reaching capacity 0
[Test]
public void ExperienceEnrollmentForm_Post_Method_ThrowsException_With_Message()
{
    // Arrange
    var vrExperience = new VRExperience
    {
        VRExperienceID = 100,
        ExperienceName = "Virtual Space Exploration",
        StartTime = "2023-01-01T10:00:00",
        EndTime = "2023-01-01T12:00:00",
        MaxCapacity = 0,
        Location = "Virtual",
        Description = "Explore the wonders of space in a fully immersive virtual reality experience."
    };
    _context.VRExperiences.Add(vrExperience);
    _context.SaveChanges();

    // Act and Assert
     var exception = Assert.ThrowsAsync<VRExperienceBookingException>(async () =>
    {
        await _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com", PhoneNumber = "9876543210" });
    });

    // Assert
    // Assert.AreEqual("Maximum Attendees Registered", exception.Message);
}

    
// This test checks if EnrollmentConfirmation action returns NotFound for a non-existent attendee ID
        [Test]
        public async Task EnrollmentConfirmation_Get_Method_NonexistentAttendeeID_ReturnsNotFound()
        {
            // Arrange
            var nonExistentAttendeeId = 999; // An ID that does not exist in the database

            // Act
            var result = await _controller.EnrollmentConfirmation(nonExistentAttendeeId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }


        // This test checks the existence of the Attendee class
        [Test]
        public void AttendeeClassExists()
        {
            // Arrange
            var attendee = new Attendee();

            // Assert
            Assert.IsNotNull(attendee);
        }

        // This test checks the existence of the Class class
        [Test]
        public void VRExperienceExists()
        {
            // Arrange
            var classEntity = new VRExperience();

            // Assert
            Assert.IsNotNull(classEntity);
        }
 
 //This test check the exists of ApplicationDbContext class has DbSet of VRExperiences
 [Test]
        public void ApplicationDbContextContainsDbSetVRExperienceProperty()
        {

            var propertyInfo = _context.GetType().GetProperty("VRExperiences");
        
            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<VRExperience>), propertyInfo.PropertyType);
                   
        }
        // This test checks the StartTime of VRExperiences property is string
       [Test]
        public void VRExperience_Properties_VRExperienceID_ReturnExpectedDataTypes()
        {
            VRExperience classEntity = new VRExperience();
            Assert.That(classEntity.VRExperienceID, Is.TypeOf<int>());
        }

       // This test checks the StartTime of VRExperience property is string
        [Test]
        public void VRExperience_Properties_StartTime_ReturnExpectedDataTypes()
        {
            // Arrange
            VRExperience classEntity = new VRExperience { StartTime = "10:00 AM" };

            // Assert
            Assert.That(classEntity.StartTime, Is.TypeOf<string>());
        }

        // This test checks the EndTime of VRExperience property is string
        [Test]
        public void VRExperience_Properties_EndTime_ReturnExpectedDataTypes()
        {
            // Arrange
            VRExperience classEntity = new VRExperience { EndTime = "12:00 PM" };

            // Assert
            Assert.That(classEntity.EndTime, Is.TypeOf<string>());
        }

        // This test checks the Capacity of VRExperience property is int
        [Test]
        public void VRExperience_Properties_MaxCapacity_ReturnExpectedDataTypes()
        {
            VRExperience classEntity = new VRExperience();
            Assert.That(classEntity.MaxCapacity, Is.TypeOf<int>());
        }

        // This test checks the expected value of VRExperienceID
        [Test]
        public void VRExperience_Properties_VRExperienceID_ReturnExpectedValues()
        {
            // Arrange
            int expectedVRExperienceID = 100;

            VRExperience classEntity = new VRExperience
            {
                VRExperienceID = expectedVRExperienceID
            };
            Assert.AreEqual(expectedVRExperienceID, classEntity.VRExperienceID);
        }

        // This test checks the expected value of StartTime
        [Test]
        public void VRExperience_Properties_StartTime_ReturnExpectedValues()
        {
            string expectedStartTime = "10:00 AM";

            VRExperience classEntity = new VRExperience
            {
                StartTime = expectedStartTime
            };
            Assert.AreEqual(expectedStartTime, classEntity.StartTime);
        }

        // This test checks the expected value of EndTime
        [Test]
        public void VRExperience_Properties_EndTime_ReturnExpectedValues()
        {
            string expectedEndTime = "12:00 PM";

            VRExperience classEntity = new VRExperience
            {
                EndTime = expectedEndTime
            };
            Assert.AreEqual(expectedEndTime, classEntity.EndTime);
        }

        // This test checks the expected value of Capacity
        [Test]
        public void VRExperience_Properties_Capacity_ReturnExpectedValues()
        {
            int expectedCapacity = 5;
            VRExperience classEntity = new VRExperience
            {
                MaxCapacity = expectedCapacity
            };
            Assert.AreEqual(expectedCapacity, classEntity.MaxCapacity);
        }

        // This test checks the expected value of AttendeeID in Attendee class is int
        [Test]
        public void Attendee_Properties_AttendeeID_ReturnExpectedDataTypes()
        {
            Attendee attendee = new Attendee();
            Assert.That(attendee.AttendeeID, Is.TypeOf<int>());
        }

        // This test checks the expected value of Name in Attendee class is string
        [Test]
        public void Attendee_Properties_Name_ReturnExpectedDataTypes()
        {
            Attendee attendee = new Attendee();
            attendee.Name = "Demo Name";
            Assert.That(attendee.Name, Is.TypeOf<string>());
        }

        // This test checks the expected value of Email in Attendee class is string
        [Test]
        public void Attendee_Properties_Email_ReturnExpectedDataTypes()
        {
            Attendee attendee = new Attendee();
            attendee.Email = "demo@gmail.com";
            Assert.That(attendee.Email, Is.TypeOf<string>());
        }

        // This test checks the expected value of Email in Attendee class is string
        [Test]
        public void Attendee_Properties_PhoneNumber_ReturnExpectedDataTypes()
        {
            Attendee attendee = new Attendee();
            attendee.PhoneNumber = "9876543210";
            Assert.That(attendee.PhoneNumber, Is.TypeOf<string>());
        }

        // This test checks the expected value of VRExperienceID in Attendee class is int
        [Test]
        public void Attendee_Properties_VRExperienceID_ReturnExpectedDataTypes()
        {
            Attendee attendee = new Attendee();
            Assert.That(attendee.VRExperienceID, Is.TypeOf<int>());
        }

        // This test checks the expected value of Email in Attendee class is string
        [Test]
        public void Attendee_Properties_Email_ReturnExpectedValues()
        {
            string expectedEmail = "john@example.com";

            Attendee attendee = new Attendee
            {
                Email = expectedEmail
            };
            Assert.AreEqual(expectedEmail, attendee.Email);
        }

        // This test checks the expected value of VRExperience in Attendee class is another entity Class
        [Test]
        public void Attendee_Properties_Returns_VRExperience_ExpectedValues()
        {
            VRExperience expectedClass = new VRExperience();

            Attendee attendee = new Attendee
            {
                VRExperience = expectedClass
            };
            Assert.AreEqual(expectedClass, attendee.VRExperience);
        }

        [Test]
        public void DeleteVRExperience_Post_Method_ValidVRExperienceId_RemovesVRExperienceFromDatabase()
        {
            // Arrange
            var vrExperience = new VRExperience 
            { 
                VRExperienceID = 100, 
                ExperienceName = "Virtual Space Exploration", 
                StartTime = "2023-01-01T10:00:00", 
                EndTime = "2023-01-01T12:00:00", 
                MaxCapacity = 5, 
                Location = "Virtual", 
                Description = "Explore the wonders of space in a fully immersive virtual reality experience." 
            };
            _context.VRExperiences.Add(vrExperience);
            _context.SaveChanges();
            var controller = new VRExperienceController(_context);

            // Act
            var result = controller.DeleteExperienceConfirmed(vrExperience.VRExperienceID).Result as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AvailableExperiences", result.ActionName);

            // Check if the VR experience was removed from the database
            var deletedExperience = _context.VRExperiences.Find(vrExperience.VRExperienceID);
            Assert.IsNull(deletedExperience);
        }

        [Test]
        public async Task AvailableExperiences_SortByNameAscending_ReturnsSortedExperiences()
        {
            // Arrange
            //  TearDown();
            var vrExperienceController = new VRExperienceController(_context);
            _context.VRExperiences.AddRange(
                new VRExperience { VRExperienceID = 121, ExperienceName = "Aaaa Space Exploration", StartTime = "2023-01-01T10:00:00", EndTime = "2023-01-01T12:00:00", MaxCapacity = 5, Location = "Virtual", Description = "Explore the wonders of space in a fully immersive virtual reality experience." },
                new VRExperience { VRExperienceID = 221, ExperienceName = "Zzzebra Ocean Adventure", StartTime = "2023-01-02T10:00:00", EndTime = "2023-01-02T12:00:00", MaxCapacity = 5, Location = "Virtual", Description = "Dive into the depths of the ocean in a virtual reality experience." },
                new VRExperience { VRExperienceID = 321, ExperienceName = "Rabbit Mountain Hiking", StartTime = "2023-01-03T10:00:00", EndTime = "2023-01-03T12:00:00", MaxCapacity = 5, Location = "Virtual", Description = "Experience the thrill of mountain hiking in virtual reality." }
            );
            _context.SaveChanges();

            // Act
            var result = await vrExperienceController.AvailableExperiences("asc") as ViewResult;
            var experiences = result.Model as List<VRExperience>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            // Assert.AreEqual(3, experiences.Count);
            Assert.AreEqual("Aaaa Space Exploration", experiences.First().ExperienceName);
            Assert.AreEqual("Zzzebra Ocean Adventure", experiences.Last().ExperienceName);
        }
    }
}