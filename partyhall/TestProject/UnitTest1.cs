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

        string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

        // Print login response
        string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        Console.WriteLine("Login Response: " + loginResponseBody);

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }
   
   [Test, Order(3)]
public async Task Backend_Test_Get_All_Bookings_With_Token_By_Admin_Returns_HttpStatusCode_OK()
{
    ClearDatabase();
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Customer\"}}";
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
    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/booking");

    // Print feed response
    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
    // Console.WriteLine("apiResponseBody: " + apiResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode);
}


[Test, Order(4)]
public async Task Backend_Test_Get_All_Bookings_Without_Token_By_Admin_Returns_HttpStatusCode_Unauthorized()
{
    ClearDatabase();
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Manager\"}}";
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

    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/booking");

    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
    // Console.WriteLine("apiResponseBody: " + apiResponseBody);

    Assert.AreEqual(HttpStatusCode.Unauthorized, apiResponse.StatusCode);
}
[Test, Order(5)]
public async Task Backend_Test_Get_Method_Get_PartyHallById_In_PartyHall_Service_Fetches_PartyHall_Successfully()
{
    ClearDatabase();

    // Set up party hall data
    var partyHallData = new Dictionary<string, object>
    {
        { "PartyHallId", 1 },
        { "HallName", "Grand Ballroom" },
        { "HallImageUrl", "http://example.com/image.jpg" },
        { "HallLocation", "City Center" },
        { "HallAvailableStatus", "Available" }, // Make sure this matches the property type in your model
        { "Price", 500 },
        { "Capacity", 100 },
        { "Description", "A luxurious and spacious ballroom." }
    };

    // Create party hall instance and set properties
    var partyHall = new PartyHall();
    foreach (var kvp in partyHallData)
    {
        var propertyInfo = typeof(PartyHall).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(partyHall, kvp.Value);
        }
    }

    // Add party hall to the context and save changes
    _context.PartyHalls.Add(partyHall);
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.PartyHallService";
    string modelName = "dotnetapp.Models.PartyHall";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get the GetPartyHallByIdAsync method
    MethodInfo getPartyHallByIdMethod = serviceType.GetMethod("GetPartyHallByIdAsync");

    // Check if method exists
    if (getPartyHallByIdMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var retrievedPartyHall = (Task<PartyHall>)getPartyHallByIdMethod.Invoke(service, new object[] { 1 });
        Assert.IsNotNull(retrievedPartyHall);
        Assert.AreEqual(partyHall.HallName, retrievedPartyHall.Result.HallName);
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(6)]
public async Task Backend_Test_Get_Method_Get_All_PartyHalls_In_PartyHall_Service_Fetches_All_PartyHalls_Successfully()
{
    ClearDatabase();

    // Set up party hall data
    var partyHallsData = new List<Dictionary<string, object>>
    {
        new Dictionary<string, object>
        {
            { "PartyHallId", 1L },
            { "HallName", "Grand Ballroom" },
            { "HallImageUrl", "http://example.com/image1.jpg" },
            { "HallLocation", "City Center" },
            { "HallAvailableStatus", "Available" },
            { "Price", 500L },
            { "Capacity", 100 },
            { "Description", "A luxurious and spacious ballroom." }
        },
        new Dictionary<string, object>
        {
            { "PartyHallId", 2L },
            { "HallName", "Elegant Banquet Hall" },
            { "HallImageUrl", "http://example.com/image2.jpg" },
            { "HallLocation", "Downtown" },
            { "HallAvailableStatus", "Available" },
            { "Price", 400L },
            { "Capacity", 80 },
            { "Description", "An elegant venue for celebrations." }
        }
    };

    // Add party halls to the context and save changes
    foreach (var partyHallData in partyHallsData)
    {
        var partyHall = new PartyHall();
        foreach (var kvp in partyHallData)
        {
            var propertyInfo = typeof(PartyHall).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(partyHall, kvp.Value);
            }
        }
        _context.PartyHalls.Add(partyHall);
    }
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.PartyHallService";
    string modelName = "dotnetapp.Models.PartyHall";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get the GetAllPartyHallsAsync method
    MethodInfo getAllPartyHallsMethod = serviceType.GetMethod("GetAllPartyHallsAsync");

    // Check if method exists
    if (getAllPartyHallsMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var retrievedPartyHalls = (Task<IEnumerable<PartyHall>>)getAllPartyHallsMethod.Invoke(service, null);

        // Assert the retrieved party halls are not null and match the added party halls
        Assert.IsNotNull(retrievedPartyHalls);
        var partyHallsList = retrievedPartyHalls.Result.ToList();
        Assert.AreEqual(2, partyHallsList.Count);
        Assert.AreEqual("Grand Ballroom", partyHallsList[0].HallName);
        Assert.AreEqual("Elegant Banquet Hall", partyHallsList[1].HallName);
        // Add assertions for other properties if needed
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(7)]
public async Task Backend_Test_Post_Method_Add_PartyHall_In_PartyHall_Service_Adds_PartyHall_Successfully()
{
    ClearDatabase();

    // Set up party hall data
    var partyHallData = new Dictionary<string, object>
    {
        { "PartyHallId", 1L },
        { "HallName", "Grand Ballroom" },
        { "HallImageUrl", "http://example.com/image.jpg" },
        { "HallLocation", "City Center" },
        { "HallAvailableStatus", "Available" },
        { "Price", 500L },
        { "Capacity", 100 },
        { "Description", "A luxurious and spacious ballroom." }
    };

    // Create party hall instance and set properties
    var partyHall = new PartyHall();
    foreach (var kvp in partyHallData)
    {
        var propertyInfo = typeof(PartyHall).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(partyHall, kvp.Value);
        }
    }

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.PartyHallService";
    string modelName = "dotnetapp.Models.PartyHall";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get the AddPartyHallAsync method
    MethodInfo addPartyHallMethod = serviceType.GetMethod("AddPartyHallAsync");

    // Check if method exists
    if (addPartyHallMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var addedPartyHallTask = (Task<PartyHall>)addPartyHallMethod.Invoke(service, new object[] { partyHall });
        await addedPartyHallTask;
        var findPartyHall = await _context.PartyHalls.FindAsync(1L);
        Assert.IsNotNull(findPartyHall);
        Assert.AreEqual("Grand Ballroom", findPartyHall.HallName);
    }
    else
    {
        Assert.Fail();
    }
}
[Test, Order(8)]
public async Task Backend_Test_Delete_Method_Delete_PartyHall_In_PartyHall_Service_Deletes_PartyHall_Successfully()
{
    ClearDatabase();

    // Set up party hall data
    var partyHallData = new Dictionary<string, object>
    {
        { "PartyHallId", 1L },
        { "HallName", "Grand Ballroom" },
        { "HallImageUrl", "http://example.com/image.jpg" },
        { "HallLocation", "City Center" },
        { "HallAvailableStatus", "Available" },
        { "Price", 500L },
        { "Capacity", 100 },
        { "Description", "A luxurious and spacious ballroom." }
    };

    // Create and add party hall to the context
    var partyHall = new PartyHall();
    foreach (var kvp in partyHallData)
    {
        var propertyInfo = typeof(PartyHall).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(partyHall, kvp.Value);
        }
    }
    _context.PartyHalls.Add(partyHall);
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.PartyHallService";
    string modelName = "dotnetapp.Models.PartyHall";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get the DeletePartyHallAsync method
    MethodInfo deletePartyHallMethod = serviceType.GetMethod("DeletePartyHallAsync");

    // Check if method exists
    if (deletePartyHallMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var deletedPartyHall = (Task<PartyHall>)deletePartyHallMethod.Invoke(service, new object[] { 1L });

        // Assert that the deleted party hall is not null
        Assert.IsNotNull(deletedPartyHall);

        // Check if the deleted party hall is not present in the context
        var deletedPartyHallFromContext = await _context.PartyHalls.FindAsync(1L);
        Assert.IsNull(deletedPartyHallFromContext);
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(9)]
public async Task Backend_Test_Post_Method_AddPartyHall_In_PartyHallService_Occurs_PartyHallException_For_Duplicate_Name()
{
    ClearDatabase();

    // Set up party hall data
    var partyHallData = new Dictionary<string, object>
    {
        { "PartyHallId", 1L },
        { "HallName", "Grand Ballroom" },
        { "HallImageUrl", "http://example.com/image.jpg" },
        { "HallLocation", "City Center" },
        { "HallAvailableStatus", "Available" },
        { "Price", 500L },
        { "Capacity", 100 },
        { "Description", "A luxurious and spacious ballroom." }
    };

    // Create party hall instance and set properties
    var partyHall = new PartyHall();
    foreach (var kvp in partyHallData)
    {
        var propertyInfo = typeof(PartyHall).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(partyHall, kvp.Value);
        }
    }

    // Add party hall to the context and save changes
    _context.PartyHalls.Add(partyHall);
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.PartyHallService";
    string modelName = "dotnetapp.Models.PartyHall";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get the AddPartyHallAsync method
    MethodInfo addPartyHallMethod = serviceType.GetMethod("AddPartyHallAsync");

    // Check if method exists
    if (addPartyHallMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        // Attempt to add a party hall with the same name again
        try
        {
            var result1 = (Task<PartyHall>)addPartyHallMethod.Invoke(service, new object[] { partyHall });
            Console.WriteLine("res" + result1.Result);
            Assert.Fail();
        }
        catch (Exception ex)
        {
            Assert.IsTrue(ex.InnerException is PartyHallException);
            Assert.AreEqual("A party hall with the same name already exists", ex.InnerException.Message);
        }
    }
    else
    {
        Assert.Fail("AddPartyHallAsync method not found.");
    }
}


