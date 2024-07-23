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
        public void ClassEnrollmentForm_Post_Method_ValidClassId_RedirectsToEnrollmentConfirmation()
        {
            // Arrange
            var classEntity = new Class { ClassID = 100, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5 };
            _context.Classes.Add(classEntity);
            _context.SaveChanges();

            var student = new Student { StudentID = 1, Name = "John Doe", Email = "john@example.com" };

            // Act
            var result = _controller.ClassEnrollmentForm(classEntity.ClassID, student) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("EnrollmentConfirmation", result.ActionName); // Ensure the correct action is redirected to
        }

//This test checks the invalid classid returns the NotFoundresult or not
        [Test]
        public void ClassEnrollmentForm_Get_Method_InvalidClassId_ReturnsNotFound()
        {
            // Arrange
          var classEntity = new Class { ClassID = 100, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5};
            // Act
            var result = _controller.ClassEnrollmentForm(classEntity.ClassID) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

// Test if ClassEnrollmentForm action with valid data creates a student and redirects to EnrollmentConfirmation
[Test]
public void ClassEnrollmentForm_Post_Method_ValidData_CreatesStudentAndRedirects()
{
    // Arrange
    var classEntity = new Class { ClassID = 100, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 1 };
    _context.Classes.Add(classEntity);
    _context.SaveChanges();

    // Act
    var result = _controller.ClassEnrollmentForm(classEntity.ClassID, new Student { Name = "John Doe", Email = "john@example.com" }) as RedirectToActionResult;

    // Assert
    Assert.IsNotNull(result);
    Assert.AreEqual("EnrollmentConfirmation", result.ActionName);

}


// Test if ClassEnrollmentForm action with valid data creates a student
[Test]
public void ClassEnrollmentForm_Post_Method_ValidData_CreatesStudent()
{
    // Arrange
    var classEntity = new Class { ClassID = 100, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 1 };
    _context.Classes.Add(classEntity);
    _context.SaveChanges();

    // Act
    var result = _controller.ClassEnrollmentForm(classEntity.ClassID, new Student { Name = "John Doe", Email = "john@example.com" }) as RedirectToActionResult;

    // Assert
    // Check if the student was created and added to the database
    var student = _context.Students.SingleOrDefault(s => s.ClassID == classEntity.ClassID);
    Assert.IsNotNull(student);
    Assert.AreEqual("John Doe", student.Name);
    Assert.AreEqual("john@example.com", student.Email);
}



// Test if ClassEnrollmentForm action throws CookingClassBookingException after reaching capacity 0
[Test]
public void ClassEnrollmentForm_Post_Method_ClassFull_ThrowsException()
{
    // Arrange
    var classEntity = new Class { ClassID = 100, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 0 };
    _context.Classes.Add(classEntity);
    _context.SaveChanges();

    // Act and Assert
    Assert.Throws<CookingClassBookingException>(() =>
    {
        // Act
        _controller.ClassEnrollmentForm(classEntity.ClassID, new Student { Name = "John Doe", Email = "john@example.com" });
    });
}

// This test checks if CookingClassBookingException throws the message "Maximum Number Reached" or not
// Test if ClassEnrollmentForm action throws CookingClassBookingException with correct message after reaching capacity 0
[Test]
public void ClassEnrollmentForm_ClassFull_Post_Method_ThrowsException_with_message()
{
    // Arrange
    var classEntity = new Class { ClassID = 100, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 0, Students = new List<Student>() };
    _context.Classes.Add(classEntity);
    _context.SaveChanges();

    // Act and Assert
    var exception = Assert.Throws<CookingClassBookingException>(() =>
    {
        // Act
        _controller.ClassEnrollmentForm(classEntity.ClassID, new Student { Name = "John Doe", Email = "john@example.com" });
    });

    // Assert
    Assert.AreEqual("Maximum Number Reached", exception.Message);
}

    
// This test checks if EnrollmentConfirmation action returns NotFound for a non-existent student ID
        [Test]
        public void EnrollmentConfirmation_Get_Method_NonexistentStudentId_ReturnsNotFound()
        {
            // Arrange
            var studentId = 1;

            // Act
            var result = _controller.EnrollmentConfirmation(studentId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        // This test checks the existence of the Student class
        [Test]
        public void StudentClassExists()
        {
            // Arrange
            var student = new Student();

            // Assert
            Assert.IsNotNull(student);
        }

        // This test checks the existence of the Class class
        [Test]
        public void ClassClassExists()
        {
            // Arrange
            var classEntity = new Class();

            // Assert
            Assert.IsNotNull(classEntity);
        }
 
 //This test check the exists of ApplicationDbContext class has DbSet of Classes
 [Test]
        public void ApplicationDbContextContainsDbSetClassProperty()
        {

            var propertyInfo = _context.GetType().GetProperty("Classes");
        
            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<Class>), propertyInfo.PropertyType);
                   
        }
        // This test checks the StartTime of Class property is string
       [Test]
        public void Class_Properties_ClassID_ReturnExpectedDataTypes()
        {
            Class classEntity = new Class();
            Assert.That(classEntity.ClassID, Is.TypeOf<int>());
        }

       // This test checks the StartTime of Class property is string
        [Test]
        public void Class_Properties_StartTime_ReturnExpectedDataTypes()
        {
            // Arrange
            Class classEntity = new Class { StartTime = "10:00 AM" };

            // Assert
            Assert.That(classEntity.StartTime, Is.TypeOf<string>());
        }

        // This test checks the EndTime of Class property is string
        [Test]
        public void Class_Properties_EndTime_ReturnExpectedDataTypes()
        {
            // Arrange
            Class classEntity = new Class { EndTime = "12:00 PM" };

            // Assert
            Assert.That(classEntity.EndTime, Is.TypeOf<string>());
        }

        // This test checks the Capacity of Class property is int
        [Test]
        public void Class_Properties_Capacity_ReturnExpectedDataTypes()
        {
            Class classEntity = new Class();
            Assert.That(classEntity.Capacity, Is.TypeOf<int>());
        }

        // This test checks the expected value of ClassID
        [Test]
        public void Class_Properties_ClassID_ReturnExpectedValues()
        {
            // Arrange
            int expectedClassID = 100;

            Class classEntity = new Class
            {
                ClassID = expectedClassID
            };
            Assert.AreEqual(expectedClassID, classEntity.ClassID);
        }

        // This test checks the expected value of StartTime
        [Test]
        public void Class_Properties_StartTime_ReturnExpectedValues()
        {
            string expectedStartTime = "10:00 AM";

            Class classEntity = new Class
            {
                StartTime = expectedStartTime
            };
            Assert.AreEqual(expectedStartTime, classEntity.StartTime);
        }

        // This test checks the expected value of EndTime
        [Test]
        public void Class_Properties_EndTime_ReturnExpectedValues()
        {
            string expectedEndTime = "12:00 PM";

            Class classEntity = new Class
            {
                EndTime = expectedEndTime
            };
            Assert.AreEqual(expectedEndTime, classEntity.EndTime);
        }

        // This test checks the expected value of Capacity
        [Test]
        public void Class_Properties_Capacity_ReturnExpectedValues()
        {
            int expectedCapacity = 5;
            Class classEntity = new Class
            {
                Capacity = expectedCapacity
            };
            Assert.AreEqual(expectedCapacity, classEntity.Capacity);
        }

        // This test checks the expected value of StudentID in Student class is int
        [Test]
        public void Student_Properties_StudentID_ReturnExpectedDataTypes()
        {
            Student student = new Student();
            Assert.That(student.StudentID, Is.TypeOf<int>());
        }

        // This test checks the expected value of Name in Student class is string
        [Test]
        public void Student_Properties_Name_ReturnExpectedDataTypes()
        {
            Student student = new Student();
            student.Name = "";
            Assert.That(student.Name, Is.TypeOf<string>());
        }

        // This test checks the expected value of Email in Student class is string
        [Test]
        public void Student_Properties_Email_ReturnExpectedDataTypes()
        {
            Student student = new Student();
            student.Email = "";
            Assert.That(student.Email, Is.TypeOf<string>());
        }

        // This test checks the expected value of ClassID in Student class is int
        [Test]
        public void Student_Properties_ClassID_ReturnExpectedDataTypes()
        {
            Student student = new Student();
            Assert.That(student.ClassID, Is.TypeOf<int>());
        }

        // This test checks the expected value of Email in Student class is string
        [Test]
        public void Student_Properties_Email_ReturnExpectedValues()
        {
            string expectedEmail = "john@example.com";

            Student student = new Student
            {
                Email = expectedEmail
            };
            Assert.AreEqual(expectedEmail, student.Email);
        }

        // This test checks the expected value of Class in Student class is another entity Class
        [Test]
        public void Student_Properties_Returns_Class_ExpectedValues()
        {
            Class expectedClass = new Class();

            Student student = new Student
            {
                Class = expectedClass
            };
            Assert.AreEqual(expectedClass, student.Class);
        }

        [Test]
        public void DeleteClass_Post_Method_ValidClassId_RemovesClassFromDatabase()
        {
            // Arrange
            var classEntity = new Class { ClassID = 100, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5 };
            _context.Classes.Add(classEntity);
            _context.SaveChanges();
            var controller = new ClassController(_context);

            // Act
            var result = controller.DeleteClassConfirmed(classEntity.ClassID).Result as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AvailableClasses", result.ActionName);

            // Check if the class was removed from the database
            var deletedClass = _context.Classes.Find(classEntity.ClassID);
            Assert.IsNull(deletedClass);
        }

        // Test if search by class name returns matching classes
        [Test]
        public async Task AvailableClasses_SearchByName_ReturnsMatchingClasses()
        {
            // Arrange
            TearDown();
            var classController = new ClassController(_context);
            _context.Classes.AddRange(
                new Class { ClassID = 1, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5 },
                new Class { ClassID = 2, ClassName = "French Pastry Making", StartTime = "1:00 PM", EndTime = "3:00 PM", Capacity = 5 },
                new Class { ClassID = 3, ClassName = "Sushi Rolling", StartTime = "4:00 PM", EndTime = "6:00 PM", Capacity = 5 }
            );
            _context.SaveChanges();
            string searchString = "Italian";

            // Act
            var result = await classController.AvailableClasses(searchString) as ViewResult;
            var classes = result.Model as List<Class>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(1, classes.Count);
            Assert.AreEqual("Italian Cooking", classes.First().ClassName);
        }


        // Test if empty search string returns all classes
        [Test]
        public async Task AvailableClasses_EmptySearchString_ReturnsAllClasses()
        {
            // Arrange
            TearDown();
            var classController = new ClassController(_context);
            _context.Classes.AddRange(
                new Class { ClassID = 1, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5 },
                new Class { ClassID = 2, ClassName = "French Pastry Making", StartTime = "1:00 PM", EndTime = "3:00 PM", Capacity = 5 },
                new Class { ClassID = 3, ClassName = "Sushi Rolling", StartTime = "4:00 PM", EndTime = "6:00 PM", Capacity = 5 }
            );
            _context.SaveChanges();
            string searchString = string.Empty;

            // Act
            var result = await classController.AvailableClasses(searchString) as ViewResult;
            var classes = result.Model as List<Class>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(3, classes.Count);
        }

        // Test if no matching classes returns empty list
        [Test]
        public async Task AvailableClasses_NoMatchingClasses_ReturnsEmptyList()
        {
            // Arrange
            TearDown();
            var classController = new ClassController(_context);
            _context.Classes.AddRange(
                new Class { ClassID = 1, ClassName = "Italian Cooking", StartTime = "10:00 AM", EndTime = "12:00 PM", Capacity = 5 },
                new Class { ClassID = 2, ClassName = "French Pastry Making", StartTime = "1:00 PM", EndTime = "3:00 PM", Capacity = 5 },
                new Class { ClassID = 3, ClassName = "Sushi Rolling", StartTime = "4:00 PM", EndTime = "6:00 PM", Capacity = 5 }
            );
            _context.SaveChanges();
            string searchString = "NonExistentClass";

            // Act
            var result = await classController.AvailableClasses(searchString) as ViewResult;
            var classes = result.Model as List<Class>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(0, classes.Count);
        }
    }
}

