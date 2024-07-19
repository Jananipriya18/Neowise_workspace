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
 public async Task Backend_Test_Post_Method_Register_Admin_Returns_HttpStatusCode_OK()
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
public async Task Backend_Test_Get_All_Workout_With_Token_By_User_Returns_HttpStatusCode_OK()
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
    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/workout");

    // Print feed response
    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
    Console.WriteLine("apiResponseBody: " + apiResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode);
}

[Test, Order(4)]
public async Task Backend_Test_Get_All_Workout_Without_Token_By_User_Returns_HttpStatusCode_Unauthorized()
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

    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/workout");

    // Print feed response
    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
    Console.WriteLine("apiResponseBody: " + apiResponseBody);

    Assert.AreEqual(HttpStatusCode.Unauthorized, apiResponse.StatusCode);
}


[Test, Order(5)]
public async Task Backend_Test_Get_Method_GetAllWorkouts_In_WorkoutService_Fetches_Successfully()
{
            ClearDatabase();

            // Add workouts
            var workoutsData = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "WorkoutId", 1 },
                    { "WorkoutName", "Workout A" },
                    { "Description", "Description A" },
                    { "DifficultyLevel", 3 },
                    { "CreatedAt", DateTime.UtcNow },
                    { "TargetArea", "Full Body" },
                    { "DaysPerWeek", 3 },
                    { "AverageWorkoutDurationInMinutes", 45 }
                },
                new Dictionary<string, object>
                {
                    { "WorkoutId", 2 },
                    { "WorkoutName", "Workout B" },
                    { "Description", "Description B" },
                    { "DifficultyLevel", 2 },
                    { "CreatedAt", DateTime.UtcNow },
                    { "TargetArea", "Core" },
                    { "DaysPerWeek", 4 },
                    { "AverageWorkoutDurationInMinutes", 30 }
                }
            };

            foreach (var workoutData in workoutsData)
            {
                var workout = new Workout();
                foreach (var kvp in workoutData)
                {
                    var propertyInfo = typeof(Workout).GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(workout, kvp.Value);
                    }
                }
                _context.Workouts.Add(workout);
            }
            _context.SaveChanges();

            // Invoke service method
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.WorkoutService";

            Type serviceType = assembly.GetType(serviceName);
            MethodInfo method = serviceType.GetMethod("GetAllWorkouts");

            if (method != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);
                var result = (Task<IEnumerable<Workout>>)method.Invoke(service, null);
                Assert.IsNotNull(result);
                var workouts = await result;
                Assert.AreEqual(2, workouts.Count());
                Assert.IsTrue(workouts.Any(w => w.WorkoutName == "Workout A"));
                Assert.IsTrue(workouts.Any(w => w.WorkoutName == "Workout B"));
            }
            else
            {
                Assert.Fail();
            }
        }


