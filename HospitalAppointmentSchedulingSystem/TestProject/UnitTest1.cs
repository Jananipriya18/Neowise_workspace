using NUnit.Framework;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace dotnetapp.Tests
{
    [TestFixture]
    public class DoctorAppointmentControllerTests
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

        private async Task<int> CreateTestDoctorAndGetId()
        {
            var newDoctor = new Doctor
            {
                Name = "Test Doctor",
                Specialty = "General",
                ContactInfo = "123-456-7890",
                DoctorFee = 100
            };

            var json = JsonConvert.SerializeObject(newDoctor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Doctor", content);
            response.EnsureSuccessStatusCode();

            var createdDoctor = JsonConvert.DeserializeObject<Doctor>(await response.Content.ReadAsStringAsync());
            return createdDoctor.DoctorId;
        }

        [Test]
        public async Task CreateDoctor_ReturnsCreatedDoctor()
        {
            // Arrange
            var newDoctor = new Doctor
            {
                Name = "Test Doctor",
                Specialty = "General",
                ContactInfo = "123-456-7890",
                DoctorFee = 150
            };

            var json = JsonConvert.SerializeObject(newDoctor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Doctor", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdDoctor = JsonConvert.DeserializeObject<Doctor>(responseContent);

            Assert.IsNotNull(createdDoctor);
            Assert.AreEqual(newDoctor.Name, createdDoctor.Name);
            Assert.AreEqual(newDoctor.Specialty, createdDoctor.Specialty);
            Assert.AreEqual(newDoctor.ContactInfo, createdDoctor.ContactInfo);
            Assert.AreEqual(newDoctor.DoctorFee, createdDoctor.DoctorFee);
        }

        [Test]
        public async Task CreateAppointment_ReturnsCreatedAppointmentWithDoctorDetails()
        {
            // Arrange
            int doctorId = await CreateTestDoctorAndGetId(); // Dynamically create a valid Doctor

            var newAppointment = new Appointment
            {
                AppointmentDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"),
                PatientName = "Test Patient",
                Reason = "Checkup",
                DoctorId = doctorId
            };

            var json = JsonConvert.SerializeObject(newAppointment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Appointment", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdAppointment = JsonConvert.DeserializeObject<Appointment>(responseContent);

            Assert.IsNotNull(createdAppointment);
            Assert.AreEqual(newAppointment.AppointmentDate, createdAppointment.AppointmentDate);
            Assert.AreEqual(newAppointment.PatientName, createdAppointment.PatientName);
            Assert.AreEqual(newAppointment.Reason, createdAppointment.Reason);
            Assert.AreEqual(doctorId, createdAppointment.DoctorId);
            Assert.IsNotNull(createdAppointment.Doctor);
            Assert.AreEqual(doctorId, createdAppointment.Doctor.DoctorId);
        }

        [Test]
        public async Task GetDoctorsSortedByFee_ReturnsSortedDoctors()
        {
            // Arrange
            await CreateTestDoctorAndGetId(); // Create doctors to be sorted

            // Act
            var response = await _httpClient.GetAsync("api/Doctor/sortedByFee");

            // Assert
            response.EnsureSuccessStatusCode();
            var doctors = JsonConvert.DeserializeObject<Doctor[]>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(doctors);
            Assert.AreEqual(doctors.OrderByDescending(d => d.DoctorFee).ToList(), doctors);
        }

        [Test]
        public async Task GetDoctorById_ReturnsDoctor()
        {
            // Arrange
            int doctorId = await CreateTestDoctorAndGetId(); // Create a doctor

            // Act
            var response = await _httpClient.GetAsync($"api/Doctor/{doctorId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var doctor = JsonConvert.DeserializeObject<Doctor>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(doctor);
            Assert.AreEqual(doctorId, doctor.DoctorId);
        }

        [Test]
        public async Task UpdateDoctor_ReturnsNoContent()
        {
            // Arrange
            int doctorId = await CreateTestDoctorAndGetId();

            var updatedDoctor = new Doctor
            {
                DoctorId = doctorId,
                Name = "Updated Doctor",
                Specialty = "Updated Specialty",
                ContactInfo = "updated-contact@example.com",
                DoctorFee = 200
            };

            var json = JsonConvert.SerializeObject(updatedDoctor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync($"api/Doctor/{doctorId}", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Test]
        public async Task UpdateAppointment_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var updatedAppointment = new Appointment
            {
                AppointmentId = 9999, // Non-existent ID
                AppointmentDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                PatientName = "Jane Doe",
                Reason = "Follow-up"
            };

            var json = JsonConvert.SerializeObject(updatedAppointment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync($"api/Appointment/{updatedAppointment.AppointmentId}", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task GetAppointments_ReturnsListOfAppointmentsWithDoctors()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Appointment");

            // Assert
            response.EnsureSuccessStatusCode();
            var appointments = JsonConvert.DeserializeObject<Appointment[]>(await response.Content.ReadAsStringAsync());

            // Ensure the deserialized appointments array is not null
            Assert.IsNotNull(appointments);
            
            // Ensure that the array contains one or more appointments
            Assert.IsTrue(appointments.Length > 0);
            
            // Ensure each appointment has an associated doctor
            foreach (var appointment in appointments)
            {
                Assert.IsNotNull(appointment.Doctor, "Doctor should not be null for each appointment.");
            }
        }


        [Test]
        public async Task GetDoctorById_InvalidId_ReturnsNotFound()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Doctor/999");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void DoctorModel_HasAllProperties()
        {
            // Arrange
            var doctorInstance = new Doctor
            {
                DoctorId = 1,
                Name = "Sample Doctor",
                Specialty = "Pediatrics",
                ContactInfo = "987-654-3210",
                DoctorFee = 150,
                Appointments = new List<Appointment>() // Ensure the Appointments collection is properly initialized
            };

            // Act & Assert
            Assert.AreEqual(1, doctorInstance.DoctorId, "DoctorId does not match.");
            Assert.AreEqual("Sample Doctor", doctorInstance.Name, "Name does not match.");
            Assert.AreEqual("Pediatrics", doctorInstance.Specialty, "Specialty does not match.");
            Assert.AreEqual("987-654-3210", doctorInstance.ContactInfo, "ContactInfo does not match.");
            Assert.AreEqual(150, doctorInstance.DoctorFee, "DoctorFee does not match.");
            Assert.IsNotNull(doctorInstance.Appointments, "Appointments collection should not be null.");
            Assert.IsInstanceOf<ICollection<Appointment>>(doctorInstance.Appointments, "Appointments should be of type ICollection<Appointment>.");
        }

        [Test]
        public void AppointmentModel_HasAllProperties()
        {
            // Arrange
            var doctorInstance = new Doctor
            {
                DoctorId = 1,
                Name = "Sample Doctor",
                Specialty = "Pediatrics",
                ContactInfo = "987-654-3210",
                DoctorFee = 150
            };

            var appointment = new Appointment
            {
                AppointmentId = 100,
                AppointmentDate = "2024-09-15",
                PatientName = "John Doe",
                Reason = "Routine Check-up",
                DoctorId = 1,
                Doctor = doctorInstance
            };

            // Act & Assert
            Assert.AreEqual(100, appointment.AppointmentId, "AppointmentId does not match.");
            Assert.AreEqual("2024-09-15", appointment.AppointmentDate, "AppointmentDate does not match.");
            Assert.AreEqual("John Doe", appointment.PatientName, "PatientName does not match.");
            Assert.AreEqual("Routine Check-up", appointment.Reason, "Reason does not match.");
            Assert.AreEqual(1, appointment.DoctorId, "DoctorId does not match.");
            Assert.IsNotNull(appointment.Doctor, "Doctor should not be null.");
            Assert.AreEqual(doctorInstance.Name, appointment.Doctor.Name, "Doctor's Name does not match.");
        }

        [Test]
        public void DbContext_HasDbSetProperties()
        {
            // Assert that the context has DbSet properties for Doctors and Appointments
            Assert.IsNotNull(_context.Doctors, "Doctors DbSet is not initialized.");
            Assert.IsNotNull(_context.Appointments, "Appointments DbSet is not initialized.");
        }

        [Test]
        public void DoctorAppointment_Relationship_IsConfiguredCorrectly()
        {
            // Check if the Doctor to Appointment relationship is configured as one-to-many
            var model = _context.Model;
            var doctorEntity = model.FindEntityType(typeof(Doctor));
            var appointmentEntity = model.FindEntityType(typeof(Appointment));

            // Assert that the foreign key relationship exists between Appointment and Doctor
            var foreignKey = appointmentEntity.GetForeignKeys().FirstOrDefault(fk => fk.PrincipalEntityType == doctorEntity);

            Assert.IsNotNull(foreignKey, "Foreign key relationship between Appointment and Doctor is not configured.");
            Assert.AreEqual("DoctorId", foreignKey.Properties.First().Name, "Foreign key property name is incorrect.");

            // Check if the cascade delete behavior is set
            Assert.AreEqual(DeleteBehavior.Cascade, foreignKey.DeleteBehavior, "Cascade delete behavior is not set correctly.");
        }

        [Test]
        public async Task CreateDoctor_ThrowsPriceException_ForInvalidDoctorFee()
        {
            // Arrange
            var newDoctor = new Doctor
            {
                Name = "Test Doctor",
                Specialty = "General",
                ContactInfo = "123-456-7890",
                DoctorFee = 0 // Invalid fee
            };

            var json = JsonConvert.SerializeObject(newDoctor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Doctor", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains("DoctorFee must be greater than 0"), "Expected error message not found in the response.");
        }

        [Test]
        public async Task CreateDoctor_ThrowsPriceException_ForNegativeDoctorFee()
        {
            // Arrange
            var newDoctor = new Doctor
            {
                Name = "Test Doctor",
                Specialty = "General",
                ContactInfo = "123-456-7890",
                DoctorFee = -10 // Invalid fee
            };

            var json = JsonConvert.SerializeObject(newDoctor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("api/Doctor", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode); // 500 for thrown exception

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Contains("DoctorFee must be greater than 0"), "Expected error message not found in the response.");
        }

        [TearDown]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
    }
}
