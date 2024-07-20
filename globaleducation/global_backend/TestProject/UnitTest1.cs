using dotnetapp.Exceptions;
using dotnetapp.Models;
using dotnetapp.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Reflection;
using dotnetapp.Services;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class Tests
    {

        private ApplicationDbContext _context; 
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            _context = new ApplicationDbContext(options);
           
             _httpClient = new HttpClient();
             _httpClient.BaseAddress = new Uri("http://localhost:8080");

        }

        [TearDown]
        public void TearDown()
        {
             _context.Dispose();
        }

   [Test, Order(1)]
    public async Task Backend_Test_Delete_Method_Feedback_In_Feeback_Service_Deletes_Successfully()
    {
        ClearDatabase();
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
  
   [Test, Order(2)]
    public async Task Backend_Test_Post_Method_Login_Admin_Returns_HttpStatusCode_OK()
    {
        ClearDatabase();

        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        // Print registration response
        string registerResponseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Registration Response: " + registerResponseBody);

        // Login with the registered user
        string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

        // Print login response
        string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        Console.WriteLine("Login Response: " + loginResponseBody);

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }

         
   [Test, Order(3)]
public async Task Backend_Test_Get_All_Colleges_With_Token_By_Manager_Returns_HttpStatusCode_OK()
{
    ClearDatabase();
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"User\"}}";
    HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    // Print registration response
    string registerResponseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Registration Response: " + registerResponseBody);

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    // Print login response
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Login Response: " + loginResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    string token = responseMap.token;

    Assert.IsNotNull(token);

    // Use the token to get all feeds
    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/colleges");

    // Print feed response
    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
    Console.WriteLine("apiResponseBody: " + apiResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode);
}

[Test, Order(4)]
public async Task Backend_Test_Get_All_Colleges_Without_Token_By_Manager_Returns_HttpStatusCode_Unauthorized()
{
    ClearDatabase();
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"User\"}}";
    HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    // Print registration response
    string registerResponseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Registration Response: " + registerResponseBody);

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    // Print login response
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Login Response: " + loginResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/colleges");

    // Print feed response
    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
    Console.WriteLine("apiResponseBody: " + apiResponseBody);

    Assert.AreEqual(HttpStatusCode.Unauthorized, apiResponse.StatusCode);
}

  [Test, Order(5)]
        public async Task Backend_Test_Get_Method_GetAllColleges_In_CollegeService_Returns_All_Colleges()
        {
            ClearDatabase();

            var collegesData = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "CollegeId", 1 },
                    { "CollegeName", "College 1" },
                    { "Location", "Location 1" },
                    { "Description", "Description 1" },
                    { "Website", "http://college1.com" }
                },
                new Dictionary<string, object>
                {
                    { "CollegeId", 2 },
                    { "CollegeName", "College 2" },
                    { "Location", "Location 2" },
                    { "Description", "Description 2" },
                    { "Website", "http://college2.com" }
                }
            };

            foreach (var collegeData in collegesData)
            {
                var college = new College();
                foreach (var kvp in collegeData)
                {
                    var propertyInfo = typeof(College).GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(college, kvp.Value);
                    }
                }
                _context.Colleges.Add(college);
            }
            _context.SaveChanges();

            // Load assembly and types
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.CollegeService";

            Type serviceType = assembly.GetType(serviceName);

            // Get GetAllColleges method
            MethodInfo getAllCollegesMethod = serviceType.GetMethod("GetAllColleges");

            if (getAllCollegesMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);

                // Invoke GetAllColleges method
                var getAllCollegesResult = (Task<IEnumerable<College>>)getAllCollegesMethod.Invoke(service, null);
                var colleges = await getAllCollegesResult;

                // Assert the results
                Assert.IsNotNull(colleges);
                Assert.AreEqual(2, colleges.Count());
                Assert.AreEqual("College 1", colleges.First().CollegeName);
                Assert.AreEqual("College 2", colleges.Last().CollegeName);
            }
            else
            {
                Assert.Fail("GetAllColleges method not found.");
            }
        }

        [Test, Order(6)]
        public async Task Backend_Test_Get_Method_GetCollegeById_In_CollegeService_Returns_Correct_College()
        {
            ClearDatabase();

            var collegeData = new Dictionary<string, object>
            {
                { "CollegeId", 1 },
                { "CollegeName", "College 1" },
                { "Location", "Location 1" },
                { "Description", "Description 1" },
                { "Website", "http://college1.com" }
            };

            var college = new College();
            foreach (var kvp in collegeData)
            {
                var propertyInfo = typeof(College).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(college, kvp.Value);
                }
            }
            _context.Colleges.Add(college);
            _context.SaveChanges();

            // Load assembly and types
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.CollegeService";

            Type serviceType = assembly.GetType(serviceName);

            // Get GetCollegeById method
            MethodInfo getCollegeByIdMethod = serviceType.GetMethod("GetCollegeById");

            if (getCollegeByIdMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);

                // Invoke GetCollegeById method
                var getCollegeByIdResult = (Task<College>)getCollegeByIdMethod.Invoke(service, new object[] { 1 });
                var collegeResult = await getCollegeByIdResult;

                // Assert the results
                Assert.IsNotNull(collegeResult);
                Assert.AreEqual("College 1", collegeResult.CollegeName);
            }
            else
            {
                Assert.Fail("GetCollegeById method not found.");
            }
        }

        [Test, Order(7)]
        public async Task Backend_Test_Post_Method_AddCollege_In_CollegeService_Adds_College_Successfully()
        {
            ClearDatabase();

            var collegeData = new Dictionary<string, object>
            {
                { "CollegeId", 1 },
                { "CollegeName", "Unique College" },
                { "Location", "Unique Location" },
                { "Description", "Unique Description" },
                { "Website", "http://uniquecollege.com" }
            };

            var college = new College();
            foreach (var kvp in collegeData)
            {
                var propertyInfo = typeof(College).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(college, kvp.Value);
                }
            }

            // Load assembly and types
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.CollegeService";
            string modelName = "dotnetapp.Models.College";

            Type serviceType = assembly.GetType(serviceName);
            Type modelType = assembly.GetType(modelName);

            // Get AddCollege method
            MethodInfo addCollegeMethod = serviceType.GetMethod("AddCollege", new[] { modelType });

            if (addCollegeMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);

                // Invoke AddCollege method
                var addCollegeResult = (Task)addCollegeMethod.Invoke(service, new object[] { college });
                await addCollegeResult;

                // Retrieve added college from database
                var addedCollege = await _context.Colleges.FindAsync(1);

                // Assert the added college properties
                Assert.IsNotNull(addedCollege);
                Assert.AreEqual("Unique College", addedCollege.CollegeName);
            }
            else
            {
                Assert.Fail("AddCollege method not found.");
            }
        }
        [Test, Order(8)]
        public async Task Backend_Test_Get_Method_GetAllApplications_In_ApplicationService_Returns_All_Applications()
        {
            ClearDatabase();

            // Add users to context
            var usersData = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "UserId", 1 },
                    { "Username", "user1" },
                    { "Password", "password1" },
                    { "Email", "user1@example.com" },
                    { "MobileNumber", "1234567890" },
                    { "UserRole", "Student" }
                },
                new Dictionary<string, object>
                {
                    { "UserId", 2 },
                    { "Username", "user2" },
                    { "Password", "password2" },
                    { "Email", "user2@example.com" },
                    { "MobileNumber", "0987654321" },
                    { "UserRole", "Student" }
                }
            };

            foreach (var userData in usersData)
            {
                var user = new User();
                foreach (var kvp in userData)
                {
                    var propertyInfo = typeof(User).GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(user, kvp.Value);
                    }
                }
                _context.Users.Add(user);
            }
            _context.SaveChanges();

            // Add colleges to context
            var collegesData = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "CollegeId", 1 },
                    { "CollegeName", "College 1" },
                    { "Location", "Location 1" },
                    { "Description", "Description 1" },
                    { "Website", "http://college1.com" }
                },
                new Dictionary<string, object>
                {
                    { "CollegeId", 2 },
                    { "CollegeName", "College 2" },
                    { "Location", "Location 2" },
                    { "Description", "Description 2" },
                    { "Website", "http://college2.com" }
                }
            };

            foreach (var collegeData in collegesData)
            {
                var college = new College();
                foreach (var kvp in collegeData)
                {
                    var propertyInfo = typeof(College).GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(college, kvp.Value);
                    }
                }
                _context.Colleges.Add(college);
            }
            _context.SaveChanges();

            // Add applications to context
            var applicationsData = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "ApplicationId", 1 },
                    { "UserId", 1 },
                    { "CollegeId", 1 },
                    { "DegreeName", "Degree 1" },
                    { "TwelfthPercentage", 85.5 },
                    { "PreviousCollege", "College A" },
                    { "PreviousCollegeCGPA", 8.5 },
                    { "Status", "Pending" },
                    { "CreatedAt", DateTime.Now }
                },
                new Dictionary<string, object>
                {
                    { "ApplicationId", 2 },
                    { "UserId", 2 },
                    { "CollegeId", 2 },
                    { "DegreeName", "Degree 2" },
                    { "TwelfthPercentage", 88.0 },
                    { "PreviousCollege", "College B" },
                    { "PreviousCollegeCGPA", 9.0 },
                    { "Status", "Approved" },
                    { "CreatedAt", DateTime.Now }
                }
            };

            foreach (var applicationData in applicationsData)
            {
                var application = new Application();
                foreach (var kvp in applicationData)
                {
                    var propertyInfo = typeof(Application).GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(application, kvp.Value);
                    }
                }
                _context.Applications.Add(application);
            }
            _context.SaveChanges();

            // Load assembly and types
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.ApplicationService";

            Type serviceType = assembly.GetType(serviceName);

            // Get GetAllApplications method
            MethodInfo getAllApplicationsMethod = serviceType.GetMethod("GetAllApplications");

            if (getAllApplicationsMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);

                // Invoke GetAllApplications method
                var getAllApplicationsResult = (Task<IEnumerable<Application>>)getAllApplicationsMethod.Invoke(service, null);
                var applications = await getAllApplicationsResult;

                // Assert the results
                Assert.IsNotNull(applications);
    
            }
            else
            {
                Assert.Fail("GetAllApplications method not found.");
            }
        }
  [Test, Order(9)]
        public async Task Backend_Test_Get_Method_GetApplicationById_In_ApplicationService_Returns_Correct_Application()
        {
            ClearDatabase();

            // Add user to context
            var userData = new Dictionary<string, object>
            {
                { "UserId", 1 },
                { "Username", "user1" },
                { "Password", "password1" },
                { "Email", "user1@example.com" },
                { "MobileNumber", "1234567890" },
                { "UserRole", "Student" }
            };

            var user = new User();
            foreach (var kvp in userData)
            {
                var propertyInfo = typeof(User).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(user, kvp.Value);
                }
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            // Add college to context
            var collegeData = new Dictionary<string, object>
            {
                { "CollegeId", 1 },
                { "CollegeName", "College 1" },
                { "Location", "Location 1" },
                { "Description", "Description 1" },
                { "Website", "http://college1.com" }
            };

            var college = new College();
            foreach (var kvp in collegeData)
            {
                var propertyInfo = typeof(College).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(college, kvp.Value);
                }
            }
            _context.Colleges.Add(college);
            _context.SaveChanges();

            // Add application to context
            var applicationData = new Dictionary<string, object>
            {
                { "ApplicationId", 1 },
                { "UserId", 1 },
                { "CollegeId", 1 },
                { "DegreeName", "Degree 1" },
                { "TwelfthPercentage", 85.5 },
                { "PreviousCollege", "College A" },
                { "PreviousCollegeCGPA", 8.5 },
                { "Status", "Pending" },
                { "CreatedAt", DateTime.Now }
            };

            var application = new Application();
            foreach (var kvp in applicationData)
            {
                var propertyInfo = typeof(Application).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(application, kvp.Value);
                }
            }
            _context.Applications.Add(application);
            _context.SaveChanges();

            // Load assembly and types
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.ApplicationService";

            Type serviceType = assembly.GetType(serviceName);

            // Get GetApplicationById method
            MethodInfo getApplicationByIdMethod = serviceType.GetMethod("GetApplicationById");

            if (getApplicationByIdMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);

                // Invoke GetApplicationById method
                var getApplicationByIdResult = (Task<Application>)getApplicationByIdMethod.Invoke(service, new object[] { 1 });
                var applicationResult = await getApplicationByIdResult;

                // Assert the results
                Assert.IsNotNull(applicationResult);
                Assert.AreEqual("Degree 1", applicationResult.DegreeName);
            }
            else
            {
                Assert.Fail("GetApplicationById method not found.");
            }
        }

            [Test, Order(10)]
        public async Task Backend_Test_Post_Method_AddApplication_In_ApplicationService_Throws_CollegeApplicationException_For_Duplicate_Application()
        {
            ClearDatabase();

            // Add user to context
            var userData = new Dictionary<string, object>
            {
                { "UserId", 1 },
                { "Username", "user1" },
                { "Password", "password1" },
                { "Email", "user1@example.com" },
                { "MobileNumber", "1234567890" },
                { "UserRole", "Student" }
            };

            var user = new User();
            foreach (var kvp in userData)
            {
                var propertyInfo = typeof(User).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(user, kvp.Value);
                }
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            // Add college to context
            var collegeData = new Dictionary<string, object>
            {
                { "CollegeId", 1 },
                { "CollegeName", "College 1" },
                { "Location", "Location 1" },
                { "Description", "Description 1" },
                { "Website", "http://college1.com" }
            };

            var college = new College();
            foreach (var kvp in collegeData)
            {
                var propertyInfo = typeof(College).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(college, kvp.Value);
                }
            }
            _context.Colleges.Add(college);
            _context.SaveChanges();

            // Add initial application to context
            var applicationData = new Dictionary<string, object>
            {
                { "ApplicationId", 1 },
                { "UserId", 1 },
                { "CollegeId", 1 },
                { "DegreeName", "Degree 1" },
                { "TwelfthPercentage", 85.5 },
                { "PreviousCollege", "College A" },
                { "PreviousCollegeCGPA", 8.5 },
                { "Status", "Pending" },
                { "CreatedAt", DateTime.Now }
            };

            var application = new Application();
            foreach (var kvp in applicationData)
            {
                var propertyInfo = typeof(Application).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(application, kvp.Value);
                }
            }
            _context.Applications.Add(application);
            _context.SaveChanges();

            // Add duplicate application to context
            var duplicateApplicationData = new Dictionary<string, object>
            {
                { "ApplicationId", 2 },
                { "UserId", 1 },
                { "CollegeId", 1 },
                { "DegreeName", "Degree 2" },
                { "TwelfthPercentage", 90.0 },
                { "PreviousCollege", "College B" },
                { "PreviousCollegeCGPA", 9.0 },
                { "Status", "Pending" },
                { "CreatedAt", DateTime.Now }
            };

            var duplicateApplication = new Application();
            foreach (var kvp in duplicateApplicationData)
            {
                var propertyInfo = typeof(Application).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(duplicateApplication, kvp.Value);
                }
            }

            // Load assembly and types
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.ApplicationService";
            string modelName = "dotnetapp.Models.Application";

            Type serviceType = assembly.GetType(serviceName);
            Type modelType = assembly.GetType(modelName);

            // Get AddApplication method
            MethodInfo addApplicationMethod = serviceType.GetMethod("AddApplication", new[] { modelType });

            if (addApplicationMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);

                // Invoke AddApplication method
                try
                {
                    var addApplicationResult = (Task<bool>)addApplicationMethod.Invoke(service, new object[] { duplicateApplication });
                 Console.WriteLine("res" + addApplicationResult.Result);
                    Assert.Fail("Expected CollegeApplicationException was not thrown.");
                }
                catch (Exception ex)
                {
                    Assert.IsNotNull(ex.InnerException);
                    Assert.IsTrue(ex.InnerException is CollegeApplicationException);
                    Assert.AreEqual("The user has already applied to this college.", ex.InnerException.Message);
                }
            }
            else
            {
                Assert.Fail("AddApplication method not found.");
            }
        }
