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
    public async Task Backend_Test_Post_Method_Register_Manager_Returns_HttpStatusCode_OK()
    {
        ClearDatabase();
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"ProjectManager\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
  
   [Test, Order(2)]
    public async Task Backend_Test_Post_Method_Login_Manager_Returns_HttpStatusCode_OK()
    {
        ClearDatabase();

        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"ProjectManager\"}}";
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
    public async Task Backend_Test_Get_All_Projects_With_Token_By_ProjectManager_Returns_HttpStatusCode_OK()
{
    ClearDatabase();
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"ProjectManager\"}}";
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
    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/projects");

    // Print feed response
    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
    Console.WriteLine("apiResponseBody: " + apiResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode);
}


[Test, Order(4)]
    public async Task Backend_Test_Get_All_Projects_Without_Token_By_ProjectManager_Returns_HttpStatusCode_Unauthorized()
{
    ClearDatabase();
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"ProjectManager\"}}";
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

    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/projects");

    // Print feed response
    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
    Console.WriteLine("apiResponseBody: " + apiResponseBody);

    Assert.AreEqual(HttpStatusCode.Unauthorized, apiResponse.StatusCode);
}


[Test, Order(5)]
public async Task Backend_Test_Post_Method_Add_Project_In_Project_Service_Adds_Project_Successfully()
{
    ClearDatabase();   
    // Set up project data
    var projectData = new Dictionary<string, object>
    {
        { "ProjectId", 1 },
        { "ProjectTitle", "Project Alpha" },
        { "ProjectDescription", "First project description" },
        { "StartDate", DateTime.Now.AddDays(5) },
        { "EndDate", DateTime.Now.AddDays(50) },
        { "Status", "Ongoing" },
        { "FrontEndTechStack", "React" },
        { "BackendTechStack", "ASP.NET Core" },
        { "Database", "SQL Server" }
    };

    // Create project instance and set properties
    var project = new Project();
    foreach (var kvp in projectData)
    {
        var propertyInfo = typeof(Project).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(project, kvp.Value);
        }
    }

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.ProjectService";
    string modelName = "dotnetapp.Models.Project";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get the AddProject method
    MethodInfo addProjectMethod = serviceType.GetMethod("AddProject");

    // Check if method exists
    if (addProjectMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var addProjectTask = (Task)addProjectMethod.Invoke(service, new object[] { project });
        await addProjectTask;

        // Verify that the project was added
        var retrievedProject = await _context.Projects.FindAsync(1);
        Assert.IsNotNull(retrievedProject);
        Assert.AreEqual(project.ProjectTitle, retrievedProject.ProjectTitle);
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(6)]
public async Task Backend_Test_Post_Method_Add_Project_In_ProjectService_Throws_ProjectException_If_ProjectTitle_Already_Exists()
{
    // Step 1: Clear the database to ensure no pre-existing data interferes with the test
    ClearDatabase();

    // Step 2: Set up the initial project data
    var projectData = new Dictionary<string, object>
    {
        { "ProjectId", 1 },
        { "ProjectTitle", "Project Alpha" },
        { "ProjectDescription", "First project description" },
        { "StartDate", DateTime.Now.AddDays(5) },
        { "EndDate", DateTime.Now.AddDays(50) },
        { "Status", "Ongoing" },
        { "FrontEndTechStack", "React" },
        { "BackendTechStack", "ASP.NET Core" },
        { "Database", "SQL Server" }
    };

    // Step 3: Create a Project instance and set its properties using reflection
    var project = new Project();
    foreach (var kvp in projectData)
    {
        var propertyInfo = typeof(Project).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(project, kvp.Value);
        }
    }

    // Step 4: Add the initial project to the context and save the changes
    _context.Projects.Add(project);
    await _context.SaveChangesAsync();

    // Step 5: Load the assembly and types required for reflection
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.ProjectService";

    Type serviceType = assembly.GetType(serviceName);

    // Step 6: Get the AddProject method using reflection
    MethodInfo addProjectMethod = serviceType.GetMethod("AddProject");

    // Step 7: Check if the AddProject method exists
    if (addProjectMethod != null)
    {
        // Step 8: Create an instance of the service type
        var service = Activator.CreateInstance(serviceType, _context);

        // Step 9: Set up the new project data with the same title as the existing project
        var newProjectData = new Dictionary<string, object>
        {
            { "ProjectId", 2 },
            { "ProjectTitle", "Project Alpha" },
            { "ProjectDescription", "Second project description" },
            { "StartDate", DateTime.Now.AddDays(10) },
            { "EndDate", DateTime.Now.AddDays(60) },
            { "Status", "Planned" },
            { "FrontEndTechStack", "Vue.js" },
            { "BackendTechStack", "Node.js" },
            { "Database", "MongoDB" }
        };

        // Step 10: Create a new Project instance and set its properties using reflection
        var newProject = new Project();
        foreach (var kvp in newProjectData)
        {
            var propertyInfo = typeof(Project).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(newProject, kvp.Value);
            }
        }

        try
        {
            // Step 11: Attempt to add the new project using the AddProject method
            var addProjectTask = (Task<bool>) addProjectMethod.Invoke(service, new object[] { newProject });
            Console.WriteLine("res" + addProjectTask.Result);

            Assert.Fail();
        }
        catch (Exception ex)
        {
            Assert.IsNotNull(ex.InnerException);
            Assert.IsTrue(ex.InnerException is ProjectException);
            Assert.AreEqual("Project with the same title already exists", ex.InnerException.Message);
        }
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(7)]
public async Task Backend_Test_Get_Method_Get_Project_By_Id_In_Project_Service_Fetches_Project_Successfully()
{
    ClearDatabase();
    // Set up project data
    var projectData = new Dictionary<string, object>
    {
        { "ProjectId", 1 },
        { "ProjectTitle", "Project Alpha" },
        { "ProjectDescription", "First project description" },
        { "StartDate", DateTime.Now.AddDays(5) },
        { "EndDate", DateTime.Now.AddDays(50) },
        { "Status", "Ongoing" },
        { "FrontEndTechStack", "React" },
        { "BackendTechStack", "ASP.NET Core" },
        { "Database", "SQL Server" }
    };

    // Create project instance and set properties
    var project = new Project();
    foreach (var kvp in projectData)
    {
        var propertyInfo = typeof(Project).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(project, kvp.Value);
        }
    }

    // Add project to the context and save changes
    _context.Projects.Add(project);
    await _context.SaveChangesAsync();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.ProjectService";
    string modelName = "dotnetapp.Models.Project";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get the GetProjectById method
    MethodInfo getProjectByIdMethod = serviceType.GetMethod("GetProjectById");

    // Check if method exists
    if (getProjectByIdMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var retrievedProjectTask = (Task<Project>)getProjectByIdMethod.Invoke(service, new object[] { 1 });
        var retrievedProject = await retrievedProjectTask;

        // Assert the retrieved project is not null and properties match
        Assert.IsNotNull(retrievedProject);
        Assert.AreEqual(project.ProjectTitle, retrievedProject.ProjectTitle);

    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(8)]
public async Task Backend_Test_Delete_Method_Delete_Project_By_Id_In_Project_Service_Deletes_Project_Successfully()
{
    ClearDatabase();
    // Set up project data
    var projectData = new Dictionary<string, object>
    {
        { "ProjectId", 1 },
        { "ProjectTitle", "Project Alpha" },
        { "ProjectDescription", "First project description" },
        { "StartDate", DateTime.Now.AddDays(5) },
        { "EndDate", DateTime.Now.AddDays(50) },
        { "Status", "Ongoing" },
        { "FrontEndTechStack", "React" },
        { "BackendTechStack", "ASP.NET Core" },
        { "Database", "SQL Server" }
    };

    // Create project instance and set properties
    var project = new Project();
    foreach (var kvp in projectData)
    {
        var propertyInfo = typeof(Project).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(project, kvp.Value);
        }
    }

    // Add project to the context and save changes
    _context.Projects.Add(project);
    await _context.SaveChangesAsync();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.ProjectService";
    string modelName = "dotnetapp.Models.Project";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get the DeleteProject method
    MethodInfo deleteProjectMethod = serviceType.GetMethod("DeleteProject");

    // Check if method exists
    if (deleteProjectMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var deleteProjectTask = (Task<bool>)deleteProjectMethod.Invoke(service, new object[] { 1 });
        var result = await deleteProjectTask;

        // Assert the project was deleted successfully
        Assert.IsTrue(result);

        // Verify that the project no longer exists
        var retrievedProject = await _context.Projects.FindAsync(1);
        Assert.IsNull(retrievedProject);
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(9)]
public async Task Backend_Test_GetAll_Method_Get_All_Project_Requirements_In_Project_Requirement_Service_Fetches_All_Requirements_Successfully()
    {
        ClearDatabase();

        // Set up user data
        var userData = new Dictionary<string, object>
        {
            { "UserId", 403 },
            { "Username", "requirementuser" },
            { "Password", "requirementpassword" },
            { "Email", "requirement@example.com" },
            { "MobileNumber", "1231231234" },
            { "UserRole", "RequirementAnalyst" }
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

        // Set up project requirement data
        var projectRequirementData1 = new Dictionary<string, object>
        {
            { "RequirementId", 1 },
            { "UserId", 403 },
            { "RequirementTitle", "Requirement One" },
            { "RequirementDescription", "First requirement description" },
            { "Status", "Pending" }
        };

        var projectRequirementData2 = new Dictionary<string, object>
        {
            { "RequirementId", 2 },
            { "UserId", 403 },
            { "RequirementTitle", "Requirement Two" },
            { "RequirementDescription", "Second requirement description" },
            { "Status", "Pending" }
        };

        var projectRequirement1 = new ProjectRequirement();
        foreach (var kvp in projectRequirementData1)
        {
            var propertyInfo = typeof(ProjectRequirement).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(projectRequirement1, kvp.Value);
            }
        }

        var projectRequirement2 = new ProjectRequirement();
        foreach (var kvp in projectRequirementData2)
        {
            var propertyInfo = typeof(ProjectRequirement).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(projectRequirement2, kvp.Value);
            }
        }

        _context.ProjectRequirements.Add(projectRequirement1);
        _context.ProjectRequirements.Add(projectRequirement2);
        await _context.SaveChangesAsync();

        // Load assembly and types
        string assemblyName = "dotnetapp";
        Assembly assembly = Assembly.Load(assemblyName);
        string serviceName = "dotnetapp.Services.ProjectRequirementService";

        Type serviceType = assembly.GetType(serviceName);

        // Get the GetAllProjectRequirements method
        MethodInfo getAllProjectRequirementsMethod = serviceType.GetMethod("GetAllProjectRequirements");

        // Check if method exists
        if (getAllProjectRequirementsMethod != null)
        {
            var service = Activator.CreateInstance(serviceType, _context);
            var retrievedRequirementsTask = (Task<IEnumerable<ProjectRequirement>>)getAllProjectRequirementsMethod.Invoke(service, null);
            var retrievedRequirements = await retrievedRequirementsTask;

            // Assert the retrieved requirements are not null and match the expected count
            Assert.IsNotNull(retrievedRequirements);
            Assert.AreEqual(2, retrievedRequirements.Count());
        }
        else
        {
            Assert.Fail();
        }
    }

