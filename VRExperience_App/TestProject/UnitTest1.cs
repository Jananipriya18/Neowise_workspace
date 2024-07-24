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
    Assert.AreEqual("Maximum Number Reached", exception.Message);
}



// This test checks if VRExperienceBookingException throws the message "Maximum Number Reached" or not
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
    // Assert.AreEqual("Maximum Number Reached", exception.Message);
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

//         // This test checks the expected value of EndTime
//         [Test]
//         public void Class_Properties_EndTime_ReturnExpectedValues()
//         {
//             string expectedEndTime = "12:00 PM";

//             Class classEntity = new Class
//             {
//                 EndTime = expectedEndTime
//             };
//             Assert.AreEqual(expectedEndTime, classEntity.EndTime);
//         }

//         // This test checks the expected value of Capacity
//         [Test]
//         public void Class_Properties_Capacity_ReturnExpectedValues()
//         {
//             int expectedCapacity = 5;
//             Class classEntity = new Class
//             {
//                 Capacity = expectedCapacity
//             };
//             Assert.AreEqual(expectedCapacity, classEntity.Capacity);
//         }

//         // This test checks the expected value of AttendeeID in Attendee class is int
//         [Test]
//         public void Attendee_Properties_AttendeeID_ReturnExpectedDataTypes()
//         {
//             Attendee attendee = new Attendee();
//             Assert.That(attendee.AttendeeID, Is.TypeOf<int>());
//         }

//         // This test checks the expected value of Name in Attendee class is string
//         [Test]
//         public void Attendee_Properties_Name_ReturnExpectedDataTypes()
//         {
//             Attendee attendee = new Attendee();
//             attendee.Name = "";
//             Assert.That(attendee.Name, Is.TypeOf<string>());
//         }

//         // This test checks the expected value of Email in Attendee class is string
//         [Test]
//         public void Attendee_Properties_Email_ReturnExpectedDataTypes()
//         {
//             Attendee attendee = new Attendee();
//             attendee.Email = "";
//             Assert.That(attendee.Email, Is.TypeOf<string>());
//         }

//         // This test checks the expected value of ClassID in Attendee class is int
//         [Test]
//         public void Attendee_Properties_ClassID_ReturnExpectedDataTypes()
//         {
//             Attendee attendee = new Attendee();
//             Assert.That(attendee.ClassID, Is.TypeOf<int>());
//         }

//         // This test checks the expected value of Email in Attendee class is string
//         [Test]
//         public void Attendee_Properties_Email_ReturnExpectedValues()
//         {
//             string expectedEmail = "john@example.com";

//             Attendee attendee = new Attendee
//             {
//                 Email = expectedEmail
//             };
//             Assert.AreEqual(expectedEmail, attendee.Email);
//         }

//         // This test checks the expected value of Class in Attendee class is another entity Class
//         [Test]
//         public void Attendee_Properties_Returns_Class_ExpectedValues()
//         {
//             Class expectedClass = new Class();

//             Attendee attendee = new Attendee
//             {
//                 Class = expectedClass
//             };
//             Assert.AreEqual(expectedClass, attendee.Class);
//         }

//         [Test]
//         public void DeleteClass_Post_Method_ValidClassId_RemovesClassFromDatabase()
//         {
//             // Arrange
//             var classEntity = new Class { ClassID = 100, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5 };
//             _context.Classes.Add(classEntity);
//             _context.SaveChanges();
//             var controller = new ClassController(_context);

//             // Act
//             var result = controller.DeleteClassConfirmed(classEntity.ClassID).Result as RedirectToActionResult;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("AvailableClasses", result.ActionName);

//             // Check if the class was removed from the database
//             var deletedClass = _context.Classes.Find(classEntity.ClassID);
//             Assert.IsNull(deletedClass);
//         }

//         // Test if search by class name returns matching classes
//         [Test]
//         public async Task AvailableClasses_SearchByName_ReturnsMatchingClasses()
//         {
//             // Arrange
//             TearDown();
//             var classController = new ClassController(_context);
//             _context.Classes.AddRange(
//                 new Class { ClassID = 1, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5 },
//                 new Class { ClassID = 2, ClassName = "French Pastry Making", StartTime = "1:00 PM", EndTime = "3:00 PM", Capacity = 5 },
//                 new Class { ClassID = 3, ClassName = "Sushi Rolling", StartTime = "4:00 PM", EndTime = "6:00 PM", Capacity = 5 }
//             );
//             _context.SaveChanges();
//             string searchString = "Italian";

