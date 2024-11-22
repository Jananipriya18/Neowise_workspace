using System.Numerics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Microsoft.Data.SqlClient;
using System;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;


namespace TestProject
{

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
        public void Product_Models_ClassExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Product";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductType = assembly.GetType(typeName);
            Assert.IsNotNull(ProductType);
        }

        // Test to Check Product Models Property Id Exists with correcct datatype int    
        [Test]
        public void Product_Id_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Product";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = ProductType.GetProperty("Id");
            Assert.IsNotNull(propertyInfo, "Property Id does not exist in Product class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property Id in Product class is not of type int");
        }

        [Test]
        public void Product_Name_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Product";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = ProductType.GetProperty("Name");
            Assert.IsNotNull(propertyInfo, "Property Name does not exist in Product class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Name in Product class is not of type string");
        }
        [Test]
        public void Product_Description_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Product";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = ProductType.GetProperty("Description");
            Assert.IsNotNull(propertyInfo, "Property Name does not exist in Product class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Description in Product class is not of type string");
        }

        [Test]
        public void Product_Category_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Product";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = ProductType.GetProperty("Category");
            Assert.IsNotNull(propertyInfo, "Property Name does not exist in Product class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Category in Product class is not of type string");
        }

        [Test]
        public void Product_Price_PropertyExists_ReturnExpectedDataTypes_decimal()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Product";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = ProductType.GetProperty("Price");
            Assert.IsNotNull(propertyInfo, "Property Name does not exist in Product class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(decimal), expectedType, "Property Price in Product class is not of type decimal");
        }

        [Test]
        public void Product_StockQuantity_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Product";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = ProductType.GetProperty("StockQuantity");
            Assert.IsNotNull(propertyInfo, "Property StockQuantity does not exist in Product class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property StockQuantity in Product class is not of type int");
        }

        [Test]
        public void Product_ExpiryDate_PropertyExists_ReturnExpectedDataTypes_DateTime()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Product";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = ProductType.GetProperty("ExpiryDate");
            Assert.IsNotNull(propertyInfo, "Property ExpiryDate does not exist in Product class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(DateTime), expectedType, "Property ExpiryDate in Product class is not of type DateTime");
        }

        [Test]
        public void ProductController_Controllers_ClassExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.ProductController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductControllerType = assembly.GetType(typeName);
            Assert.IsNotNull(ProductControllerType);
        }

        [Test]
        public void ProductController_View_MethodExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.ProductController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = ProductControllerType.GetMethod("View");
            Assert.IsNotNull(methodInfo, "Method View does not exist in ProductController class");
        }

        [Test]
        public void ProductController_ViewProducts_MethodReturns_IActionResult()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.ProductController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = ProductControllerType.GetMethod("ViewProducts");
            Assert.AreEqual(typeof(IActionResult), methodInfo.ReturnType, "Method ViewProducts in ProductController class is not of type IActionResult");
        }

        [Test]
        public void ProductController_Create_MethodExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.ProductController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = ProductControllerType.GetMethod("Create", Type.EmptyTypes);
            Assert.IsNotNull(methodInfo, "Method Create does not exist in ProductController class");
        }

        [Test]
        public void ProductController_Create_Method_with_NoParams_Returns_IActionResult()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.ProductController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type ProductControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = ProductControllerType.GetMethod("Create", Type.EmptyTypes);
            Assert.AreEqual(typeof(IActionResult), methodInfo.ReturnType, "Method Create in ProductController class is not of type IActionResult");
        }

        [Test]
        public void Create_in_ProductController_Add_new_Product_to_DB()
        {
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string modelTypeName = "dotnetapp.Models.Product";
            string controllerTypeName = "dotnetapp.Controllers.ProductController";
            Type controllerType = assembly.GetType(controllerTypeName);
            Type modelType = assembly.GetType(modelTypeName);

            // Prepare data for creating a new product
            var productData = new Dictionary<string, object>
            {
                { "Name", "Sample Product" },
                { "Description", "Sample Description" },
                { "Category", "Sample Category" },
                { "Price", 10.99m },
                { "StockQuantity", 100 },
                { "ExpiryDate", DateTime.Now.AddDays(30) }
            };

            var product = new Product();
            foreach (var kvp in productData)
            {
                var propertyInfo = modelType.GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(product, kvp.Value);
                }
            }

            // Invoke the Create method in the ProductController
            MethodInfo method = controllerType.GetMethod("Create", new[] { modelType });
            if (method != null)
            {
                // Create DbContextOptions using DbContextOptionsBuilder
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase");
                var options = optionsBuilder.Options;

                var dbContextInstance = new ApplicationDbContext(options);
                var controller = Activator.CreateInstance(controllerType, dbContextInstance);
                var result = method.Invoke(controller, new object[] { product });

                // Validate the result
                var savedProduct = dbContextInstance.Products.FirstOrDefault(p => p.Name == "Sample Product");
                Assert.IsNotNull(savedProduct);
                Assert.AreEqual("Sample Description", savedProduct.Description);
                // Add more assertions as needed

                // Optionally, clean up by deleting the added product from the database
                dbContextInstance.Products.Remove(savedProduct);
                dbContextInstance.SaveChanges();
            }
            else
            {
                Assert.Fail("Create method not found in ProductController");
            }
        }


}
}