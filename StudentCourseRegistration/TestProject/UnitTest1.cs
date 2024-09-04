// using NUnit.Framework;
// using System;
// using System.Net;
// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using dotnetapp.Models;
// using System.Reflection;

// namespace dotnetapp.Tests
// {
//     [TestFixture]
//     public class MusicRecordsControllerTests
//     {
//         private HttpClient _httpClient;
//         private Assembly _assembly;

//         private MusicRecord _testMusicRecord;
//         private Order _testOrder;

//         [SetUp]
//         public async Task Setup()
//         {
//             _httpClient = new HttpClient();
//             _httpClient.BaseAddress = new Uri("http://localhost:8080"); // Base URL of your API
//         }

//         private async Task<MusicRecord> CreateTestMusicRecord()
//         {
//             var newMusicRecord = new MusicRecord
//             {
//                 Artist = "Test Artist",
//                 Album = "Test Album",
//                 Genre = "Test Genre",
//                 Price = 19.99m,
//                 StockQuantity = 10
//             };

//             var json = JsonConvert.SerializeObject(newMusicRecord);
//             var content = new StringContent(json, Encoding.UTF8, "application/json");

//             var response = await _httpClient.PostAsync("api/MusicRecord", content);
//             response.EnsureSuccessStatusCode();

//             return JsonConvert.DeserializeObject<MusicRecord>(await response.Content.ReadAsStringAsync());
//         }

//         [Test]
//         public async Task CreateTestOrder_ReturnsCreatedOrder()
//         {
//             // Arrange
//             var newOrder = new Order
//             {
//                 CustomerName = "Test Customer",
//                 OrderDate = "2024-10-24" // Format to match the string format in the model
//                 // Initialize other properties if needed
//             };

//             var json = JsonConvert.SerializeObject(newOrder);
//             var content = new StringContent(json, Encoding.UTF8, "application/json");

//             // Act
//             var response = await _httpClient.PostAsync("api/Order", content);
//             response.EnsureSuccessStatusCode();

//             // Assert
//             var createdOrderJson = await response.Content.ReadAsStringAsync();
//             var createdOrder = JsonConvert.DeserializeObject<Order>(createdOrderJson);

//             Assert.IsNotNull(createdOrder);
//             Assert.AreEqual(newOrder.CustomerName, createdOrder.CustomerName);
//             Assert.AreEqual(newOrder.OrderDate, createdOrder.OrderDate);
//         }

//         [Test]
//         public async Task CreateTestMusicRecord_ReturnsCreatedMusicRecord()
//         {
//             // Arrange
//             int validOrderId = await CreateTestOrderAndGetId(); // Dynamically get a valid OrderId

//             var newMusicRecord = new MusicRecord
//             {
//                 Artist = "Test Artist",
//                 Album = "Test Album",
//                 Genre = "Test Genre",
//                 Price = 19.99m,
//                 StockQuantity = 10,
//                 OrderId = validOrderId // Use the valid OrderId obtained from the helper method
//             };

//             var json = JsonConvert.SerializeObject(newMusicRecord);
//             var content = new StringContent(json, Encoding.UTF8, "application/json");

//             // Act
//             var response = await _httpClient.PostAsync("api/MusicRecord", content);
//             response.EnsureSuccessStatusCode();

//             // Assert
//             var createdMusicRecordJson = await response.Content.ReadAsStringAsync();
//             var createdMusicRecord = JsonConvert.DeserializeObject<MusicRecord>(createdMusicRecordJson);

//             Assert.IsNotNull(createdMusicRecord);
//             Assert.AreEqual(newMusicRecord.Artist, createdMusicRecord.Artist);
//             Assert.AreEqual(newMusicRecord.Album, createdMusicRecord.Album);
//             Assert.AreEqual(newMusicRecord.Genre, createdMusicRecord.Genre);
//             Assert.AreEqual(newMusicRecord.Price, createdMusicRecord.Price);
//             Assert.AreEqual(newMusicRecord.StockQuantity, createdMusicRecord.StockQuantity);
//             Assert.AreEqual(newMusicRecord.OrderId, createdMusicRecord.OrderId); // Ensure OrderId matches
//         }



//         [Test]
//         public async Task Test_GetAllMusicRecords_ReturnsListOfMusicRecords()
//         {
//             var response = await _httpClient.GetAsync("api/MusicRecord");
//             response.EnsureSuccessStatusCode();

//             var content = await response.Content.ReadAsStringAsync();
//             var musicRecords = JsonConvert.DeserializeObject<MusicRecord[]>(content);

