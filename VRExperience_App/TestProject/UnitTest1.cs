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

  

// Test if BatchEnrollmentForm action with valid BatchID redirects to EnrollmentConfirmation action with correct route values
 [Test]
        public void VRExperienceEnrollmentForm_Post_Method_ValidVRExperienceId_RedirectsToEnrollmentConfirmation()
        {
            var vrExperience = new VRExperience { VRExperienceID = 100, ExperienceName = "Virtual Reality Adventure", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 5, Location = "DemoLocation", Description = "DemoDescription" };
            _context.VRExperiences.Add(vrExperience);
            _context.SaveChanges();

            var attendee = new Attendee { AttendeeID = 1, Name = "John Doe", Email = "john@example.com",PhoneNumber = "9876543210", VRExperienceID = VRExperience.VRExperienceID };

            var result = _controller.ExperienceEnrollmentForm(VRExperience.VRExperienceID, attendee) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.AreEqual("EnrollmentConfirmation", result.ActionName);
        }

// //This test checks the invalid classid returns the NotFoundresult or not
//         [Test]
//         public void VRExperienceEnrollmentForm_Get_Method_InvalidVRExperienceId_ReturnsNotFound()
//         {
//             // Arrange
//           var classEntity = new VRExperience { VRExperienceID = 100, ExperienceName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 5};
//             // Act
//             var result = _controller.VRExperienceEnrollmentForm(classEntity.VRExperienceID) as NotFoundResult;

//             // Assert
//             Assert.IsNotNull(result);
//         }

// // Test if VRExperienceEnrollmentForm action with valid data creates a student and redirects to EnrollmentConfirmation
// [Test]
// public void VRExperienceEnrollmentForm_Post_Method_ValidData_CreatesAttendeeAndRedirects()
// {
//     // Arrange
//     var classEntity = new VRExperience { VRExperienceID = 100, ExperienceName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 1 };
//     _context.Experiences.Add(classEntity);
//     _context.SaveChanges();

//     // Act
//     var result = _controller.VRExperienceEnrollmentForm(classEntity.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com" }) as RedirectToActionResult;

//     // Assert
//     Assert.IsNotNull(result);
//     Assert.AreEqual("EnrollmentConfirmation", result.ActionName);

// }


// // Test if VRExperienceEnrollmentForm action with valid data creates a student
// [Test]
// public void VRExperienceEnrollmentForm_Post_Method_ValidData_CreatesAttendee()
// {
//     // Arrange
//     var classEntity = new VRExperience { VRExperienceID = 100, ExperienceName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 1 };
//     _context.Experiences.Add(classEntity);
//     _context.SaveChanges();

//     // Act
//     var result = _controller.VRExperienceEnrollmentForm(classEntity.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com" }) as RedirectToActionResult;

//     // Assert
//     // Check if the student was created and added to the database
//     var student = _context.Attendees.SingleOrDefault(s => s.VRExperienceID == classEntity.VRExperienceID);
//     Assert.IsNotNull(student);
//     Assert.AreEqual("John Doe", student.Name);
//     Assert.AreEqual("john@example.com", student.Email);
// }



// // Test if VRExperienceEnrollmentForm action throws VRExperienceBookingException after reaching capacity 0
// [Test]
// public void VRExperienceEnrollmentForm_Post_Method_VRExperienceFull_ThrowsException()
// {
//     // Arrange
//     var classEntity = new VRExperience { VRExperienceID = 100, ExperienceName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 0 };
//     _context.Experiences.Add(classEntity);
//     _context.SaveChanges();

//     // Act and Assert
//     Assert.Throws<VRExperienceBookingException>(() =>
//     {
//         // Act
//         _controller.VRExperienceEnrollmentForm(classEntity.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com" });
//     });
// }

// // This test checks if VRExperienceBookingException throws the message "Maximum Number Reached" or not
// // Test if VRExperienceEnrollmentForm action throws VRExperienceBookingException with correct message after reaching capacity 0
// [Test]
// public void VRExperienceEnrollmentForm_VRExperienceFull_Post_Method_ThrowsException_with_message()
// {
//     // Arrange
//     var classEntity = new VRExperience { VRExperienceID = 100, ExperienceName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 0, Attendees = new List<Attendee>() };
//     _context.Experiences.Add(classEntity);
//     _context.SaveChanges();

//     // Act and Assert
//     var exception = Assert.Throws<VRExperienceBookingException>(() =>
//     {
//         // Act
//         _controller.VRExperienceEnrollmentForm(classEntity.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com" });
//     });

//     // Assert
//     Assert.AreEqual("Maximum Number Reached", exception.Message);
// }

    
// // This test checks if EnrollmentConfirmation action returns NotFound for a non-existent student ID
//         [Test]
//         public void EnrollmentConfirmation_Get_Method_NonexistentAttendeeId_ReturnsNotFound()
//         {
//             // Arrange
//             var studentId = 1;

//             // Act
//             var result = _controller.EnrollmentConfirmation(studentId) as NotFoundResult;

//             // Assert
//             Assert.IsNotNull(result);
//         }

