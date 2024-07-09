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
    public class JobControllerTests
    {
        private JobController _jobController;
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
                new Job { JobID = 1, JobTitle = "Job 1", Department = "HR", Location = "Chennai", Responsibility = "Job responsibility1", Qualification = "BE", DeadLine = "2023-08-30" },
                new Job { JobID = 2, JobTitle = "Job 2", Department = "Admin", Location = "Pune", Responsibility = "Job responsibility2", Qualification = "MBA", DeadLine = "2023-08-30" },
                new Job { JobID = 3, JobTitle = "Job 3", Department = "IT", Location = "Mumbai", Responsibility = "Job responsibility3", Qualification = "MSc", DeadLine = "2023-08-30" }
            });
            _context.SaveChanges();

            _jobController = new JobController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Delete the in-memory database after each test
            _context.Dispose();
        }

        [Test]
        public void JobClassExists()
        {
            // Arrange
            Type jobType = typeof(Job);

            // Act & Assert
            Assert.IsNotNull(jobType, "Job class not found.");
        }

        [Test]
        public void Job_Properties_JobTitle_ReturnExpectedDataTypes()
        {
            // Arrange
            Job job = new Job();
            PropertyInfo propertyInfo = job.GetType().GetProperty("JobTitle");

            // Act & Assert
            Assert.IsNotNull(propertyInfo, "JobTitle property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "JobTitle property type is not string.");
        }

        [Test]
        public void Job_Properties_Department_ReturnExpectedDataTypes()
        {
            // Arrange
            Job job = new Job();
            PropertyInfo propertyInfo = job.GetType().GetProperty("Department");

            // Act & Assert
            Assert.IsNotNull(propertyInfo, "Department property not found.");
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType, "Department property type is not string.");
        }

        [Test]
        public async Task GetAllJobs_ReturnsOkResult()
        {
            // Act
            var result = await _jobController.GetAllJobs();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetAllJobs_ReturnsAllJobs()
        {
            // Act
            var result = await _jobController.GetAllJobs();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;

            Assert.IsInstanceOf<IEnumerable<Job>>(okResult.Value);
            var jobs = okResult.Value as IEnumerable<Job>;

            var jobCount = jobs.Count();
            Assert.AreEqual(3, jobCount); // Assuming you have 3 jobs in the seeded data
        }

        [Test]
        public async Task AddJob_ValidData_ReturnsCreatedResult()
        {
            // Arrange
            var newJob = new Job
            {
                JobTitle = "New Job Title",
                Department = "HR",
                Location = "Chennai",
                Responsibility = "Job responsibility1",
                Qualification = "BE",
                DeadLine = "2023-08-10"
            };

            // Act
            var result = await _jobController.AddJob(newJob);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdResult = result as CreatedAtActionResult;
            Assert.AreEqual(nameof(JobController.GetJobById), createdResult.ActionName);
        }

        [Test]
        public async Task DeleteJob_ValidId_ReturnsNoContent()
        {
            // Act
            var result = await _jobController.DeleteJob(1) as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public async Task DeleteJob_InvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await _jobController.DeleteJob(0) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Not a valid Job ID", result.Value);
        }
    }
}