[Test, Order(10)]
   public async Task Backend_Test_Get_Method_Get_Project_Requirement_By_Id_In_Project_Requirement_Service_Fetches_Requirement_Successfully()
    {
        ClearDatabase();

        // Set up user data
        var userData = new Dictionary<string, object>
        {
            { "UserId", 403 },
            { "Username", "requirementuser" },
            { "Password", "requirementpassword" },
            { "Email", "requirement@example.com" },
            { "MobileNumber", "1231231234" },
            { "UserRole", "RequirementAnalyst" }
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

        // Set up project requirement data
        var projectRequirementData = new Dictionary<string, object>
        {
            { "RequirementId", 1 },
            { "UserId", 403 },
            { "RequirementTitle", "Requirement One" },
            { "RequirementDescription", "First requirement description" },
            { "Status", "Pending" }
        };

        var projectRequirement = new ProjectRequirement();
        foreach (var kvp in projectRequirementData)
        {
            var propertyInfo = typeof(ProjectRequirement).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(projectRequirement, kvp.Value);
            }
        }

        _context.ProjectRequirements.Add(projectRequirement);
        await _context.SaveChangesAsync();

        // Load assembly and types
        string assemblyName = "dotnetapp";
        Assembly assembly = Assembly.Load(assemblyName);
        string serviceName = "dotnetapp.Services.ProjectRequirementService";

        Type serviceType = assembly.GetType(serviceName);

        // Get the GetProjectRequirementById method
        MethodInfo getProjectRequirementByIdMethod = serviceType.GetMethod("GetProjectRequirementById");

        // Check if method exists
        if (getProjectRequirementByIdMethod != null)
        {
            var service = Activator.CreateInstance(serviceType, _context);
            var retrievedRequirementTask = (Task<ProjectRequirement>)getProjectRequirementByIdMethod.Invoke(service, new object[] { 1 });
            var retrievedRequirement = await retrievedRequirementTask;

            // Assert the retrieved requirement is not null and properties match
            Assert.IsNotNull(retrievedRequirement);
            Assert.AreEqual(projectRequirement.RequirementTitle, retrievedRequirement.RequirementTitle);
    
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
        { "UserRole", "Employee" }
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
        { "UserRole", "Employee" }
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
        { "UserRole", "Employee" }
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