[Test, Order(10)]
public async Task Backend_Test_Post_Method_Add_Review_In_Review_Service_Adds_Review_Successfully()
{
    ClearDatabase();

    // Set up user data
    var userData = new Dictionary<string, object>
    {
        { "UserId", 1L },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Customer" }
    };

    // Create user instance and set properties
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

    // Create review data
    var reviewData = new Dictionary<string, object>
    {
        { "ReviewId", 1 },
        { "UserId", 1L },
        { "Rating", 5 },
        { "Subject", "Amazing resort!" },
        { "Body", "It was an amazing experience at this resort." },
        { "DateCreated", DateTime.Now }
    };

    // Create review instance and set properties
    var review = new Review();
    foreach (var kvp in reviewData)
    {
        var propertyInfo = typeof(Review).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(review, kvp.Value);
        }
    }

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.ReviewService";
    string modelName = "dotnetapp.Models.Review";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get the AddReviewAsync method
    MethodInfo addReviewMethod = serviceType.GetMethod("AddReviewAsync");

    // Check if method exists
    if (addReviewMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var addedReview = (Task<Review>)addReviewMethod.Invoke(service, new object[] { review });

        // Assert the added review is not null and matches the input
        Assert.IsNotNull(addedReview);
        Assert.AreEqual(review.Subject, addedReview.Result.Subject);
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(11)]
public async Task Backend_Test_Get_Method_Get_All_Reviews_In_Review_Service_Fetches_All_Reviews_Successfully()
{
    ClearDatabase();

    // Set up user data
    var userData = new Dictionary<string, object>
    {
        { "UserId", 1L },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Customer" }
    };

    // Create user instance and set properties
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

    // Set up review data
    var reviewsData = new List<Dictionary<string, object>>
    {
        new Dictionary<string, object>
        {
            { "ReviewId", 1 },
            { "UserId", 1L },
            { "Rating", 5 },
            { "Subject", "Amazing resort!" },
            { "Body", "It was an amazing experience at this resort." },
            { "DateCreated", DateTime.Now }
        },
        new Dictionary<string, object>
        {
            { "ReviewId", 2 },
            { "UserId", 1L },
            { "Rating", 4 },
            { "Subject", "Very nice, but a bit pricey." },
            { "Body", "The resort was nice, but the price was a bit high." },
            { "DateCreated", DateTime.Now }
        }
    };

    // Add reviews to the context and save changes
    foreach (var reviewData in reviewsData)
    {
        var review = new Review();
        foreach (var kvp in reviewData)
        {
            var propertyInfo = typeof(Review).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(review, kvp.Value);
            }
        }
        _context.Reviews.Add(review);
    }
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.ReviewService";
    string modelName = "dotnetapp.Models.Review";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get the GetAllReviewsAsync method
    MethodInfo getAllReviewsMethod = serviceType.GetMethod("GetAllReviewsAsync");

    // Check if method exists
    if (getAllReviewsMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var retrievedReviews = (Task<List<Review>>)getAllReviewsMethod.Invoke(service, null);

        // Assert the retrieved reviews are not null and match the added reviews
        Assert.IsNotNull(retrievedReviews);
        Assert.AreEqual(2, retrievedReviews.Result.Count);

        // Add assertions for other properties if needed
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(12)]
public async Task Backend_Test_Get_Method_Get_All_Bookings_In_Booking_Service_Fetches_All_Bookings_Successfully()
{
    ClearDatabase();

    // Set up user data
    var userData = new Dictionary<string, object>
    {
        { "UserId", 1L },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Customer" }
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

  var partyHallData = new Dictionary<string, object>
    {
        { "PartyHallId", 1L },
        { "HallName", "Grand Ballroom" },
        { "HallImageUrl", "http://example.com/image.jpg" },
        { "HallLocation", "City Center" },
        { "HallAvailableStatus", "Available" },
        { "Price", 500L },
        { "Capacity", 100 },
        { "Description", "A luxurious and spacious ballroom." }
    };

    // Create party hall instance and set properties
    var partyHall = new PartyHall();
    foreach (var kvp in partyHallData)
    {
        var propertyInfo = typeof(PartyHall).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(partyHall, kvp.Value);
        }
    }

    // Add party hall to the context and save changes
    _context.PartyHalls.Add(partyHall);

    // Set up booking data
    var bookingData = new Dictionary<string, object>
    {
        { "BookingId", 1L },
        { "UserId", 1L },
        { "PartyHallId", 1L },
        { "FromDate", DateTime.Now.AddDays(1) },
        { "ToDate", DateTime.Now.AddDays(3) },
        { "TotalPrice", 400 },
        { "NoOfPersons", 2 },
        { "Status", "Confirmed" },
        { "Address", "123 Beach Road" }
    };

    // Create and add user, resort, and booking to the context
  

    var booking = new Booking();
    foreach (var kvp in bookingData)
    {
        var propertyInfo = typeof(Booking).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(booking, kvp.Value);
        }
    }
    _context.Bookings.Add(booking);

    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.BookingService";

    Type serviceType = assembly.GetType(serviceName);

    // Get the GetAllBookingsAsync method
    MethodInfo getAllBookingsMethod = serviceType.GetMethod("GetAllBookingsAsync");

    // Check if method exists
    if (getAllBookingsMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var retrievedBookings = (Task<IEnumerable<Booking>>)getAllBookingsMethod.Invoke(service, null);
        Assert.IsNotNull(retrievedBookings);
    }
    else
    {
        Assert.Fail();
    }
}
[Test, Order(13)]
public async Task Backend_Test_Get_Method_Get_Booking_By_Id_In_Booking_Service_Returns_Correct_Booking()
{
    ClearDatabase();

    // Set up user data
    var userData = new Dictionary<string, object>
    {
        { "UserId", 1L },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Customer" }
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


  var partyHallData = new Dictionary<string, object>
    {
        { "PartyHallId", 1L },
        { "HallName", "Grand Ballroom" },
        { "HallImageUrl", "http://example.com/image.jpg" },
        { "HallLocation", "City Center" },
        { "HallAvailableStatus", "Available" },
        { "Price", 500L },
        { "Capacity", 100 },
        { "Description", "A luxurious and spacious ballroom." }
    };

    // Create party hall instance and set properties
    var partyHall = new PartyHall();
    foreach (var kvp in partyHallData)
    {
        var propertyInfo = typeof(PartyHall).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(partyHall, kvp.Value);
        }
    }

    // Add party hall to the context and save changes
    _context.PartyHalls.Add(partyHall);
    
    // Set up booking data
    var bookingData = new Dictionary<string, object>
    {
        { "BookingId", 1L },
        { "UserId", 1L },
        { "PartyHallId", 1L },
        { "FromDate", DateTime.Now.AddDays(1) },
        { "ToDate", DateTime.Now.AddDays(3) },
        { "TotalPrice", 400 },
        { "NoOfPersons", 2 },
        { "Status", "Confirmed" },
        { "Address", "123 Beach Road" }
    };

    // Create and add user, resort, and booking to the context
 


    var booking = new Booking();
    foreach (var kvp in bookingData)
    {
        var propertyInfo = typeof(Booking).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(booking, kvp.Value);
        }
    }
    _context.Bookings.Add(booking);

    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.BookingService";

    Type serviceType = assembly.GetType(serviceName);

    // Get the GetBookingByIdAsync method
    MethodInfo getBookingByIdMethod = serviceType.GetMethod("GetBookingByIdAsync");

    // Check if method exists
    if (getBookingByIdMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var retrievedBooking = (Task<Booking>)getBookingByIdMethod.Invoke(service, new object[] { 1L });

        // Assert the retrieved booking is not null and has the correct ID
        Assert.IsNotNull(retrievedBooking);
        Assert.AreEqual("123 Beach Road", retrievedBooking.Result.Address);
    }
    else
    {
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