//         // This test checks the existence of the Attendee class
//         [Test]
//         public void AttendeeVRExperienceExists()
//         {
//             // Arrange
//             var student = new Attendee();

//             // Assert
//             Assert.IsNotNull(student);
//         }

//         // This test checks the existence of the VRExperience class
//         [Test]
//         public void VRExperienceVRExperienceExists()
//         {
//             // Arrange
//             var classEntity = new VRExperience();

//             // Assert
//             Assert.IsNotNull(classEntity);
//         }
 
//  //This test check the exists of ApplicationDbContext class has DbSet of Experiences
//  [Test]
//         public void ApplicationDbContextContainsDbSetVRExperienceProperty()
//         {

//             var propertyInfo = _context.GetType().GetProperty("Experiences");
        
//             Assert.IsNotNull(propertyInfo);
//             Assert.AreEqual(typeof(DbSet<VRExperience>), propertyInfo.PropertyType);
                   
//         }
//         // This test checks the StartTime of VRExperience property is string
//        [Test]
//         public void VRExperience_Properties_VRExperienceID_ReturnExpectedDataTypes()
//         {
//             VRExperience classEntity = new VRExperience();
//             Assert.That(classEntity.VRExperienceID, Is.TypeOf<int>());
//         }

//        // This test checks the StartTime of VRExperience property is string
//         [Test]
//         public void VRExperience_Properties_StartTime_ReturnExpectedDataTypes()
//         {
//             // Arrange
//             VRExperience classEntity = new VRExperience { StartTime = "10:00 AM" };

//             // Assert
//             Assert.That(classEntity.StartTime, Is.TypeOf<string>());
//         }

//         // This test checks the EndTime of VRExperience property is string
//         [Test]
//         public void VRExperience_Properties_EndTime_ReturnExpectedDataTypes()
//         {
//             // Arrange
//             VRExperience classEntity = new VRExperience { EndTime = "12:00 PM" };

//             // Assert
//             Assert.That(classEntity.EndTime, Is.TypeOf<string>());
//         }

//         // This test checks the MaxCapacity of VRExperience property is int
//         [Test]
//         public void VRExperience_Properties_MaxCapacity_ReturnExpectedDataTypes()
//         {
//             VRExperience classEntity = new VRExperience();
//             Assert.That(classEntity.MaxCapacity, Is.TypeOf<int>());
//         }

//         // This test checks the expected value of VRExperienceID
//         [Test]
//         public void VRExperience_Properties_VRExperienceID_ReturnExpectedValues()
//         {
//             // Arrange
//             int expectedVRExperienceID = 100;

//             VRExperience classEntity = new VRExperience
//             {
//                 VRExperienceID = expectedVRExperienceID
//             };
//             Assert.AreEqual(expectedVRExperienceID, classEntity.VRExperienceID);
//         }

//         // This test checks the expected value of StartTime
//         [Test]
//         public void VRExperience_Properties_StartTime_ReturnExpectedValues()
//         {
//             string expectedStartTime = "10:00 AM";

//             VRExperience classEntity = new VRExperience
//             {
//                 StartTime = expectedStartTime
//             };
//             Assert.AreEqual(expectedStartTime, classEntity.StartTime);
//         }

//         // This test checks the expected value of EndTime
//         [Test]
//         public void VRExperience_Properties_EndTime_ReturnExpectedValues()
//         {
//             string expectedEndTime = "12:00 PM";

//             VRExperience classEntity = new VRExperience
//             {
//                 EndTime = expectedEndTime
//             };
//             Assert.AreEqual(expectedEndTime, classEntity.EndTime);
//         }

//         // This test checks the expected value of MaxCapacity
//         [Test]
//         public void VRExperience_Properties_MaxCapacity_ReturnExpectedValues()
//         {
//             int expectedMaxCapacity = 5;
//             VRExperience classEntity = new VRExperience
//             {
//                 MaxCapacity = expectedMaxCapacity
//             };
//             Assert.AreEqual(expectedMaxCapacity, classEntity.MaxCapacity);
//         }

//         // This test checks the expected value of AttendeeID in Attendee class is int
//         [Test]
//         public void Attendee_Properties_AttendeeID_ReturnExpectedDataTypes()
//         {
//             Attendee student = new Attendee();
//             Assert.That(student.AttendeeID, Is.TypeOf<int>());
//         }

//         // This test checks the expected value of Name in Attendee class is string
//         [Test]
//         public void Attendee_Properties_Name_ReturnExpectedDataTypes()
//         {
//             Attendee student = new Attendee();
//             student.Name = "";
//             Assert.That(student.Name, Is.TypeOf<string>());
//         }

//         // This test checks the expected value of Email in Attendee class is string
//         [Test]
//         public void Attendee_Properties_Email_ReturnExpectedDataTypes()
//         {
//             Attendee student = new Attendee();
//             student.Email = "";
//             Assert.That(student.Email, Is.TypeOf<string>());
//         }

//         // This test checks the expected value of VRExperienceID in Attendee class is int
//         [Test]
//         public void Attendee_Properties_VRExperienceID_ReturnExpectedDataTypes()
//         {
//             Attendee student = new Attendee();
//             Assert.That(student.VRExperienceID, Is.TypeOf<int>());
//         }