[Test, Order(6)]
        public async Task Backend_Test_Get_Method_GetWorkoutById_In_WorkoutService_Fetches_Successfully()
        {
            ClearDatabase();

            // Add a workout
            var workoutData = new Dictionary<string, object>
            {
                { "WorkoutId", 3 },
                { "WorkoutName", "Workout C" },
                { "Description", "Description C" },
                { "DifficultyLevel", 5 },
                { "CreatedAt", DateTime.UtcNow },
                { "TargetArea", "Upper Body" },
                { "DaysPerWeek", 5 },
                { "AverageWorkoutDurationInMinutes", 60 }
            };

            var workout = new Workout();
            foreach (var kvp in workoutData)
            {
                var propertyInfo = typeof(Workout).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(workout, kvp.Value);
                }
            }
            _context.Workouts.Add(workout);
            _context.SaveChanges();

            // Invoke service method
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.WorkoutService";

            Type serviceType = assembly.GetType(serviceName);
            MethodInfo method = serviceType.GetMethod("GetWorkoutById");

            if (method != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);
                var result = (Task<Workout>)method.Invoke(service, new object[] { 3 });
                Assert.IsNotNull(result);
                var fetchedWorkout = await result;

                Assert.IsNotNull(fetchedWorkout);
                Assert.AreEqual("Workout C", fetchedWorkout.WorkoutName);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test, Order(7)]
        public async Task Backend_Test_Post_Method_Add_Workout_In_WorkoutService_Adds_Workout_Successfully()
        {
            ClearDatabase();

            // Set up workout data
            var workoutData = new Dictionary<string, object>
            {
                { "WorkoutId", 1 },
                { "WorkoutName", "Healthy Workout" },
                { "Description", "A healthy workout" },
                { "DifficultyLevel", 4 },
                { "CreatedAt", DateTime.UtcNow },
                { "TargetArea", "Full Body" },
                { "DaysPerWeek", 3 },
                { "AverageWorkoutDurationInMinutes", 45 }
            };

            var workout = new Workout();
            foreach (var kvp in workoutData)
            {
                var propertyInfo = typeof(Workout).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(workout, kvp.Value);
                }
            }

            // Load assembly and types
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.WorkoutService";

            Type serviceType = assembly.GetType(serviceName);

            // Get the AddWorkout method
            MethodInfo addWorkoutMethod = serviceType.GetMethod("AddWorkout");

            // Check if method exists
            if (addWorkoutMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);
                var addWorkoutTask = (Task<bool>)addWorkoutMethod.Invoke(service, new object[] { workout });
                var result = await addWorkoutTask;

                // Assert that the workout was added successfully
                Assert.IsTrue(result);

                // Verify that the workout was added
                var retrievedWorkout = await _context.Workouts.FindAsync(1);

                // Assert the retrieved workout is not null and properties match
                Assert.IsNotNull(retrievedWorkout);
                Assert.AreEqual(workout.WorkoutName, retrievedWorkout.WorkoutName);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test, Order(8)]
        public async Task Backend_Test_Post_Method_Add_Workout_In_WorkoutService_Throws_WorkoutException_If_Name_Exists()
        {
            ClearDatabase();

            // Set up initial workout data
            var initialWorkoutData = new Dictionary<string, object>
            {
                { "WorkoutId", 1 },
                { "WorkoutName", "Duplicate Workout" },
                { "Description", "Initial workout description" },
                { "DifficultyLevel", 3 },
                { "CreatedAt", DateTime.UtcNow },
                { "TargetArea", "Core" },
                { "DaysPerWeek", 4 },
                { "AverageWorkoutDurationInMinutes", 30 }
            };

            var initialWorkout = new Workout();
            foreach (var kvp in initialWorkoutData)
            {
                var propertyInfo = typeof(Workout).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(initialWorkout, kvp.Value);
                }
            }
            _context.Workouts.Add(initialWorkout);
            await _context.SaveChangesAsync();

            // Load assembly and types
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.WorkoutService";

            Type serviceType = assembly.GetType(serviceName);

            // Get the AddWorkout method
            MethodInfo addWorkoutMethod = serviceType.GetMethod("AddWorkout");

            // Check if method exists
            if (addWorkoutMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);

                // Attempt to add a new workout with the same name
                var duplicateWorkoutData = new Dictionary<string, object>
                {
                    { "WorkoutId", 2 },
                    { "WorkoutName", "Duplicate Workout" },
                    { "Description", "Duplicate workout description" },
                    { "DifficultyLevel", 2 },
                    { "CreatedAt", DateTime.UtcNow },
                    { "TargetArea", "Upper Body" },
                    { "DaysPerWeek", 2 },
                    { "AverageWorkoutDurationInMinutes", 25 }
                };

                var duplicateWorkout = new Workout();
                foreach (var kvp in duplicateWorkoutData)
                {
                    var propertyInfo = typeof(Workout).GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(duplicateWorkout, kvp.Value);
                    }
                }

                try
                {
                    var addWorkoutTask = (Task<bool>)addWorkoutMethod.Invoke(service, new object[] { duplicateWorkout });
                    Console.WriteLine("res" + addWorkoutTask.Result);

                    // If no exception is thrown, fail the test
                    Assert.Fail();
                }
                catch (Exception ex)
                {
                    Assert.IsNotNull(ex.InnerException);
                    Assert.IsTrue(ex.InnerException is WorkoutException);
                    Assert.AreEqual("Workout with the same name already exists", ex.InnerException.Message);
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test, Order(9)]
        public async Task Backend_Test_Post_Method_AddWorkout_In_Workout_Service_Adds_Successfully()
        {
            ClearDatabase();

            // Add initial workout
            var initialWorkoutData = new Dictionary<string, object>
            {
                { "WorkoutId", 1 },
                { "WorkoutName", "Unique Workout" },
                { "Description", "Initial Description" },
                { "DifficultyLevel", 3 },
                { "CreatedAt", DateTime.UtcNow },
                { "TargetArea", "Full Body" },
                { "DaysPerWeek", 3 },
                { "AverageWorkoutDurationInMinutes", 45 }
            };

            var initialWorkout = new Workout();
            foreach (var kvp in initialWorkoutData)
            {
                var propertyInfo = typeof(Workout).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(initialWorkout, kvp.Value);
                }
            }
            _context.Workouts.Add(initialWorkout);
            _context.SaveChanges();

            // New workout to add
            var newWorkoutData = new Dictionary<string, object>
            {
                { "WorkoutId", 2 },
                { "WorkoutName", "New Workout" },
                { "Description", "New Description" },
                { "DifficultyLevel", 2 },
                { "CreatedAt", DateTime.UtcNow },
                { "TargetArea", "Core" },
                { "DaysPerWeek", 4 },
                { "AverageWorkoutDurationInMinutes", 30 }
            };

            var newWorkout = new Workout();
            foreach (var kvp in newWorkoutData)
            {
                var propertyInfo = typeof(Workout).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(newWorkout, kvp.Value);
                }
            }

            // Invoke service method
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string serviceName = "dotnetapp.Services.WorkoutService";

            Type serviceType = assembly.GetType(serviceName);
            MethodInfo addMethod = serviceType.GetMethod("AddWorkout");

            if (addMethod != null)
            {
                var service = Activator.CreateInstance(serviceType, _context);
                var result = (Task<bool>)addMethod.Invoke(service, new object[] { newWorkout });
                var addResult = await result;

                Assert.IsTrue(addResult);

                var addedWorkout = await _context.Workouts.FindAsync(2);
                Assert.IsNotNull(addedWorkout);
                Assert.AreEqual("New Workout", addedWorkout.WorkoutName);
                Assert.AreEqual("New Description", addedWorkout.Description);
            }
            else
            {
                Assert.Fail();
            }
        }