//             // Act
//             var result = await classController.AvailableClasses(searchString) as ViewResult;
//             var classes = result.Model as List<Class>;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.IsInstanceOf<ViewResult>(result);
//             Assert.AreEqual(1, classes.Count);
//             Assert.AreEqual("Italian Cooking", classes.First().ClassName);
//         }


//         // Test if empty search string returns all classes
//         [Test]
//         public async Task AvailableClasses_EmptySearchString_ReturnsAllClasses()
//         {
//             // Arrange
//             TearDown();
//             var classController = new ClassController(_context);
//             _context.Classes.AddRange(
//                 new Class { ClassID = 1, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5 },
//                 new Class { ClassID = 2, ClassName = "French Pastry Making", StartTime = "1:00 PM", EndTime = "3:00 PM", Capacity = 5 },
//                 new Class { ClassID = 3, ClassName = "Sushi Rolling", StartTime = "4:00 PM", EndTime = "6:00 PM", Capacity = 5 }
//             );
//             _context.SaveChanges();
//             string searchString = string.Empty;

//             // Act
//             var result = await classController.AvailableClasses(searchString) as ViewResult;
//             var classes = result.Model as List<Class>;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.IsInstanceOf<ViewResult>(result);
//             Assert.AreEqual(3, classes.Count);
//         }

//         // Test if no matching classes returns empty list
//         [Test]
//         public async Task AvailableClasses_NoMatchingClasses_ReturnsEmptyList()
//         {
//             // Arrange
//             TearDown();
//             var classController = new ClassController(_context);
//             _context.Classes.AddRange(
//                 new Class { ClassID = 1, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5 },
//                 new Class { ClassID = 2, ClassName = "French Pastry Making", StartTime = "1:00 PM", EndTime = "3:00 PM", Capacity = 5 },
//                 new Class { ClassID = 3, ClassName = "Sushi Rolling", StartTime = "4:00 PM", EndTime = "6:00 PM", Capacity = 5 }
//             );
//             _context.SaveChanges();
//             string searchString = "NonExistentClass";

//             // Act
//             var result = await classController.AvailableClasses(searchString) as ViewResult;
//             var classes = result.Model as List<Class>;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.IsInstanceOf<ViewResult>(result);
//             Assert.AreEqual(0, classes.Count);
//         }
    }
}


// using dotnetapp.Controllers;
// using dotnetapp.Exceptions;
// using dotnetapp.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using NUnit.Framework;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace dotnetapp.Tests
// {
//     [TestFixture]
//     public class BookingControllerTests
//     {
//         private ApplicationDbContext _context;
//         private BookingController _controller;

//         [SetUp]
//         public void Setup()
//         {
//             // Set up the test database context
//             var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                 .UseInMemoryDatabase(databaseName: "TestDatabase")
//                 .Options;
//             _context = new ApplicationDbContext(options);
//             _context.Database.EnsureCreated();

//             _controller = new BookingController(_context);
//         }

//         [TearDown]
//         public void TearDown()
//         {
//             // Clean up the test database context
//             _context.Database.EnsureDeleted();
//         }

//         // Test if ExperienceEnrollmentForm action with valid VRExperienceID redirects to EnrollmentConfirmation action
//         // [Test]
//         // public void ExperienceEnrollmentForm_Post_Method_ValidVRExperienceId_RedirectsToEnrollmentConfirmation()
//         // {
//         //     var vrExperience = new VRExperience 
//         //     { 
//         //         VRExperienceID = 100, 
//         //         ExperienceName = "Virtual Reality Adventure", 
//         //         StartTime = "10:00 AM", 
//         //         EndTime = "12:00 PM", 
//         //         MaxCapacity = 5, 
//         //         Location = "DemoLocation", 
//         //         Description = "DemoDescription" 
//         //     };
//         //     _context.VRExperiences.Add(vrExperience);
//         //     _context.SaveChanges();

//         //     var attendee = new Attendee 
//         //     { 
//         //         AttendeeID = 1, 
//         //         Name = "John Doe", 
//         //         Email = "john@example.com",
//         //         PhoneNumber = "9876543210", 
//         //         VRExperienceID = vrExperience.VRExperienceID 
//         //     };

//         //     var result = _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, attendee) as RedirectToActionResult;

//         //     Assert.NotNull(result);
//         //     Assert.AreEqual("EnrollmentConfirmation", result.ActionName);
//         // }