//             Assert.IsNotNull(musicRecords);
//             Assert.IsTrue(musicRecords.Length > 0);
//         }

//         [Test]
//         public async Task Test_GetOrders_ReturnsListOfOrders()
//         {
//             var response = await _httpClient.GetAsync("api/Order");
//             response.EnsureSuccessStatusCode();

//             var content = await response.Content.ReadAsStringAsync();
//             var orders = JsonConvert.DeserializeObject<Order[]>(content);

//             Assert.IsNotNull(orders);
//             Assert.IsTrue(orders.Length > 0);
//         }


//         [Test]
//         public async Task Test_GetMusicRecordById_InvalidId_ReturnsNotFound()
//         {
//             var response = await _httpClient.GetAsync($"api/MusicRecord/999");

//             Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
//         }

//         [Test]
//         public async Task Test_GetOrderId_InvalidId_ReturnsNotFound()
//         {
//             var response = await _httpClient.GetAsync($"api/Order/999");

//             Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
//         }

//         private async Task<int> CreateTestOrderAndGetId()
//         {
//             var newOrder = new Order
//             {
//                 CustomerName = "Test Customer",
//                 OrderDate = "2024-10-24" // Use a valid format
//             };

//             var json = JsonConvert.SerializeObject(newOrder);
//             var content = new StringContent(json, Encoding.UTF8, "application/json");

//             var response = await _httpClient.PostAsync("api/Order", content);
//             response.EnsureSuccessStatusCode();

//             var createdOrderJson = await response.Content.ReadAsStringAsync();
//             var createdOrder = JsonConvert.DeserializeObject<Order>(createdOrderJson);

//             return createdOrder.OrderId; // Return the ID of the created Order
//         }

//         [Test]
//         public async Task Test_AddMusicRecord_ReturnsCreatedResponse()
//         {
//             // Arrange
//             int validOrderId = await CreateTestOrderAndGetId(); // Use the helper method to get a valid OrderId

//             var newMusicRecord = new MusicRecord
//             {
//                 Artist = "Test Artist",
//                 Album = "Test Album",
//                 Genre = "Test Genre",
//                 Price = 19.99m,
//                 StockQuantity = 10,
//                 OrderId = validOrderId // Use the valid OrderId obtained from the helper method
//             };

//             var json = JsonConvert.SerializeObject(newMusicRecord);
//             var content = new StringContent(json, Encoding.UTF8, "application/json");

//             // Act
//             var response = await _httpClient.PostAsync("api/MusicRecord", content);
//             response.EnsureSuccessStatusCode();

//             // Assert
//             var createdMusicRecordJson = await response.Content.ReadAsStringAsync();
//             var createdMusicRecord = JsonConvert.DeserializeObject<MusicRecord>(createdMusicRecordJson);

//             Assert.IsNotNull(createdMusicRecord);
//             Assert.AreEqual(newMusicRecord.Artist, createdMusicRecord.Artist);
//             Assert.AreEqual(newMusicRecord.OrderId, createdMusicRecord.OrderId); // Ensure OrderId matches
//         }


//         [Test]
//         public async Task Test_AddOrder_ReturnsCreatedResponse()
//         {
//             // Arrange
//             var newOrder = new Order
//             {
//                 CustomerName = "Test Customer",
//                 OrderDate = "2024-20-24" // Ensure the date format matches your model's expectations
//                 // Initialize other properties if needed
//             };

//             var json = JsonConvert.SerializeObject(newOrder);
//             var content = new StringContent(json, Encoding.UTF8, "application/json");

//             // Act
//             var response = await _httpClient.PostAsync("api/Order", content);

//             // Assert
//             response.EnsureSuccessStatusCode(); // Ensure the response status is 200-299

//             var createdOrder = JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());

//             Assert.IsNotNull(createdOrder, "The created order is null.");
//             Assert.AreEqual(newOrder.CustomerName, createdOrder.CustomerName, "Customer names do not match.");
//             Assert.AreEqual(newOrder.OrderDate, createdOrder.OrderDate, "Order dates do not match.");
//             // Add additional assertions as needed
//         }


//         [TearDown]
//         public async Task Cleanup()
//         {
//             if (_testMusicRecord != null)
//             {
//                 var response = await _httpClient.DeleteAsync($"api/MusicRecord/{_testMusicRecord.MusicRecordId}");
//                 if (response.StatusCode != HttpStatusCode.NotFound)
//                 {
//                     response.EnsureSuccessStatusCode();
//                 }
//             }
//             _httpClient.Dispose();
//         }
//     }
// }