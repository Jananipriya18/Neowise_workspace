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
 
//  //This test check the exists of ApplicationDbContext class has DbSet of Classes
//  [Test]
//         public void ApplicationDbContextContainsDbSetClassProperty()
//         {

//             var propertyInfo = _context.GetType().GetProperty("Classes");
        
//             Assert.IsNotNull(propertyInfo);
//             Assert.AreEqual(typeof(DbSet<Class>), propertyInfo.PropertyType);
                   
//         }
//         // This test checks the StartTime of Class property is string
//        [Test]
//         public void Class_Properties_ClassID_ReturnExpectedDataTypes()
//         {
//             Class classEntity = new Class();
//             Assert.That(classEntity.ClassID, Is.TypeOf<int>());
//         }

//        // This test checks the StartTime of Class property is string
//         [Test]
//         public void Class_Properties_StartTime_ReturnExpectedDataTypes()
//         {
//             // Arrange
//             Class classEntity = new Class { StartTime = "10:00 AM" };

//             // Assert
//             Assert.That(classEntity.StartTime, Is.TypeOf<string>());
//         }

//         // This test checks the EndTime of Class property is string
//         [Test]
//         public void Class_Properties_EndTime_ReturnExpectedDataTypes()
//         {
//             // Arrange
//             Class classEntity = new Class { EndTime = "12:00 PM" };

//             // Assert
//             Assert.That(classEntity.EndTime, Is.TypeOf<string>());
//         }

//         // This test checks the Capacity of Class property is int
//         [Test]
//         public void Class_Properties_Capacity_ReturnExpectedDataTypes()
//         {
//             Class classEntity = new Class();
//             Assert.That(classEntity.Capacity, Is.TypeOf<int>());
//         }

//         // This test checks the expected value of ClassID
//         [Test]
//         public void Class_Properties_ClassID_ReturnExpectedValues()
//         {
//             // Arrange
//             int expectedClassID = 100;

//             Class classEntity = new Class
//             {
//                 ClassID = expectedClassID
//             };
//             Assert.AreEqual(expectedClassID, classEntity.ClassID);
//         }

//         // This test checks the expected value of StartTime
//         [Test]
//         public void Class_Properties_StartTime_ReturnExpectedValues()
//         {
//             string expectedStartTime = "10:00 AM";

//             Class classEntity = new Class
//             {
//                 StartTime = expectedStartTime
//             };
//             Assert.AreEqual(expectedStartTime, classEntity.StartTime);
//         }

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

//         // This test checks the expected value of ParticipantID in Participant class is int
//         [Test]
//         public void Participant_Properties_ParticipantID_ReturnExpectedDataTypes()
//         {
//             Participant participant = new Participant();
//             Assert.That(participant.ParticipantID, Is.TypeOf<int>());
//         }

//         // This test checks the expected value of Name in Participant class is string
//         [Test]
//         public void Participant_Properties_Name_ReturnExpectedDataTypes()
//         {
//             Participant participant = new Participant();
//             participant.Name = "";
//             Assert.That(participant.Name, Is.TypeOf<string>());
//         }

//         // This test checks the expected value of Email in Participant class is string
//         [Test]
//         public void Participant_Properties_Email_ReturnExpectedDataTypes()
//         {
//             Participant participant = new Participant();
//             participant.Email = "";
//             Assert.That(participant.Email, Is.TypeOf<string>());
//         }

//         // This test checks the expected value of ClassID in Participant class is int
//         [Test]
//         public void Participant_Properties_ClassID_ReturnExpectedDataTypes()
//         {
//             Participant participant = new Participant();
//             Assert.That(participant.ClassID, Is.TypeOf<int>());
//         }

//         // This test checks the expected value of Email in Participant class is string
//         [Test]
//         public void Participant_Properties_Email_ReturnExpectedValues()
//         {
//             string expectedEmail = "john@example.com";

//             Participant participant = new Participant
//             {
//                 Email = expectedEmail
//             };
//             Assert.AreEqual(expectedEmail, participant.Email);
//         }

//         // This test checks the expected value of Class in Participant class is another entity Class
//         [Test]
//         public void Participant_Properties_Returns_Class_ExpectedValues()
//         {
//             Class expectedClass = new Class();

//             Participant participant = new Participant
//             {
//                 Class = expectedClass
//             };
//             Assert.AreEqual(expectedClass, participant.Class);
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

