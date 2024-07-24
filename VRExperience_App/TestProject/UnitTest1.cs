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
        [Test]
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

        // Test if search by name returns matching experiences
        // [Test]
        // public async Task AvailableExperiences_SearchByName_ReturnsMatchingExperiences()
        // {
        //     // Arrange
        //     var controller = new VRExperienceController(_context);
        //     _context.VRExperiences.AddRange(
        //         new VRExperience { VRExperienceID = 1, ExperienceName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 5 },
        //         new VRExperience { VRExperienceID = 2, ExperienceName = "French Pastry Making", StartTime = "1:00 PM", EndTime = "3:00 PM", MaxCapacity = 10 }
        //     );
        //     _context.SaveChanges();

        //     // Act
        //     var result = await controller.AvailableExperiences("Italian") as ViewResult;

        //     // Assert
        //     Assert.IsNotNull(result);
        //     var model = result.Model as IEnumerable<VRExperience>;
        //     Assert.IsNotNull(model);
        //     Assert.AreEqual(1, model.Count());
        //     Assert.AreEqual("Italian Cooking", model.First().ExperienceName);
        // }
    }
}