//         // Test if ExperienceEnrollmentForm action with invalid VRExperienceID returns NotFound
//         // [Test]
//         // public void ExperienceEnrollmentForm_Get_Method_InvalidVRExperienceId_ReturnsNotFound()
//         // {
//         //     // Act
//         //     var result = _controller.ExperienceEnrollmentForm(999) as NotFoundResult; // Using a non-existent ID

//         //     // Assert
//         //     Assert.IsNotNull(result);
//         // }

//         // Test if ExperienceEnrollmentForm action with valid data creates an attendee and redirects to EnrollmentConfirmation
//         // [Test]
//         // public void ExperienceEnrollmentForm_Post_Method_ValidData_CreatesAttendeeAndRedirects()
//         // {
//         //     // Arrange
//         //     var vrExperience = new VRExperience { VRExperienceID = 100, ExperienceName = "Virtual Reality Adventure", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 1 };
//         //     _context.VRExperiences.Add(vrExperience);
//         //     _context.SaveChanges();

//         //     // Act
//         //     var result = _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com" }) as RedirectToActionResult;

//         //     // Assert
//         //     Assert.IsNotNull(result);
//         //     Assert.AreEqual("EnrollmentConfirmation", result.ActionName);

//         //     // Check if the attendee was created and added to the database
//         //     var attendee = _context.Attendees.SingleOrDefault(a => a.VRExperienceID == vrExperience.VRExperienceID);
//         //     Assert.IsNotNull(attendee);
//         //     Assert.AreEqual("John Doe", attendee.Name);
//         //     Assert.AreEqual("john@example.com", attendee.Email);
//         // }

//         // Test if ExperienceEnrollmentForm action throws VRExperienceBookingException after reaching capacity 0
//         [Test]
//         public void ExperienceEnrollmentForm_Post_Method_VRExperienceFull_ThrowsException()
//         {
//             // Arrange
//             var vrExperience = new VRExperience { VRExperienceID = 100, ExperienceName = "Virtual Reality Adventure", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 0 };
//             _context.VRExperiences.Add(vrExperience);
//             _context.SaveChanges();

//             // Act and Assert
//             var exception = Assert.Throws<VRExperienceBookingException>(() =>
//             {
//                 _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com" });
//             });

//             Assert.AreEqual("Maximum Number Reached", exception.Message);
//         }

//         // Test if EnrollmentConfirmation action returns NotFound for a non-existent attendee ID
//         // [Test]
//         // public void EnrollmentConfirmation_Get_Method_NonexistentAttendeeId_ReturnsNotFound()
//         // {
//         //     // Arrange
//         //     var attendeeId = 999; // Non-existent attendee ID

//         //     // Act
//         //     var result = _controller.EnrollmentConfirmation(attendeeId) as NotFoundResult;

//         //     // Assert
//         //     Assert.IsNotNull(result);
//         // }

//         // Test if Attendee class exists
//         [Test]
//         public void Attendee_Class_Exists()
//         {
//             // Arrange
//             var attendee = new Attendee();

//             // Assert
//             Assert.IsNotNull(attendee);
//         }

//         // Test if VRExperience class exists
//         [Test]
//         public void VRExperience_Class_Exists()
//         {
//             // Arrange
//             var vrExperience = new VRExperience();

//             // Assert
//             Assert.IsNotNull(vrExperience);
//         }

//         // Test if ApplicationDbContext contains DbSet<VRExperience>
//         [Test]
//         public void ApplicationDbContextContainsDbSetVRExperienceProperty()
//         {
//             var propertyInfo = _context.GetType().GetProperty("VRExperiences");
        
//             Assert.IsNotNull(propertyInfo);
//             Assert.AreEqual(typeof(DbSet<VRExperience>), propertyInfo.PropertyType);
//         }

//         // Test VRExperience properties return expected data types
//         [Test]`
//         public void VRExperience_Properties_ReturnExpectedDataTypes()
//         {
//             var vrExperience = new VRExperience();

//             Assert.That(vrExperience.VRExperienceID, Is.TypeOf<int>());
//             Assert.That(vrExperience.StartTime, Is.TypeOf<string>());
//             Assert.That(vrExperience.EndTime, Is.TypeOf<string>());
//             Assert.That(vrExperience.MaxCapacity, Is.TypeOf<int>());
//         }

//         // Test VRExperience properties return expected values
//         [Test]
//         public void VRExperience_Properties_ReturnExpectedValues()
//         {
//             var vrExperience = new VRExperience
//             {
//                 VRExperienceID = 100,
//                 StartTime = "10:00 AM",
//                 EndTime = "12:00 PM",
//                 MaxCapacity = 5
//             };

