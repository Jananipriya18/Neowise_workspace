using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using dotnetapp.Controllers;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class PostTests
    {
        private Type _restaurantType;
        private Type _menuItemType;
        private Type _controllerType;
        private Assembly _assembly;
        private AppDbContext _context;
        private FoodController _foodController;

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;

            _context = new AppDbContext(dbContextOptions);
            _foodController = new FoodController(_context);

            // Seed data for testing
            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "Test Restaurant",
                Location = "123 Test St",
                PhoneNumber = "(123) 456-7890"
            };
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void TestRestaurant_ClassExists()
        {
            _assembly = Assembly.Load("dotnetapp");
            _restaurantType = _assembly.GetType("dotnetapp.Models.Restaurant");
            Assert.NotNull(_restaurantType, "Restaurant class does not exist.");
        }

        [Test]
        public void TestMenuItem_ClassExists()
        {
            _assembly = Assembly.Load("dotnetapp");
            _menuItemType = _assembly.GetType("dotnetapp.Models.MenuItem");
            Assert.NotNull(_menuItemType, "MenuItem class does not exist.");
        }

        [Test]
        public void TestAppDbContext_ClassExists_in_Models()
        {
            _assembly = Assembly.Load("dotnetapp");
            var contextType = _assembly.GetType("dotnetapp.Models.AppDbContext");
            Assert.NotNull(contextType, "AppDbContext class does not exist.");
        }

        [Test]
        public void AppDbContext_ContainsDbSet_Restaurant()
        {
            _assembly = Assembly.GetAssembly(typeof(AppDbContext));
            var contextType = _assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            Assert.NotNull(contextType, "No DbContext found in the assembly");

            var propertyInfo = contextType.GetProperty("Restaurants");
            Assert.NotNull(propertyInfo, "Restaurants property not found in the DbContext");
            Assert.AreEqual(typeof(DbSet<>).MakeGenericType(_restaurantType), propertyInfo.PropertyType);
        }

        [Test]
        public void AppDbContext_ContainsDbSet_MenuItem()
        {
            _assembly = Assembly.GetAssembly(typeof(AppDbContext));
            var contextType = _assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            Assert.NotNull(contextType, "No DbContext found in the assembly");

            var propertyInfo = contextType.GetProperty("MenuItems");
            Assert.NotNull(propertyInfo, "MenuItems property not found in the DbContext");
            Assert.AreEqual(typeof(DbSet<>).MakeGenericType(_menuItemType), propertyInfo.PropertyType);
        }

        [Test]
        public void TestMenuItemNamePropertyType()
        {
            _assembly = Assembly.Load("dotnetapp");
            _menuItemType = _assembly.GetType("dotnetapp.Models.MenuItem");
            var nameProperty = _menuItemType.GetProperty("Name");
            Assert.NotNull(nameProperty, "Name property does not exist.");
            Assert.AreEqual(typeof(string), nameProperty.PropertyType, "Name property should be of type string.");
        }

        [Test]
        public void TestMenuItemNamePropertyMaxLength100()
        {
            _assembly = Assembly.Load("dotnetapp");
            _menuItemType = _assembly.GetType("dotnetapp.Models.MenuItem");
            var nameProperty = _menuItemType.GetProperty("Name");
            var maxLengthAttribute = nameProperty.GetCustomAttribute<MaxLengthAttribute>();
            
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Name property.");
            Assert.AreEqual(100, maxLengthAttribute.Length, "Name property should have a max length of 100.");
        }

        [Test]
        public void TestRestaurantPhoneNumberRegularExpressionAttribute()
        {
            _assembly = Assembly.Load("dotnetapp");
            _restaurantType = _assembly.GetType("dotnetapp.Models.Restaurant");
            var phoneNumberProperty = _restaurantType.GetProperty("PhoneNumber");
            var regexAttribute = phoneNumberProperty.GetCustomAttribute<RegularExpressionAttribute>();

            Assert.NotNull(regexAttribute, "RegularExpression attribute not found on PhoneNumber property.");
            Assert.AreEqual(@"\(\d{3}\) \d{3}-\d{4}", regexAttribute.Pattern, "PhoneNumber property should have the correct regular expression pattern.");
        }

        [Test]
        public void Test_DisplayMenuItemsForRestaurant_Action()
        {
            _assembly = Assembly.Load("dotnetapp");
            _controllerType = _assembly.GetType("dotnetapp.Controllers.FoodController");
            var method = _controllerType.GetMethod("DisplayMenuItemsForRestaurant", new Type[] { typeof(int) });
            Assert.NotNull(method, "DisplayMenuItemsForRestaurant action does not exist.");
        }

        [Test]
        public void Test_AddMenuItem_Action()
        {
            var menuItem = new MenuItem
            {
                Name = "Test Item",
                Price = 9.99m,
                Description = "Test Description",
                RestaurantId = 1
            };

            var result = _foodController.AddMenuItem(menuItem) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.AreEqual(nameof(FoodController.DisplayMenuItemsForRestaurant), result.ActionName);
            Assert.AreEqual(menuItem.RestaurantId, result.RouteValues["restaurantId"]);
        }

        [Test]
        public void Test_DisplayAllRestaurants_Action()
        {
            _assembly = Assembly.Load("dotnetapp");
            _controllerType = _assembly.GetType("dotnetapp.Controllers.FoodController");
            var method = _controllerType.GetMethod("DisplayAllRestaurants");
            Assert.NotNull(method, "DisplayAllRestaurants action does not exist.");
        }

        [Test]
        public void Test_SearchRestaurantsByName_Action()
        {
            _assembly = Assembly.Load("dotnetapp");
            _controllerType = _assembly.GetType("dotnetapp.Controllers.FoodController");
            var method = _controllerType.GetMethod("SearchRestaurantsByName", new Type[] { typeof(string) });
            Assert.NotNull(method, "SearchRestaurantsByName action does not exist.");
        }
    }
}
