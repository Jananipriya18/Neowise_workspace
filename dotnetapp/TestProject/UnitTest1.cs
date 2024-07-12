using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Text;
using dotnetapp.Models;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class ShopTests
    {
        private Type _shopType;
        private PropertyInfo[] _shopProperties; 
        private Type _controllerType;
        
        private ApplicationDbContext _context;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _shopType = new Shop().GetType();
            _shopProperties = _shopType.GetProperties();
            _controllerType = typeof(ShopController);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri("http://localhost:8080/");
        }

        [Test]
        public async Task Test_GetAllShopItems_ReturnsSuccess()
        {
            HttpResponseMessage response = await _client.GetAsync("api/Shop/getAllShopitem");
            if ((int)response.StatusCode == 200)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            else
            {
                Assert.Fail();
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseBody);
        }

        [Test]
        public async Task Test_AddShopItem_ReturnsSuccess()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/Shop/addShopitem");
            request.Content = new StringContent("{\"productName\": \"Sample Product\",\"productType\": \"Electronics\",\"stockItem\": 10,\"price\": 100,\"mfDate\": \"2024-07-12\",\"companyName\": \"Sample Company\"}",
                Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.SendAsync(request);

            if ((int)response.StatusCode == 201)
            {
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            }
            else
            {
                Assert.Fail();
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(responseBody);
        }

        [Test]
        public void Test_Shop_Class_Exists()
        {
            Assert.NotNull(_shopType);
        }

        [Test]
        public void Test_Shop_Id_Property_DataType()
        {
            var idProperty = _shopProperties.FirstOrDefault(prop => prop.Name == "Id");
            Assert.NotNull(idProperty);
            Assert.AreEqual(typeof(int), idProperty.PropertyType);
        }

        [Test]
        public void Test_Shop_ProductName_Property_DataType()
        {
            var productNameProperty = _shopProperties.FirstOrDefault(prop => prop.Name == "ProductName");
            Assert.NotNull(productNameProperty);
            Assert.AreEqual(typeof(string), productNameProperty.PropertyType);
        }

        [Test]
        public void Test_Shop_ProductType_Property_DataType()
        {
            var productTypeProperty = _shopProperties.FirstOrDefault(prop => prop.Name == "ProductType");
            Assert.NotNull(productTypeProperty);
            Assert.AreEqual(typeof(string), productTypeProperty.PropertyType);
        }

        [Test]
        public void Test_Shop_StockItem_Property_DataType()
        {
            var stockItemProperty = _shopProperties.FirstOrDefault(prop => prop.Name == "StockItem");
            Assert.NotNull(stockItemProperty);
            Assert.AreEqual(typeof(int), stockItemProperty.PropertyType);
        }

        [Test]
        public void Test_Shop_Price_Property_DataType()
        {
            var priceProperty = _shopProperties.FirstOrDefault(prop => prop.Name == "Price");
            Assert.NotNull(priceProperty);
            Assert.AreEqual(typeof(int), priceProperty.PropertyType);
        }

        [Test]
        public void Test_Shop_MfDate_Property_DataType()
        {
            var mfDateProperty = _shopProperties.FirstOrDefault(prop => prop.Name == "MfDate");
            Assert.NotNull(mfDateProperty);
            Assert.AreEqual(typeof(string), mfDateProperty.PropertyType);
        }

        [Test]
        public void Test_Shop_CompanyName_Property_DataType()
        {
            var companyNameProperty = _shopProperties.FirstOrDefault(prop => prop.Name == "CompanyName");
            Assert.NotNull(companyNameProperty);
            Assert.AreEqual(typeof(string), companyNameProperty.PropertyType);
        }

        [Test]
        public void Test_ShopController_Class_Exists()
        {
            Assert.NotNull(_controllerType);
        }

        [Test]
        public void Test_GetAllShopItems_Method_Exists()
        {
            var methodInfo = _controllerType.GetMethod("GetAllShopItems");
            Assert.NotNull(methodInfo);
        }

        [Test]
        public void Test_GetAllShopItems_Method_HasHttpGetAttribute()
        {
            var methodInfo = _controllerType.GetMethod("GetAllShopItems");
            var httpGetAttribute = methodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true).FirstOrDefault();
            Assert.NotNull(httpGetAttribute);
        }

        [Test]
        public void Test_AddShopItem_Method_Exists()
        {
            var methodInfo = _controllerType.GetMethod("AddShopItem");
            Assert.NotNull(methodInfo);
        }

        [Test]
        public void Test_AddShopItem_Method_HasHttpPostAttribute()
        {
            var methodInfo = _controllerType.GetMethod("AddShopItem");
            var httpPostAttribute = methodInfo.GetCustomAttributes(typeof(HttpPostAttribute), true).FirstOrDefault();
            Assert.NotNull(httpPostAttribute);
        }
    }
}
