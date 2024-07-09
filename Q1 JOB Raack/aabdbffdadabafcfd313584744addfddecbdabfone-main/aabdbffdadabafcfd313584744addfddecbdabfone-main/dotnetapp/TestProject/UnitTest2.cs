using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Controllers;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class ApplicationControllerTests
    {
        private ApplicationController _applicationController;
        private JobApplicationDbContext _context;

        [SetUp]
        public void Setup()
        {
            // Initialize an in-memory database for testing
            var options = new DbContextOptionsBuilder<JobApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new JobApplicationDbContext(options);
            _context.Database.EnsureCreated(); // Create the database

            // Seed the database with sample data
            _context.Jobs.AddRange(new List<Job>
            {
                new Job { JobID = 1, JobTitle = "HR Job", Department = "HR", Location = "Chennai", Responsibility = "Job responsibility1", Qualification = "BE", DeadLine = "2023-08-30" },
                new Job { JobID = 2, JobTitle = "IT Admin Job", Department = "Admin", Location = "Pune", Responsibility = "Job responsibility2", Qualification = "MBA", DeadLine = "2023-08-30" },
                new Job { JobID = 3, JobTitle = "IT Job", Department = "IT", Location = "Mumbai", Responsibility = "Job responsibility3", Qualification = "MSc", DeadLine = "2023-08-30" }
            });
            _context.SaveChanges();

            _context.Applications.AddRange(new List<Application>
            {
                new Application { ApplicationID = 1, ApplicationName = "Applicant 1", ContactNumber = "9876543210", MailID = "mymail1@gmail.com", JobID = 1 },
                new Application { ApplicationID = 2, ApplicationName = "Applicant 2", ContactNumber = "9876543217", MailID = "mymail2@gmail.com", JobID = 2 },
                new Application { ApplicationID = 3, ApplicationName = "Applicant 3", ContactNumber = "9876543216", MailID = "mymail3@gmail.com", JobID = 1 }
            });
            _context.SaveChanges();

            _applicationController = new ApplicationController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Delete the in-memory database after each test
            _context.Dispose();
        }

        [Test]
        public void ApplicationClassExists()
        {
            // Arrange
            Type applicationType = typeof(Application);

            // Act & Assert
            Assert.IsNotNull(applicationType, "Application class not found.");
        }

        [Test]
        public void Application_Properties_ApplicationName_ReturnExpectedDataTypes()
        {
            // Arrange
            Application application = new Application();
            PropertyInfo propertyInfo = application.GetType().GetProperty("ApplicationName");

            // Act & Assert
            Assert.IsNotNull(propertyInfo, "ApplicationName property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "ApplicationName property type is not string.");
        }

        [Test]
        public void Application_Properties_JobId_ReturnExpectedDataTypes()
        {
            // Arrange
            Application application = new Application();
            PropertyInfo propertyInfo = application.GetType().GetProperty("JobID");

            // Act & Assert
            Assert.IsNotNull(propertyInfo, "JobID property not found.");
            Assert.AreEqual(typeof(int), propertyInfo.PropertyType, "JobID property type is not int.");
        }

        [Test]
        public async Task GetAllApplications_ReturnsOkResult()
        {
            // Act
            var result = await _applicationController.GetAllApplications();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetAllApplications_ReturnsAllApplications()
        {
            // Act
            var result = await _applicationController.GetAllApplications();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;

            Assert.IsInstanceOf<IEnumerable<Application>>(okResult.Value);
            var applications = okResult.Value as IEnumerable<Application>;

            var applicationCount = applications.Count();
            Assert.AreEqual(3, applicationCount); // Assuming you have 3 Applications in the seeded data
        }

        [Test]
        public async Task AddApplication_ValidData_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newApplication = new Application
            {
                ApplicationName = "Applicant New",
                ContactNumber = "9877743210",
                MailID = "newmymail1@gmail.com",
                JobID = 1
            };

            // Act
            var result = await _applicationController.AddApplication(newApplication);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual("GetApplicationById", createdAtActionResult.ActionName);
            Assert.IsInstanceOf<Application>(createdAtActionResult.Value);
            var returnedApplication = createdAtActionResult.Value as Application;
            Assert.AreEqual(newApplication.ApplicationName, returnedApplication.ApplicationName);
            Assert.AreEqual(newApplication.ContactNumber, returnedApplication.ContactNumber);
            Assert.AreEqual(newApplication.MailID, returnedApplication.MailID);
            Assert.AreEqual(newApplication.JobID, returnedApplication.JobID);
        }

        [Test]
        public async Task DeleteApplication_ValidId_ReturnsNoContent()
        {
            // Act
            var result = await _applicationController.DeleteApplication(1) as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public async Task DeleteApplication_InvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await _applicationController.DeleteApplication(0) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Not a valid Application ID", result.Value);
        }
    }
}
