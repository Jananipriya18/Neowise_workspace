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
    public class PostTests
    {
        // private const string ViewsFolderPath = "Views";
        // private const string PostViewsFolderPath = "D:\\Visual Studio\\W5_D1_S1_Client\\dotnetapp\\dotnetapp\\Views\\Post";
        private Type _productType;
        private Type controllerType;
        private Type _viewType;
        private Assembly _assembly1;
        private string relativeFolderPath; // Set this to the relative path of the folder you want to check
        private string fileName; 
        private Mock<AppDbContext> _mockContext;
        private AppDbContext _context;
        private MovieRentalController _movieController;
        private DbContextOptions<AppDbContext> _dbContextOptions;


        // private PostController _postcontroller;
        // private List<Post> _fakePosts;


        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;

            var dbContext = new AppDbContext(_dbContextOptions);
            _context = new AppDbContext(_dbContextOptions);

            _movieController = new MovieRentalController(dbContext);
           
        }

        [TearDown]
        public void TearDown()
        {
            //_postcontroller = null;
        }

        private static MethodInfo GetMethod1(Type type, string methodName, Type[] parameterTypes)
        {
            return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance, null, parameterTypes, null);
        }
        


        [Test]
        public void TestMovie_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type postType = assembly.GetType("dotnetapp.Models.Movie");
            Assert.NotNull(postType, "Movie class does not exist.");
        }
        [Test]
        public void TestCustomer_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type postType = assembly.GetType("dotnetapp.Models.Customer");
            Assert.NotNull(postType, "Customer class does not exist.");
        }
        [Test]
        public void TestAppDbContext_ClassExists_in_Models()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type postType = assembly.GetType("dotnetapp.Models.AppDbContext");
            Assert.NotNull(postType, "AppDbContext class does not exist.");
        }

        // Test to check that AppDbContext Contains DbSet for model Movie
        [Test]
        public void AppDbContext_ContainsDbSet_Movie()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(AppDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type MovieType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Movie");
            if (MovieType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Movies");
            if (propertyInfo == null)
            {
                Assert.Fail("Movies property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(MovieType), propertyInfo.PropertyType);
            }
        }

        // Test to check that AppDbContext Contains DbSet for model Customer
        [Test]
        public void AppDbContext_ContainsDbSet_Customer()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(AppDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type CustomerType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Customer");
            if (CustomerType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Customers");
            if (propertyInfo == null)
            {
                Assert.Fail("Customers property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(CustomerType), propertyInfo.PropertyType);
            }
        }

        [Test]
        public void TestTitlePropertyType()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Movie");
            PropertyInfo UnitPriceProperty = _productType.GetProperty("Title");
            Assert.NotNull(UnitPriceProperty, "Title property does not exist.");
            Assert.AreEqual(typeof(string), UnitPriceProperty.PropertyType, "Title property should be of type String.");
        }

        [Test]
        public void TestTitlePropertyMaxLength100()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Movie");
            PropertyInfo titleProperty = _productType.GetProperty("Title");
            var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Title property.");
            Assert.AreEqual(100, maxLengthAttribute.Length, "Title property should have a max length of 100.");
        }

        [Test]
        public void TestDirectorPropertyMaxLength50()
        {            
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Movie");
            PropertyInfo titleProperty = _productType.GetProperty("Director");
            var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Director property.");
            Assert.AreEqual(50, maxLengthAttribute.Length, "Director property should have a max length of 50.");
        }

        [Test]
        public void TestReleaseYearPropertyRange()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Movie");
            PropertyInfo publishedYearProperty = _productType.GetProperty("ReleaseYear");
            var rangeAttribute = publishedYearProperty.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
            
            Assert.NotNull(rangeAttribute, "Range attribute not found on ReleaseYear property.");
            Assert.AreEqual(1900, rangeAttribute.Minimum, "ReleaseYear property should have a minimum value of 1900.");
            Assert.AreEqual(2024, rangeAttribute.Maximum, "ReleaseYear property should have a maximum value of 2024");
        }

        [Test]
        public void TestNamePropertyRegularExpressionAttribute()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Customer");            
            PropertyInfo cardNumberProperty = _productType.GetProperty("Name");
            var regexAttribute = cardNumberProperty.GetCustomAttribute<RegularExpressionAttribute>();

            Assert.NotNull(regexAttribute, "RegularExpression attribute not found on Name property.");
            Assert.AreEqual(@"LC-\d{5}", regexAttribute.Pattern, "Name property should have the correct regular expression pattern.");
        }

        [Test]
        public void TestEmailPropertyMaxLength100()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Customer");
            PropertyInfo titleProperty = _productType.GetProperty("Email");
            var requiredAttribute = titleProperty.GetCustomAttribute<RequiredAttribute>();
            
            Assert.NotNull(requiredAttribute, "<RequiredAttribute>() attribute not found on Email property.");
            
        }

        [Test]
        public void TestPhoneNumberPropertyMaxLength10()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Customer");
            PropertyInfo titleProperty = _productType.GetProperty("PhoneNumber");
            var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on PhoneNumber property.");
            Assert.AreEqual(10, maxLengthAttribute.Length, "PhoneNumber property should have a max length of 10.");
        }

        [Test]
        public void TestMigrationExists()
        {
            bool viewsFolderExists = Directory.Exists(@"/home/coder/project/workspace/dotnetapp/Migrations");

            Assert.IsTrue(viewsFolderExists, "Migrations does not exist.");
        }

        [Test]
        public void Test_DisplayAllMovies_Action()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            controllerType = assembly.GetType("dotnetapp.Controllers.MovieRentalController");
            var detailsMethod = GetMethod1(controllerType, "DisplayAllMovies", new Type[] {  });

            Assert.NotNull(detailsMethod);
        }

        [Test]
        public void Test_SearchMoviesByTitle_Action()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            Type controllerType = assembly.GetType("dotnetapp.Controllers.MovieRentalController");
            var searchMoviesByTitleMethod = GetMethod1(controllerType, "SearchMoviesByTitle", new Type[] { typeof(string) });

            Assert.NotNull(searchMoviesByTitleMethod);
        }

        [Test]
        public void Test_DisplayMoviesForCustomer_Action()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            Type controllerType = assembly.GetType("dotnetapp.Controllers.MovieRentalController");
            var searchMoviesByTitleMethod = GetMethod1(controllerType, "DisplayMoviesForCustomer", new Type[] { typeof(int) });

            Assert.NotNull(searchMoviesByTitleMethod);
        }

        [Test]
        public void Test_GetAvailableMovies_Action()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            controllerType = assembly.GetType("dotnetapp.Controllers.MovieRentalController");
            var detailsMethod = GetMethod1(controllerType, "GetAvailableMovies", new Type[] {  });

            Assert.NotNull(detailsMethod);
        }

        [Test]
        public void SearchMoviesByTitle_ShouldReturnWithCorrectModel()
        {
            // Arrange
            Assembly assembly = Assembly.Load("dotnetapp");
            Type controllerType = assembly.GetType("dotnetapp.Controllers.MovieRentalController");
            var controller = Activator.CreateInstance(controllerType, _context);
            MethodInfo method = controllerType.GetMethod("SearchMoviesByTitle", new Type[] { typeof(string) });
            var result = method.Invoke(controller, new object[] { "demo" });
            Assert.NotNull(result);
        }

[Test]
        public void Test_AddMovie_Action()
        {
            // Arrange
            var movie = new dotnetapp.Models.Movie()
            {
                Title = "Sample Movie",
                Director = "Sample Director",
                ReleaseYear = 2020
            };

            // Act
            var result = _movieController.AddMovie(movie);

            // Assert
            Assert.NotNull(result); // Assuming your AddMovie method returns something meaningful, adjust this assertion accordingly
        }



    }
}
