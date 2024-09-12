using NUnit.Framework;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class StudentControllerTests
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

        private async Task<int> CreateTestStudentAndGetId()
        {
            var newStudent = new Student
            {
                Name = "Test Student",
                Email = "teststudent@example.com",
                PhoneNumber = "123-456-7890",
                Department = "Test Department"
            };

            var json = JsonConvert.SerializeObject(newStudent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Student", content);
            response.EnsureSuccessStatusCode();

            var createdStudent = JsonConvert.DeserializeObject<Student>(await response.Content.ReadAsStringAsync());
            return createdStudent.StudentId;
        }

        [Test]
        public async Task CreateStudent_ReturnsCreatedStudent()
        {
            // Arrange
            var newStudent = new Student
            {
                Name = "Test Student",
                Email = "teststudent@example.com",
                PhoneNumber = "123-456-7890",
                Department = "Test Department"
            };

            var json = JsonConvert.SerializeObject(newStudent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Student", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdStudent = JsonConvert.DeserializeObject<Student>(responseContent);
            Assert.IsNotNull(createdStudent);
            Assert.AreEqual(newStudent.Name, createdStudent.Name);
            Assert.AreEqual(newStudent.Email, createdStudent.Email);
            Assert.AreEqual(newStudent.PhoneNumber, createdStudent.PhoneNumber);
            Assert.AreEqual(newStudent.Department, createdStudent.Department);
        }

        [Test]
        public async Task SearchStudentByName_ReturnsStudentWithCourses()
        {
            // Arrange
            string studentName = "Test Student"; // Use an existing student name

            // Act
            var response = await _httpClient.GetAsync($"api/Student/Search?name={studentName}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
            else
            {
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var student = JsonConvert.DeserializeObject<Student[]>(responseContent);
                Assert.IsNotNull(student);
                Assert.IsTrue(student.Length > 0);
                Assert.AreEqual(studentName, student[0].Name);
            }
        }

        [Test]
        public async Task PostCourse_ReturnsCreatedCourseWithStudentDetails()
        {
            // Arrange
            int studentId = await CreateTestStudentAndGetId(); // Dynamically create a valid Student

            var newCourse = new Course
            {
                Title = "Test Course",
                Description = "Test Course Description",
                StudentId = studentId
            };

            var json = JsonConvert.SerializeObject(newCourse);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Course", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdCourse = JsonConvert.DeserializeObject<Course>(responseContent);

            Assert.IsNotNull(createdCourse);
            Assert.AreEqual(newCourse.Title, createdCourse.Title);
            Assert.AreEqual(newCourse.Description, createdCourse.Description);
            Assert.AreEqual(studentId, createdCourse.StudentId);
            Assert.IsNotNull(createdCourse.Student);
            Assert.AreEqual(studentId, createdCourse.Student.StudentId);
        }

        [Test]
        public async Task DeleteCourse_ReturnsNoContent()
        {
            // Arrange
            int studentId = await CreateTestStudentAndGetId();
            var newCourse = new Course
            {
                Title = "Course to be deleted",
                Description = "Course Description",
                StudentId = studentId
            };

            var json = JsonConvert.SerializeObject(newCourse);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Course", content);
            var createdCourse = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());

            // Act
            var deleteResponse = await _httpClient.DeleteAsync($"api/Course/{createdCourse.CourseId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Test]
        public async Task DeleteCourse_InvalidId_ReturnsNotFound()
        {
            // Arrange
            int invalidCourseId = 9999; // Use an ID that doesn't exist in the database

            // Act
            var response = await _httpClient.DeleteAsync($"api/Course/{invalidCourseId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode, "Expected 404 Not Found for an invalid Course ID.");
        }

        [Test]
        public async Task GetCourses_ReturnsListOfCoursesWithStudents()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Course");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var courses = JsonConvert.DeserializeObject<Course[]>(responseContent);

            Assert.IsNotNull(courses);
            Assert.IsTrue(courses.Length > 0);
            Assert.IsNotNull(courses[0].Student); // Ensure each course has a student loaded
        }

        [Test]
        public async Task GetCourseById_ReturnsCourseWithStudentDetails()
        {
            // Arrange
            int studentId = await CreateTestStudentAndGetId();
            var newCourse = new Course
            {
                Title = "Course for ID Test",
                Description = "Test Course Description",
                StudentId = studentId
            };

            var json = JsonConvert.SerializeObject(newCourse);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Course", content);
            var createdCourse = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());

            // Act
            var getResponse = await _httpClient.GetAsync($"api/Course/{createdCourse.CourseId}");

            // Assert
            getResponse.EnsureSuccessStatusCode();
            var course = JsonConvert.DeserializeObject<Course>(await getResponse.Content.ReadAsStringAsync());
            Assert.IsNotNull(course);
            Assert.AreEqual(newCourse.Title, course.Title);
            Assert.IsNotNull(course.Student);
            Assert.AreEqual(studentId, course.Student.StudentId);
        }

        [Test]
        public async Task GetCourseById_InvalidId_ReturnsNotFound()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Course/999");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void StudentModel_HasAllProperties()
        {
            // Arrange
            var student = new Student
            {
                StudentId = 1,
                Name = "John Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "123-456-7890",
                Department = "Computer Science",
                Courses = new List<Course>() // Ensure the Courses collection is properly initialized
            };

            // Act & Assert
            Assert.AreEqual(1, student.StudentId, "StudentId does not match.");
            Assert.AreEqual("John Doe", student.Name, "Name does not match.");
            Assert.AreEqual("johndoe@example.com", student.Email, "Email does not match.");
            Assert.AreEqual("123-456-7890", student.PhoneNumber, "PhoneNumber does not match.");
            Assert.AreEqual("Computer Science", student.Department, "Department does not match.");
            Assert.IsNotNull(student.Courses, "Courses collection should not be null.");
            Assert.IsInstanceOf<ICollection<Course>>(student.Courses, "Courses should be of type ICollection<Course>.");
        }

        [Test]
        public void CourseModel_HasAllProperties()
        {
            // Arrange
            var student = new Student
            {
                StudentId = 1,
                Name = "John Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "123-456-7890",
                Department = "Computer Science"
            };

            var course = new Course
            {
                CourseId = 100,
                Title = "Introduction to Programming",
                Description = "Basic programming concepts.",
                StudentId = 1,
                Student = student
            };

            // Act & Assert
            Assert.AreEqual(100, course.CourseId, "CourseId does not match.");
            Assert.AreEqual("Introduction to Programming", course.Title, "Title does not match.");
            Assert.AreEqual("Basic programming concepts.", course.Description, "Description does not match.");
            Assert.AreEqual(1, course.StudentId, "StudentId does not match.");
            Assert.IsNotNull(course.Student, "Student should not be null.");
            Assert.AreEqual(student.Name, course.Student.Name, "Student's Name does not match.");
        }


        [Test]
        public void DbContext_HasDbSetProperties()
        {
            // Assert that the context has DbSet properties for Students and Courses
            Assert.IsNotNull(_context.Students, "Students DbSet is not initialized.");
            Assert.IsNotNull(_context.Courses, "Courses DbSet is not initialized.");
        }

        [Test]
        public void StudentCourse_Relationship_IsConfiguredCorrectly()
        {
            // Check if the Student to Course relationship is configured as one-to-many
            var model = _context.Model;
            var studentEntity = model.FindEntityType(typeof(Student));
            var courseEntity = model.FindEntityType(typeof(Course));

            // Assert that the foreign key relationship exists between Course and Student
            var foreignKey = courseEntity.GetForeignKeys().FirstOrDefault(fk => fk.PrincipalEntityType == studentEntity);

            Assert.IsNotNull(foreignKey, "Foreign key relationship between Course and Student is not configured.");
            Assert.AreEqual("StudentId", foreignKey.Properties.First().Name, "Foreign key property name is incorrect.");

            // Check if the cascade delete behavior is set
            Assert.AreEqual(DeleteBehavior.Cascade, foreignKey.DeleteBehavior, "Cascade delete behavior is not set correctly.");
        }


        [Test]
        public async Task CreateStudent_ThrowsStudentException_ForShortName()
        {
            // Arrange
            var newStudent = new Student
            {
                Name = "Jo",  // Short name
                Email = "shortname@example.com",
                PhoneNumber = "987-654-3210",
                Department = "Test Department"
            };

            var json = JsonConvert.SerializeObject(newStudent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Student", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains("Student name should be at least 3 characters long."), "Expected error message not found in the response.");
        }

        [Test]
        public async Task CreateStudent_ThrowsStudentException_ForInvalidShortName()
        {
            // Arrange
            var newStudent = new Student
            {
                Name = "Jo",  // Short name
                Email = "shortname@example.com",
                PhoneNumber = "987-654-3210",
                Department = "Test Department"
            };

            var json = JsonConvert.SerializeObject(newStudent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Student", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains("Student name should be at least 3 characters long."), "Expected error message not found in the response.");
        }

        [TearDown]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
    }
}
