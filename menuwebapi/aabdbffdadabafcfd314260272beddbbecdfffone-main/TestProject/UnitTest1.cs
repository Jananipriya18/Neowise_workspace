using System;
using System.Reflection;
using NUnit.Framework;
using dotnetapp.Models;
using System.ComponentModel.DataAnnotations;
using dotnetapp.Controllers;
using System.Linq;
using Moq;
using Microsoft.EntityFrameworkCore;
namespace dotnetapp.Tests
{
    [TestFixture]
    public class FoodControllerTests
    {
        private Type _restaurantType;
        private Type _menuItemType;
        private Type _controllerType;
        private Assembly _assembly;
        private Mock<AppDbContext> _mockContext;
        private AppDbContext _context;
        private FoodController _foodController;
        private DbContextOptions<AppDbContext> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;

            var dbContext = new AppDbContext(_dbContextOptions);
            _context = new AppDbContext(_dbContextOptions);

            _foodController = new FoodController(dbContext);
        }

        private static MethodInfo GetMethod(Type type, string methodName, Type[] parameterTypes)
        {
            return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance, null, parameterTypes, null);
        }

        [Test]
        public void TestRestaurant_ClassExists()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            Type restaurantType = assembly.GetType("dotnetapp.Models.Restaurant");
            Assert.NotNull(restaurantType, "Restaurant class does not exist.");
        }

        [Test]
        public void TestMenuItem_ClassExists()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            Type menuItemType = assembly.GetType("dotnetapp.Models.MenuItem");
            Assert.NotNull(menuItemType, "MenuItem class does not exist.");
        }

        [Test]
        public void TestAppDbContext_ClassExists_in_Models()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            Type appDbContextType = assembly.GetType("dotnetapp.Models.AppDbContext");
            Assert.NotNull(appDbContextType, "AppDbContext class does not exist.");
        }

        [Test]
        public void AppDbContext_ContainsDbSet_Restaurant()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(AppDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type restaurantType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Restaurant");
            if (restaurantType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Restaurants");
            if (propertyInfo == null)
            {
                Assert.Fail("Restaurants property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(restaurantType), propertyInfo.PropertyType);
            }
        }

        [Test]
        public void AppDbContext_ContainsDbSet_MenuItem()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(AppDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type menuItemType = assembly.GetTypes().FirstOrDefault(t => t.Name == "MenuItem");
            if (menuItemType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("MenuItems");
            if (propertyInfo == null)
            {
                Assert.Fail("MenuItems property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(menuItemType), propertyInfo.PropertyType);
            }
        }

        [Test]
        public void TestRestaurantNamePropertyMaxLength100()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _restaurantType = assembly.GetType("dotnetapp.Models.Restaurant");
            PropertyInfo nameProperty = _restaurantType.GetProperty("Name");
            var maxLengthAttribute = nameProperty.GetCustomAttribute<MaxLengthAttribute>();
            
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Name property.");
            Assert.AreEqual(100, maxLengthAttribute.Length, "Name property should have a max length of 100.");
        }

        [Test]
        public void TestMenuItemNamePropertyMaxLength100()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _menuItemType = assembly.GetType("dotnetapp.Models.MenuItem");
            PropertyInfo nameProperty = _menuItemType.GetProperty("Name");
            var maxLengthAttribute = nameProperty.GetCustomAttribute<MaxLengthAttribute>();
            
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Name property.");
            Assert.AreEqual(100, maxLengthAttribute.Length, "Name property should have a max length of 100.");
        }

        [Test]
        public void TestMenuItemPricePropertyRange()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _menuItemType = assembly.GetType("dotnetapp.Models.MenuItem");
            PropertyInfo priceProperty = _menuItemType.GetProperty("Price");
            var rangeAttribute = priceProperty.GetCustomAttribute<RangeAttribute>();
            
            Assert.NotNull(rangeAttribute, "Range attribute not found on Price property.");
            Assert.AreEqual(0.01, rangeAttribute.Minimum, "Price property should have a minimum value of 0.01.");
            Assert.AreEqual(1000.00, rangeAttribute.Maximum, "Price property should have a maximum value of 1000.00");
        }

        [Test]
        public void TestRestaurantPhoneNumberPropertyRegularExpressionAttribute()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _restaurantType = assembly.GetType("dotnetapp.Models.Restaurant");            
            PropertyInfo phoneNumberProperty = _restaurantType.GetProperty("PhoneNumber");
            var regexAttribute = phoneNumberProperty.GetCustomAttribute<RegularExpressionAttribute>();

            Assert.NotNull(regexAttribute, "RegularExpression attribute not found on PhoneNumber property.");
            Assert.AreEqual(@"^\d{10}$", regexAttribute.Pattern, "PhoneNumber property should have the correct regular expression pattern.");
        }

        [Test]
        public void TestMigrationExists()
        {
            bool migrationsFolderExists = Directory.Exists(@"/home/coder/project/workspace/dotnetapp/Migrations");

            Assert.IsTrue(migrationsFolderExists, "Migrations folder does not exist.");
        }

        [Test]
        public void Test_GetAllRestaurants_Action()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _controllerType = assembly.GetType("dotnetapp.Controllers.FoodController");
            var method = GetMethod(_controllerType, "GetAllRestaurants", new Type[] { });

            Assert.NotNull(method, "GetAllRestaurants action does not exist.");
        }

        [Test]
        public void Test_GetMenuItemsByRestaurant_Action()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _controllerType = assembly.GetType("dotnetapp.Controllers.FoodController");
            var method = GetMethod(_controllerType, "GetMenuItemsByRestaurant", new Type[] { typeof(int) });

            Assert.NotNull(method, "GetMenuItemsByRestaurant action does not exist.");
        }

        [Test]
        public void Test_AddRestaurant_Action()
        {
            var restaurant = new Restaurant()
            {
                Name = "Sample Restaurant",
                PhoneNumber = "1234567890",
                Location = "Demo"
            };

            var result = _foodController.AddMenuItem(restaurant);

            Assert.NotNull(result, "AddMenuItem action did not return a result.");
        }

        [Test]
        public void Test_AddMenuItem_Action()
        {
            var menuItem = new MenuItem()
            {
                Name = "Sample MenuItem",
                Price = 9.99m,
                RestaurantId = 1
            };

            var result = _foodController.AddMenuItem(menuItem);

            Assert.NotNull(result, "AddMenuItem action did not return a result.");
        }
    }
}