[Test, Order(10)]
public async Task Backend_Test_Get_Method_GetWorkoutRequestsByUserId_In_WorkoutRequestService_Fetches_Successfully()
{
    ClearDatabase();

    // Set up user data
    var userData = new Dictionary<string, object>
    {
        { "UserId", 1 },
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
    await _context.SaveChangesAsync();

    // Set up workout data
    var workoutData = new Dictionary<string, object>
    {
        { "WorkoutId", 1 },
        { "WorkoutName", "Strength Training" },
        { "Description", "A workout focused on building strength" },
        { "DifficultyLevel", 4 },
        { "CreatedAt", DateTime.UtcNow },
        { "TargetArea", "Full Body" },
        { "DaysPerWeek", 3 },
        { "AverageWorkoutDurationInMinutes", 60 }
    };

    var workout = new Workout();
    foreach (var kvp in workoutData)
    {
        var propertyInfo = typeof(Workout).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(workout, kvp.Value);
        }
    }
    _context.Workouts.Add(workout);
    await _context.SaveChangesAsync();

    // Set up workout request data
    var workoutRequestData = new List<Dictionary<string, object>>
    {
        new Dictionary<string, object>
        {
            { "WorkoutRequestId", 1 },
            { "UserId", 1 },
            { "WorkoutId", 1 },
            { "Age", 25 },
            { "BMI", 22.5 },
            { "Gender", "Male" },
            { "DietaryPreferences", "Vegetarian" },
            { "MedicalHistory", "None" },
            { "RequestedDate", DateTime.UtcNow },
            { "RequestStatus", "Pending" }
        },
        new Dictionary<string, object>
        {
            { "WorkoutRequestId", 2 },
            { "UserId", 1 },
            { "WorkoutId", 1 },
            { "Age", 30 },
            { "BMI", 24.0 },
            { "Gender", "Female" },
            { "DietaryPreferences", "Vegan" },
            { "MedicalHistory", "None" },
            { "RequestedDate", DateTime.UtcNow },
            { "RequestStatus", "Approved" }
        }
    };

    foreach (var requestData in workoutRequestData)
    {
        var workoutRequest = new WorkoutRequest();
        foreach (var kvp in requestData)
        {
            var propertyInfo = typeof(WorkoutRequest).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(workoutRequest, kvp.Value);
            }
        }
        _context.WorkoutRequests.Add(workoutRequest);
    }
    await _context.SaveChangesAsync();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.WorkoutRequestService";

    Type serviceType = assembly.GetType(serviceName);

    // Get the GetWorkoutRequestsByUserId method
    MethodInfo getWorkoutRequestsByUserIdMethod = serviceType.GetMethod("GetWorkoutRequestsByUserId");

    // Check if method exists
    if (getWorkoutRequestsByUserIdMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var getWorkoutRequestsByUserIdTask = (Task<IEnumerable<WorkoutRequest>>)getWorkoutRequestsByUserIdMethod.Invoke(service, new object[] { 1 });
        var result = await getWorkoutRequestsByUserIdTask;
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.Any(wr => wr.Gender == "Male"));
        Assert.IsTrue(result.Any(wr => wr.Gender == "Female"));
    }
    else
    {
        Assert.Fail();
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

[Test, Order(13)]
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