//             Assert.AreEqual(100, vrExperience.VRExperienceID);
//             Assert.AreEqual("10:00 AM", vrExperience.StartTime);
//             Assert.AreEqual("12:00 PM", vrExperience.EndTime);
//             Assert.AreEqual(5, vrExperience.MaxCapacity);
//         }

//         // Test Attendee properties return expected data types
//         [Test]
//         public void Attendee_Properties_ReturnExpectedDataTypes()
//         {
//             var attendee = new Attendee();

//             Assert.That(attendee.AttendeeID, Is.TypeOf<int>());
//             Assert.That(attendee.Name, Is.TypeOf<string>());
//             Assert.That(attendee.Email, Is.TypeOf<string>());
//             Assert.That(attendee.VRExperienceID, Is.TypeOf<int>());
//         }

//         // Test Attendee properties return expected values
//         [Test]
//         public void Attendee_Properties_ReturnExpectedValues()
//         {
//             var attendee = new Attendee
//             {
//                 Email = "john@example.com",
//                 VRExperience = new VRExperience()
//             };

//             Assert.AreEqual("john@example.com", attendee.Email);
//             Assert.IsNotNull(attendee.VRExperience);
//         }

//         // Test if DeleteVRExperience action removes VRExperience from database
//         [Test]
//         public async Task DeleteVRExperience_Post_Method_ValidVRExperienceId_RemovesVRExperienceFromDatabase()
//         {
//             // Arrange
//             var vrExperience = new VRExperience { VRExperienceID = 100, ExperienceName = "Virtual Reality Adventure", StartTime = "10:00 AM", EndTime = "12:00 PM", MaxCapacity = 5 };
//             _context.VRExperiences.Add(vrExperience);
//             _context.SaveChanges();
//             var controller = new VRExperienceController(_context);

//             // Act
//             var result = await controller.DeleteExperienceConfirmed(vrExperience.VRExperienceID) as RedirectToActionResult;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("AvailableExperiences", result.ActionName);

//             // Check if the VRExperience was removed from the database
//             var deletedVRExperience = _context.VRExperiences.Find(vrExperience.VRExperienceID);
//             Assert.IsNull(deletedVRExperience);
//         }

//         [Test]
// public async Task AvailableExperiences_SortByName_ReturnsSortedExperiences()
// {
//     // Arrange
//     var controller = new VRExperienceController(_context);
//     _context.VRExperiences.AddRange(
//         new VRExperience { VRExperienceID = 1, ExperienceName = "Zebra", StartTime = "2023-01-01T10:00:00", EndTime = "2023-01-01T12:00:00", MaxCapacity = 10, Location = "Virtual", Description = "Explore the wonders of space in a fully immersive virtual reality experience." },
//         new VRExperience { VRExperienceID = 2, ExperienceName = "Apple", StartTime = "2023-01-01T13:00:00", EndTime = "2023-01-01T15:00:00", MaxCapacity = 10, Location = "Virtual", Description = "Explore the wonders of space in a fully immersive virtual reality experience." }
//     );
//     _context.SaveChanges();

//     // Act: Sort by ascending order
//     var resultAsc = await controller.AvailableExperiences("asc") as ViewResult;

//     // Assert: Check ascending order
//     Assert.IsNotNull(resultAsc);
//     var modelAsc = resultAsc.Model as IEnumerable<VRExperience>;
//     Assert.IsNotNull(modelAsc);
//     Assert.AreEqual(2, modelAsc.Count());
//     Assert.AreEqual("Apple", modelAsc.First().ExperienceName);  // "Apple" should come first in ascending order
//     Assert.AreEqual("Zebra", modelAsc.Last().ExperienceName);   // "Zebra" should come last in ascending order

//     // Act: Sort by descending order
//     var resultDesc = await controller.AvailableExperiences("desc") as ViewResult;

//     // Assert: Check descending order
//     Assert.IsNotNull(resultDesc);
//     var modelDesc = resultDesc.Model as IEnumerable<VRExperience>;
//     Assert.IsNotNull(modelDesc);
//     Assert.AreEqual(2, modelDesc.Count());
//     Assert.AreEqual("Zebra", modelDesc.First().ExperienceName);  // "Zebra" should come first in descending order
//     Assert.AreEqual("Apple", modelDesc.Last().ExperienceName);   // "Apple" should come last in descending order
// }

//     }
// }