[Test, Order(11)]
public async Task Backend_Test_Post_Method_AddFeedback_In_Feedback_Service_Posts_Successfully()
{
     ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId",42 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();
    // Add loan application
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.FeedbackService";
    string typeName = "dotnetapp.Models.Feedback";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo method = serviceType.GetMethod("AddFeedback", new[] { modelType });

    if (method != null)
    {
           var feedbackData = new Dictionary<string, object>
            {
                { "FeedbackId", 11 },
                { "UserId", 42 },
                { "FeedbackText", "Great experience!" },
                { "Date", DateTime.Now }
            };
        var feedback = new Feedback();
        foreach (var kvp in feedbackData)
        {
            var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(feedback, kvp.Value);
            }
        }
        var service = Activator.CreateInstance(serviceType, _context);
        var result = (Task<bool>)method.Invoke(service, new object[] { feedback });
    
        var addedFeedback= await _context.Feedbacks.FindAsync(11);
        Assert.IsNotNull(addedFeedback);
        Assert.AreEqual("Great experience!",addedFeedback.FeedbackText);

    }
    else{
        Assert.Fail();
    }
}

[Test, Order(12)]
public async Task Backend_Test_Delete_Method_Feedback_In_Feeback_Service_Deletes_Successfully()
{
    // Add user
     ClearDatabase();

    var userData = new Dictionary<string, object>
    {
        { "UserId",42 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

           var feedbackData = new Dictionary<string, object>
            {
                { "FeedbackId", 11 },
                { "UserId", 42 },
                { "FeedbackText", "Great experience!" },
                { "Date", DateTime.Now }
            };
        var feedback = new Feedback();
        foreach (var kvp in feedbackData)
        {
            var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(feedback, kvp.Value);
            }
        }
     _context.Feedbacks.Add(feedback);
    _context.SaveChanges();
    // Add loan application
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.FeedbackService";
    string typeName = "dotnetapp.Models.Feedback";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

  
    MethodInfo deletemethod = serviceType.GetMethod("DeleteFeedback", new[] { typeof(int) });

    if (deletemethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var deleteResult = (Task<bool>)deletemethod.Invoke(service, new object[] { 11 });

        var deletedFeedbackFromDb = await _context.Feedbacks.FindAsync(11);
        Assert.IsNull(deletedFeedbackFromDb);
    }
    else
    {
        Assert.Fail();
    }
}



public async Task Backend_Test_Get_Method_GetFeedbacksByUserId_In_Feedback_Service_Fetches_Successfully()
{
    ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId", 330 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "User" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    var feedbackData= new Dictionary<string, object>
    {
        { "FeedbackId", 13 },
        { "UserId", 330 },
        { "FeedbackText", "Great experience!" },
        { "Date", DateTime.Now }
    };

    var feedback = new Feedback();
    foreach (var kvp in feedbackData)
    {
        var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(feedback, kvp.Value);
        }
    }
    _context.Feedbacks.Add(feedback);
    _context.SaveChanges();

    // Add loan application
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.FeedbackService";
    string typeName = "dotnetapp.Models.Feedback";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo method = serviceType.GetMethod("GetFeedbacksByUserId");

    if (method != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var result = ( Task<IEnumerable<Feedback>>)method.Invoke(service, new object[] {330});
        Assert.IsNotNull(result);
         var check=true;
        foreach (var item in result.Result)
        {
            check=false;
            Assert.AreEqual("Great experience!", item.FeedbackText);
   
        }
        if(check==true)
        {
            Assert.Fail();

        }
    }
    else{
        Assert.Fail();
    }
}

private void ClearDatabase()
{
    _context.Database.EnsureDeleted();
    _context.Database.EnsureCreated();
}

}
}