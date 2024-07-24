using dotnetapp.Controllers;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
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
        }

        // Test if ExperienceEnrollmentForm action with valid VRExperienceID redirects to EnrollmentConfirmation action
        // [Test]
        // public void ExperienceEnrollmentForm_Post_Method_ValidVRExperienceId_RedirectsToEnrollmentConfirmation()
        // {
        //     var vrExperience = new VRExperience 
        //     { 
        //         VRExperienceID = 100, 
        //         ExperienceName = "Virtual Reality Adventure", 
        //         StartTime = "10:00 AM", 
        //         EndTime = "12:00 PM", 
        //         MaxCapacity = 5, 
        //         Location = "DemoLocation", 
        //         Description = "DemoDescription" 
        //     };
        //     _context.VRExperiences.Add(vrExperience);
        //     _context.SaveChanges();

        //     var attendee = new Attendee 
        //     { 
        //         AttendeeID = 1, 
        //         Name = "John Doe", 
        //         Email = "john@example.com",
        //         PhoneNumber = "9876543210", 
        //         VRExperienceID = vrExperience.VRExperienceID 
        //     };

        //     var result = _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, attendee) as RedirectToActionResult;

        //     Assert.NotNull(result);
        //     Assert.AreEqual("EnrollmentConfirmation", result.ActionName);
        // }

        // Test if ExperienceEnrollmentForm action with invalid VRExperienceID returns NotFound
        // [Test]
        // public void ExperienceEnrollmentForm_Get_Method_InvalidVRExperienceId_ReturnsNotFound()
        // {
        //     // Act
        //     var result = _controller.ExperienceEnrollmentForm(999) as NotFoundResult; // Using a non-existent ID

        //     // Assert
        //     Assert.IsNotNull(result);
        // }

        // Test if ExperienceEnrollmentForm action with valid data creates an attendee and redirects to EnrollmentConfirmation
        // [Test]
        // public void ExperienceEnrollmentForm_Post_Method_ValidData_CreatesAttendeeAndRedirects()
        // {
        //     // Arrange
        //     var vrExperience = new VRExperience { VRExperienceID = 100, ExperienceName = "Virtual Reality Adventure", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 1 };
        //     _context.VRExperiences.Add(vrExperience);
        //     _context.SaveChanges();

        //     // Act
        //     var result = _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com" }) as RedirectToActionResult;

        //     // Assert
        //     Assert.IsNotNull(result);
        //     Assert.AreEqual("EnrollmentConfirmation", result.ActionName);

        //     // Check if the attendee was created and added to the database
        //     var attendee = _context.Attendees.SingleOrDefault(a => a.VRExperienceID == vrExperience.VRExperienceID);
        //     Assert.IsNotNull(attendee);
        //     Assert.AreEqual("John Doe", attendee.Name);
        //     Assert.AreEqual("john@example.com", attendee.Email);
        // }

        // Test if ExperienceEnrollmentForm action throws VRExperienceBookingException after reaching capacity 0
        [Test]
        public void ExperienceEnrollmentForm_Post_Method_VRExperienceFull_ThrowsException()
        {
            // Arrange
            var vrExperience = new VRExperience { VRExperienceID = 100, ExperienceName = "Virtual Reality Adventure", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 0 };
            _context.VRExperiences.Add(vrExperience);
            _context.SaveChanges();

            // Act and Assert
            var exception = Assert.Throws<VRExperienceBookingException>(() =>
            {
                _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com" });
            });

            Assert.AreEqual("Maximum Number Reached", exception.Message);
        }

        // Test if EnrollmentConfirmation action returns NotFound for a non-existent attendee ID
        // [Test]
        // public void EnrollmentConfirmation_Get_Method_NonexistentAttendeeId_ReturnsNotFound()
        // {
        //     // Arrange
        //     var attendeeId = 999; // Non-existent attendee ID

        //     // Act
        //     var result = _controller.EnrollmentConfirmation(attendeeId) as NotFoundResult;

        //     // Assert
        //     Assert.IsNotNull(result);
        // }

        // Test if Attendee class exists
        [Test]
        public void Attendee_Class_Exists()
        {
            // Arrange
            var attendee = new Attendee();

            // Assert
            Assert.IsNotNull(attendee);
        }

        // Test if VRExperience class exists
        [Test]
        public void VRExperience_Class_Exists()
        {
            // Arrange
            var vrExperience = new VRExperience();

            // Assert
            Assert.IsNotNull(vrExperience);
        }

        // Test if ApplicationDbContext contains DbSet<VRExperience>
        [Test]
        public void ApplicationDbContextContainsDbSetVRExperienceProperty()
        {
            var propertyInfo = _context.GetType().GetProperty("VRExperiences");
        
            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<VRExperience>), propertyInfo.PropertyType);
        }

        // Test VRExperience properties return expected data types
        [Test]`
        public void VRExperience_Properties_ReturnExpectedDataTypes()
        {
            var vrExperience = new VRExperience();

            Assert.That(vrExperience.VRExperienceID, Is.TypeOf<int>());
            Assert.That(vrExperience.StartTime, Is.TypeOf<string>());
            Assert.That(vrExperience.EndTime, Is.TypeOf<string>());
            Assert.That(vrExperience.MaxCapacity, Is.TypeOf<int>());
        }

        // Test VRExperience properties return expected values
        [Test]
        public void VRExperience_Properties_ReturnExpectedValues()
        {
            var vrExperience = new VRExperience
            {
                VRExperienceID = 100,
                StartTime = "10:00 AM",
                EndTime = "12:00 PM",
                MaxCapacity = 5
            };

            Assert.AreEqual(100, vrExperience.VRExperienceID);
            Assert.AreEqual("10:00 AM", vrExperience.StartTime);
            Assert.AreEqual("12:00 PM", vrExperience.EndTime);
            Assert.AreEqual(5, vrExperience.MaxCapacity);
        }

        // Test Attendee properties return expected data types
        [Test]
        public void Attendee_Properties_ReturnExpectedDataTypes()
        {
            var attendee = new Attendee();

            Assert.That(attendee.AttendeeID, Is.TypeOf<int>());
            Assert.That(attendee.Name, Is.TypeOf<string>());
            Assert.That(attendee.Email, Is.TypeOf<string>());
            Assert.That(attendee.VRExperienceID, Is.TypeOf<int>());
        }

        // Test Attendee properties return expected values
        [Test]
        public void Attendee_Properties_ReturnExpectedValues()
        {
            var attendee = new Attendee
            {
                Email = "john@example.com",
                VRExperience = new VRExperience()
            };

            Assert.AreEqual("john@example.com", attendee.Email);
            Assert.IsNotNull(attendee.VRExperience);
        }

        // Test if DeleteVRExperience action removes VRExperience from database
        [Test]
        public async Task DeleteVRExperience_Post_Method_ValidVRExperienceId_RemovesVRExperienceFromDatabase()
        {
            // Arrange
            var vrExperience = new VRExperience { VRExperienceID = 100, ExperienceName = "Virtual Reality Adventure", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 5 };
            _context.VRExperiences.Add(vrExperience);
            _context.SaveChanges();
            var controller = new VRExperienceController(_context);

            // Act
            var result = await controller.DeleteExperienceConfirmed(vrExperience.VRExperienceID) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AvailableExperiences", result.ActionName);

            // Check if the VRExperience was removed from the database
            var deletedVRExperience = _context.VRExperiences.Find(vrExperience.VRExperienceID);
            Assert.IsNull(deletedVRExperience);
        }

        [Test]
public async Task AvailableExperiences_SortByName_ReturnsSortedExperiences()
{
    // Arrange
    var controller = new VRExperienceController(_context);
    _context.VRExperiences.AddRange(
        new VRExperience { VRExperienceID = 1, ExperienceName = "Zebra", StartTime = "2023-01-01T10:00:00", EndTime = "2023-01-01T12:00:00", MaxCapacity = 10, Location = "Virtual", Description = "Explore the wonders of space in a fully immersive virtual reality experience." },
        new VRExperience { VRExperienceID = 2, ExperienceName = "Apple", StartTime = "2023-01-01T13:00:00", EndTime = "2023-01-01T15:00:00", MaxCapacity = 10, Location = "Virtual", Description = "Explore the wonders of space in a fully immersive virtual reality experience." }
    );
    _context.SaveChanges();

    // Act: Sort by ascending order
    var resultAsc = await controller.AvailableExperiences("asc") as ViewResult;

    // Assert: Check ascending order
    Assert.IsNotNull(resultAsc);
    var modelAsc = resultAsc.Model as IEnumerable<VRExperience>;
    Assert.IsNotNull(modelAsc);
    Assert.AreEqual(2, modelAsc.Count());
    Assert.AreEqual("Apple", modelAsc.First().ExperienceName);  // "Apple" should come first in ascending order
    Assert.AreEqual("Zebra", modelAsc.Last().ExperienceName);   // "Zebra" should come last in ascending order

    // Act: Sort by descending order
    var resultDesc = await controller.AvailableExperiences("desc") as ViewResult;

    // Assert: Check descending order
    Assert.IsNotNull(resultDesc);
    var modelDesc = resultDesc.Model as IEnumerable<VRExperience>;
    Assert.IsNotNull(modelDesc);
    Assert.AreEqual(2, modelDesc.Count());
    Assert.AreEqual("Zebra", modelDesc.First().ExperienceName);  // "Zebra" should come first in descending order
    Assert.AreEqual("Apple", modelDesc.Last().ExperienceName);   // "Apple" should come last in descending order
}

    }
}
