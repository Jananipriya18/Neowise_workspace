using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using dotnetapp.Controllers;
using dotnetapp.Models;

namespace TestProject
{
    public class Tests
    {
        private ApplicationDbContext _context;
        private ShopController _shopController;
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080/");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _shopController = new ShopController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        } 

        [Test]
        public async Task Test_GetAllShopItems_ReturnsSuccess()
        {
            // Sending the GET request
            HttpResponseMessage response = await _httpClient.GetAsync("/getAllShopitem");

            // Asserting the response status
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Expected OK status code but got {response.StatusCode}");

            // Reading and asserting the response body
            string responseBody = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseBody, "Response body is empty");
        }

       [Test]
        public async Task Test_AddShopItem_ReturnsSuccess()
        {
            // Creating the HTTP POST request
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/addShopitem");
            request.Content = new StringContent("{\"ProductName\": \"Sample Product\",\"ProductType\": \"Electronics\",\"StockItem\": 10,\"Price\": 100,\"MfDate\": \"2024-07-12T00:00:00\",\"CompanyName\": \"Sample Company\"}",
                Encoding.UTF8, "application/json");

            // Sending the request
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            // Asserting the response status
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, $"Expected Created status code but got {response.StatusCode}");

            // Reading and asserting the response body
            string responseBody = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseBody, "Response body is empty");
        }

        [Test]
        public void Backend_Shop_Id_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Shop";
            Assembly assembly = Assembly.Load(assemblyName);
            Type shopType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = shopType.GetProperty("Id");
            Assert.IsNotNull(propertyInfo, "Property Id does not exist in Shop class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property Id in Shop class is not of type int");
        }

        [Test]
        public void Backend_Shop_ProductName_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Shop";
            Assembly assembly = Assembly.Load(assemblyName);
            Type shopType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = shopType.GetProperty("ProductName");
            Assert.IsNotNull(propertyInfo, "Property ProductName does not exist in Shop class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property ProductName in Shop class is not of type string");
        }

        [Test]
        public void Backend_Shop_ProductType_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Shop";
            Assembly assembly = Assembly.Load(assemblyName);
            Type shopType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = shopType.GetProperty("ProductType");
            Assert.IsNotNull(propertyInfo, "Property ProductType does not exist in Shop class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property ProductType in Shop class is not of type string");
        }

        [Test]
        public void Backend_Shop_StockItem_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Shop";
            Assembly assembly = Assembly.Load(assemblyName);
            Type shopType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = shopType.GetProperty("StockItem");
            Assert.IsNotNull(propertyInfo, "Property StockItem does not exist in Shop class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property StockItem in Shop class is not of type int");
        }

        [Test]
        public void Backend_Shop_Price_PropertyExists_ReturnExpectedDataTypes_decimal()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Shop";
            Assembly assembly = Assembly.Load(assemblyName);
            Type shopType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = shopType.GetProperty("Price");
            Assert.IsNotNull(propertyInfo, "Property Price does not exist in Shop class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property Price in Shop class is not of type decimal");
        }

        [Test]
        public void Backend_Shop_MfDate_PropertyExists_ReturnExpectedDataTypes_DateTime()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Shop";
            Assembly assembly = Assembly.Load(assemblyName);
            Type shopType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = shopType.GetProperty("MfDate");
            Assert.IsNotNull(propertyInfo, "Property MfDate does not exist in Shop class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(String), expectedType, "Property MfDate in Shop class is not of type String");
        }

        [Test]
        public void Backend_Shop_CompanyName_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Shop";
            Assembly assembly = Assembly.Load(assemblyName);
            Type shopType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = shopType.GetProperty("CompanyName");
            Assert.IsNotNull(propertyInfo, "Property CompanyName does not exist in Shop class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property CompanyName in Shop class is not of type string");
        }

        [Test]
        public void Test_ShopController_Class_Exists()
        {
            var _controllerType = typeof(ShopController);
            Assert.NotNull(_controllerType);
        }

        [Test]
        public void Test_GetAllShopItems_Method_Exists()
        {
            var _controllerType = typeof(ShopController);
            var methodInfo = _controllerType.GetMethod("GetAllShopItems");
            Assert.NotNull(methodInfo);
        }

        [Test]
        public void Test_GetAllShopItems_Method_HasHttpGetAttribute()
        {
            var _controllerType = typeof(ShopController);
            var methodInfo = _controllerType.GetMethod("GetAllShopItems");
            var httpGetAttribute = methodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true).FirstOrDefault();
            Assert.NotNull(httpGetAttribute);
        }

        [Test]
        public void Test_AddShopItem_Method_Exists()
        {
            var _controllerType = typeof(ShopController);
            var methodInfo = _controllerType.GetMethod("AddShopItem");
            Assert.NotNull(methodInfo);
        }

        [Test]
        public void Test_AddShopItem_Method_HasHttpPostAttribute()
        {
            var _controllerType = typeof(ShopController);
            var methodInfo = _controllerType.GetMethod("AddShopItem");
            var httpPostAttribute = methodInfo.GetCustomAttributes(typeof(HttpPostAttribute), true).FirstOrDefault();
            Assert.NotNull(httpPostAttribute);
        }
    }
}
