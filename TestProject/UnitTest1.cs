using System;
using System.Collections.Generic;
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
using dotnetapp.Exceptions;
using dotnetapp.Models;

namespace TestProject
{
    public class Tests
    {
        private ApplicationDbContext _context;
        private PantryItemController _pantryItemController;
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
            _pantryItemController = new PantryItemController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task Test_GetAllPantryItems_ReturnsSuccess()
        {
            // Sending the GET request
            HttpResponseMessage response = await _httpClient.GetAsync("/getAllPantryitem");

            // Asserting the response status
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Expected OK status code but got {response.StatusCode}");

            // Reading and asserting the response body
            string responseBody = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseBody, "Response body is empty");
        }

        [Test]
        public async Task Test_AddPantryItem_ReturnsSuccess()
        {
            // Arrange
            var newPantryItemData = new Dictionary<string, object>
            {
                { "ProductName", "Sample Product" },
                { "ProductType", "Grocery" },
                { "StockItem", 10 },
                { "Price", 100 },
                { "ExpDate", "2024-07-12" }
            };

            var newPantryItem = CreatePantryItemObject(newPantryItemData);

            // Act
            var result = await _pantryItemController.AddPantryItem(newPantryItem);

            // Assert
            Assert.IsNotNull(result);

            // Additional assertions if needed
            var createdPantryItem = GetEntityFromDatabase<PantryItem>(_context, "PantryItems", 1); // Adjust with actual ID or suitable condition
            Assert.IsNotNull(createdPantryItem);
            Assert.AreEqual(newPantryItem.ProductName, createdPantryItem.ProductName);
        }

        private PantryItem CreatePantryItemObject(Dictionary<string, object> pantryItemData)
        {
            var pantryItem = new PantryItem();

            foreach (var kvp in pantryItemData)
            {
                var property = typeof(PantryItem).GetProperty(kvp.Key);
                if (property != null)
                {
                    var value = Convert.ChangeType(kvp.Value, property.PropertyType);
                    property.SetValue(pantryItem, value);
                }
            }

            return pantryItem;
        }

        private TEntity GetEntityFromDatabase<TEntity>(DbContext context, string collectionName, int id)
        {
            var entityType = typeof(TEntity);
            var propertyInfoId = entityType.GetProperty("Id");

            var propertyInfoCollection = context.GetType().GetProperty(collectionName);
            var entities = propertyInfoCollection.GetValue(context, null) as IEnumerable<TEntity>;

            var entity = entities.FirstOrDefault(e => (int)propertyInfoId.GetValue(e) == id);
            return entity;
        }

        [Test]
        public void Backend_PantryItem_Id_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.PantryItem";
            Assembly assembly = Assembly.Load(assemblyName);
            Type pantryItemType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = pantryItemType.GetProperty("Id");
            Assert.IsNotNull(propertyInfo, "Property Id does not exist in PantryItem class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property Id in PantryItem class is not of type int");
        }

        [Test]
        public void Backend_PantryItem_ProductName_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.PantryItem";
            Assembly assembly = Assembly.Load(assemblyName);
            Type pantryItemType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = pantryItemType.GetProperty("ProductName");
            Assert.IsNotNull(propertyInfo, "Property ProductName does not exist in PantryItem class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property ProductName in PantryItem class is not of type string");
        }

        [Test]
        public void Backend_PantryItem_ProductType_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.PantryItem";
            Assembly assembly = Assembly.Load(assemblyName);
            Type pantryItemType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = pantryItemType.GetProperty("ProductType");
            Assert.IsNotNull(propertyInfo, "Property ProductType does not exist in PantryItem class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property ProductType in PantryItem class is not of type string");
        }

        [Test]
        public void Backend_PantryItem_StockItem_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.PantryItem";
            Assembly assembly = Assembly.Load(assemblyName);
            Type pantryItemType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = pantryItemType.GetProperty("StockItem");
            Assert.IsNotNull(propertyInfo, "Property StockItem does not exist in PantryItem class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property StockItem in PantryItem class is not of type int");
        }

        [Test]
        public void Backend_PantryItem_Price_PropertyExists_ReturnExpectedDataTypes_decimal()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.PantryItem";
            Assembly assembly = Assembly.Load(assemblyName);
            Type pantryItemType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = pantryItemType.GetProperty("Price");
            Assert.IsNotNull(propertyInfo, "Property Price does not exist in PantryItem class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property Price in PantryItem class is not of type decimal");
        }

        [Test]
        public void Backend_PantryItem_ExpDate_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.PantryItem";
            Assembly assembly = Assembly.Load(assemblyName);
            Type pantryItemType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = pantryItemType.GetProperty("ExpDate");
            Assert.IsNotNull(propertyInfo, "Property ExpDate does not exist in PantryItem class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property ExpDate in PantryItem class is not of type string");
        }

        [Test]
        public void Test_PantryItemController_Class_Exists()
        {
            var _controllerType = typeof(PantryItemController);
            Assert.NotNull(_controllerType);
        }

        [Test]
        public void Test_GetAllPantryItems_Method_Exists()
        {
            var _controllerType = typeof(PantryItemController);
            var methodInfo = _controllerType.GetMethod("GetAllPantryItems");
            Assert.NotNull(methodInfo);
        }

        [Test]
        public void Test_GetAllPantryItems_Method_HasHttpGetAttribute()
        {
            var _controllerType = typeof(PantryItemController);
            var methodInfo = _controllerType.GetMethod("GetAllPantryItems");
            var httpGetAttribute = methodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true).FirstOrDefault();
            Assert.NotNull(httpGetAttribute);
        }

        [Test]
        public void Test_AddPantryItem_Method_Exists()
        {
            var _controllerType = typeof(PantryItemController);
            var methodInfo = _controllerType.GetMethod("AddPantryItem");
            Assert.NotNull(methodInfo);
        }

        [Test]
        public void Test_AddPantryItem_Method_HasHttpPostAttribute()
        {
            var _controllerType = typeof(PantryItemController);
            var methodInfo = _controllerType.GetMethod("AddPantryItem");
            var httpPostAttribute = methodInfo.GetCustomAttributes(typeof(HttpPostAttribute), true).FirstOrDefault();
            Assert.NotNull(httpPostAttribute);
        }

        [Test]
        public async Task Test_AddPantryItem_ReturnsBadRequest_WhenStockItemIsZero()
        {
            var newPantryItem = new PantryItem
            {
                ProductName = "Sample Product",
                ProductType = "Grocery",
                StockItem = 0, // Invalid stock item
                Price = 100,
                ExpDate = "2024-07-12"
            };

            var result = await _pantryItemController.AddPantryItem(newPantryItem);
            var badRequestResult = result.Result as BadRequestObjectResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
            Assert.AreEqual("Stock Item must be greater than 0.", badRequestResult.Value.GetType().GetProperty("message").GetValue(badRequestResult.Value));
        }
    }
}