//         // This test checks the expected value of Email in Attendee class is string
//         [Test]
//         public void Attendee_Properties_Email_ReturnExpectedValues()
//         {
//             string expectedEmail = "john@example.com";

//             Attendee student = new Attendee
//             {
//                 Email = expectedEmail
//             };
//             Assert.AreEqual(expectedEmail, student.Email);
//         }

//         // This test checks the expected value of VRExperience in Attendee class is another entity VRExperience
//         [Test]
//         public void Attendee_Properties_Returns_VRExperience_ExpectedValues()
//         {
//             VRExperience expectedVRExperience = new VRExperience();

//             Attendee student = new Attendee
//             {
//                 VRExperience = expectedVRExperience
//             };
//             Assert.AreEqual(expectedVRExperience, student.VRExperience);
//         }

//         [Test]
//         public void DeleteVRExperience_Post_Method_ValidVRExperienceId_RemovesVRExperienceFromDatabase()
//         {
//             // Arrange
//             var classEntity = new VRExperience { VRExperienceID = 100, ExperienceName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 5 };
//             _context.Experiences.Add(classEntity);
//             _context.SaveChanges();
//             var controller = new VRExperienceController(_context);

//             // Act
//             var result = controller.DeleteVRExperienceConfirmed(classEntity.VRExperienceID).Result as RedirectToActionResult;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("AvailableExperiences", result.ActionName);

//             // Check if the class was removed from the database
//             var deletedVRExperience = _context.Experiences.Find(classEntity.VRExperienceID);
//             Assert.IsNull(deletedVRExperience);
//         }

//         // Test if search by class name returns matching classes
//         [Test]
//         public async Task AvailableExperiences_SearchByName_ReturnsMatchingExperiences()
//         {
//             // Arrange
//             TearDown();
//             var classController = new VRExperienceController(_context);
//             _context.Experiences.AddRange(
//                 new VRExperience { VRExperienceID = 1, ExperienceName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 5 },
//                 new VRExperience { VRExperienceID = 2, ExperienceName = "French Pastry Making", StartTime = "1:00 PM", EndTime = "3:00 PM", MaxCapacity = 5 },
//                 new VRExperience { VRExperienceID = 3, ExperienceName = "Sushi Rolling", StartTime = "4:00 PM", EndTime = "6:00 PM", MaxCapacity = 5 }
//             );
//             _context.SaveChanges();
//             string searchString = "Italian";

//             // Act
//             var result = await classController.AvailableExperiences(searchString) as ViewResult;
//             var classes = result.Model as List<VRExperience>;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.IsInstanceOf<ViewResult>(result);
//             Assert.AreEqual(1, classes.Count);
//             Assert.AreEqual("Italian Cooking", classes.First().ExperienceName);
//         }


//         // Test if empty search string returns all classes
//         [Test]
//         public async Task AvailableExperiences_EmptySearchString_ReturnsAllExperiences()
//         {
//             // Arrange
//             TearDown();
//             var classController = new VRExperienceController(_context);
//             _context.Experiences.AddRange(
//                 new VRExperience { VRExperienceID = 1, ExperienceName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 5 },
//                 new VRExperience { VRExperienceID = 2, ExperienceName = "French Pastry Making", StartTime = "1:00 PM", EndTime = "3:00 PM", MaxCapacity = 5 },
//                 new VRExperience { VRExperienceID = 3, ExperienceName = "Sushi Rolling", StartTime = "4:00 PM", EndTime = "6:00 PM", MaxCapacity = 5 }
//             );
//             _context.SaveChanges();
//             string searchString = string.Empty;

//             // Act
//             var result = await classController.AvailableExperiences(searchString) as ViewResult;
//             var classes = result.Model as List<VRExperience>;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.IsInstanceOf<ViewResult>(result);
//             Assert.AreEqual(3, classes.Count);
//         }

//         // Test if no matching classes returns empty list
//         [Test]
//         public async Task AvailableExperiences_NoMatchingExperiences_ReturnsEmptyList()
//         {
//             // Arrange
//             TearDown();
//             var classController = new VRExperienceController(_context);
//             _context.Experiences.AddRange(
//                 new VRExperience { VRExperienceID = 1, ExperienceName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 5 },
//                 new VRExperience { VRExperienceID = 2, ExperienceName = "French Pastry Making", StartTime = "1:00 PM", EndTime = "3:00 PM", MaxCapacity = 5 },
//                 new VRExperience { VRExperienceID = 3, ExperienceName = "Sushi Rolling", StartTime = "4:00 PM", EndTime = "6:00 PM", MaxCapacity = 5 }
//             );
//             _context.SaveChanges();
//             string searchString = "NonExistentVRExperience";

//             // Act
//             var result = await classController.AvailableExperiences(searchString) as ViewResult;
//             var classes = result.Model as List<VRExperience>;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.IsInstanceOf<ViewResult>(result);
//             Assert.AreEqual(0, classes.Count);
//         }
    }
}

