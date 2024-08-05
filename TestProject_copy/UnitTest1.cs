// using System;
// using System.Reflection;
// using NUnit.Framework;
// using dotnetapp.Models;
// using System.ComponentModel.DataAnnotations;
// using dotnetapp.Controllers;
// using System.Linq;
// using Moq;
// using Microsoft.EntityFrameworkCore;
// namespace dotnetapp.Tests
// {
//     [TestFixture]
//     public class PostTests
//     {
//         // private const string ViewsFolderPath = "Views";
//         // private const string PostViewsFolderPath = "D:\\Visual Studio\\W5_D1_S1_Client\\dotnetapp\\dotnetapp\\Views\\Post";
//         private Type _productType;
//         private Type controllerType;
//         private Type _viewType;
//         private Assembly _assembly1;
//         private string relativeFolderPath; // Set this to the relative path of the folder you want to check
//         private string fileName; 
//         private Mock<AppDbContext> _mockContext;
//         private AppDbContext _context;
//         private MovieRentalController _libraryController;
//         private DbContextOptions<AppDbContext> _dbContextOptions;


//         // private PostController _postcontroller;
//         // private List<Post> _fakePosts;


//         [SetUp]
//         public void Setup()
//         {
//             _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
//                 .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
//                 .Options;

//             var dbContext = new AppDbContext(_dbContextOptions);
//             _context = new AppDbContext(_dbContextOptions);

//             _libraryController = new MovieRentalController(dbContext);
           
//         }

//         [TearDown]
//         public void TearDown()
//         {
//             //_postcontroller = null;
//         }

//         private static MethodInfo GetMethod1(Type type, string methodName, Type[] parameterTypes)
//         {
//             return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance, null, parameterTypes, null);
//         }
        


//         [Test]
//         public void TestBook_ClassExists()
//         {
//             // Load the assembly at runtime
//             Assembly assembly = Assembly.Load("dotnetapp");
//             Type postType = assembly.GetType("dotnetapp.Models.Book");
//             Assert.NotNull(postType, "Book class does not exist.");
//         }
//         [Test]
//         public void TestLibraryCard_ClassExists()
//         {
//             // Load the assembly at runtime
//             Assembly assembly = Assembly.Load("dotnetapp");
//             Type postType = assembly.GetType("dotnetapp.Models.LibraryCard");
//             Assert.NotNull(postType, "LibraryCard class does not exist.");
//         }
//         [Test]
//         public void TestAppDbContext_ClassExists_in_Models()
//         {
//             // Load the assembly at runtime
//             Assembly assembly = Assembly.Load("dotnetapp");
//             Type postType = assembly.GetType("dotnetapp.Models.AppDbContext");
//             Assert.NotNull(postType, "AppDbContext class does not exist.");
//         }

//         // Test to check that AppDbContext Contains DbSet for model Book
//         [Test]
//         public void AppDbContext_ContainsDbSet_Book()
//         {
//             Assembly assembly = Assembly.GetAssembly(typeof(AppDbContext));
//             Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
//             if (contextType == null)
//             {
//                 Assert.Fail("No DbContext found in the assembly");
//                 return;
//             }
//             Type BookType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Book");
//             if (BookType == null)
//             {
//                 Assert.Fail("No DbSet found in the DbContext");
//                 return;
//             }
//             var propertyInfo = contextType.GetProperty("Books");
//             if (propertyInfo == null)
//             {
//                 Assert.Fail("Books property not found in the DbContext");
//                 return;
//             }
//             else
//             {
//                 Assert.AreEqual(typeof(DbSet<>).MakeGenericType(BookType), propertyInfo.PropertyType);
//             }
//         }

//         // Test to check that AppDbContext Contains DbSet for model LibraryCard
//         [Test]
//         public void AppDbContext_ContainsDbSet_LibraryCard()
//         {
//             Assembly assembly = Assembly.GetAssembly(typeof(AppDbContext));
//             Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
//             if (contextType == null)
//             {
//                 Assert.Fail("No DbContext found in the assembly");
//                 return;
//             }
//             Type LibraryCardType = assembly.GetTypes().FirstOrDefault(t => t.Name == "LibraryCard");
//             if (LibraryCardType == null)
//             {
//                 Assert.Fail("No DbSet found in the DbContext");
//                 return;
//             }
//             var propertyInfo = contextType.GetProperty("LibraryCards");
//             if (propertyInfo == null)
//             {
//                 Assert.Fail("LibraryCards property not found in the DbContext");
//                 return;
//             }
//             else
//             {
//                 Assert.AreEqual(typeof(DbSet<>).MakeGenericType(LibraryCardType), propertyInfo.PropertyType);
//             }
//         }

//         [Test]
//         public void TestTitlePropertyType()
//         {
//             Assembly assembly = Assembly.Load("dotnetapp");
//             _productType = assembly.GetType("dotnetapp.Models.Book");
//             PropertyInfo UnitPriceProperty = _productType.GetProperty("Title");
//             Assert.NotNull(UnitPriceProperty, "Title property does not exist.");
//             Assert.AreEqual(typeof(string), UnitPriceProperty.PropertyType, "Title property should be of type String.");
//         }

//         [Test]
//         public void TestTitlePropertyMaxLength100()
//         {
//             Assembly assembly = Assembly.Load("dotnetapp");
//             _productType = assembly.GetType("dotnetapp.Models.Book");
//             PropertyInfo titleProperty = _productType.GetProperty("Title");
//             var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            
//             Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Title property.");
//             Assert.AreEqual(100, maxLengthAttribute.Length, "Title property should have a max length of 100.");
//         }

//         [Test]
//         public void TestAuthorPropertyMaxLength50()
//         {            
//             Assembly assembly = Assembly.Load("dotnetapp");
//             _productType = assembly.GetType("dotnetapp.Models.Book");
//             PropertyInfo titleProperty = _productType.GetProperty("Author");
//             var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            
//             Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Author property.");
//             Assert.AreEqual(50, maxLengthAttribute.Length, "Author property should have a max length of 50.");
//         }

//         [Test]
//         public void TestPublishedYearPropertyRange()
//         {
//             Assembly assembly = Assembly.Load("dotnetapp");
//             _productType = assembly.GetType("dotnetapp.Models.Book");
//             PropertyInfo publishedYearProperty = _productType.GetProperty("PublishedYear");
//             var rangeAttribute = publishedYearProperty.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
            
//             Assert.NotNull(rangeAttribute, "Range attribute not found on PublishedYear property.");
//             Assert.AreEqual(1000, rangeAttribute.Minimum, "PublishedYear property should have a minimum value of 1000.");
//             Assert.AreEqual(2024, rangeAttribute.Maximum, "PublishedYear property should have a maximum value of 2024");
//         }

//         [Test]
//         public void TestCardNumberPropertyRegularExpressionAttribute()
//         {
//             Assembly assembly = Assembly.Load("dotnetapp");
//             _productType = assembly.GetType("dotnetapp.Models.LibraryCard");            
//             PropertyInfo cardNumberProperty = _productType.GetProperty("CardNumber");
//             var regexAttribute = cardNumberProperty.GetCustomAttribute<RegularExpressionAttribute>();

//             Assert.NotNull(regexAttribute, "RegularExpression attribute not found on CardNumber property.");
//             Assert.AreEqual(@"LC-\d{5}", regexAttribute.Pattern, "CardNumber property should have the correct regular expression pattern.");
//         }

//         [Test]
//         public void TestMemberNamePropertyMaxLength100()
//         {
//             Assembly assembly = Assembly.Load("dotnetapp");
//             _productType = assembly.GetType("dotnetapp.Models.LibraryCard");
//             PropertyInfo titleProperty = _productType.GetProperty("MemberName");
//             var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            
//             Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on MemberName property.");
//             Assert.AreEqual(100, maxLengthAttribute.Length, "MemberName property should have a max length of 100.");
//         }

//         [Test]
//         public void TestMigrationExists()
//         {
//             bool viewsFolderExists = Directory.Exists(@"/home/coder/project/workspace/dotnetapp/Migrations");

//             Assert.IsTrue(viewsFolderExists, "Migrations does not exist.");
//         }

//         [Test]
//         public void Test_DisplayAllBooks_Action()
//         {
//             Assembly assembly = Assembly.Load("dotnetapp");
//             controllerType = assembly.GetType("dotnetapp.Controllers.MovieRentalController");
//             var detailsMethod = GetMethod1(controllerType, "DisplayAllBooks", new Type[] {  });

//             Assert.NotNull(detailsMethod);
//         }

//         [Test]
//         public void Test_SearchBooksByTitle_Action()
//         {
//             Assembly assembly = Assembly.Load("dotnetapp");
//             Type controllerType = assembly.GetType("dotnetapp.Controllers.MovieRentalController");
//             var searchBooksByTitleMethod = GetMethod1(controllerType, "SearchBooksByTitle", new Type[] { typeof(string) });

//             Assert.NotNull(searchBooksByTitleMethod);
//         }

//         [Test]
//         public void Test_DisplayBooksForLibraryCard_Action()
//         {
//             Assembly assembly = Assembly.Load("dotnetapp");
//             Type controllerType = assembly.GetType("dotnetapp.Controllers.MovieRentalController");
//             var searchBooksByTitleMethod = GetMethod1(controllerType, "DisplayBooksForLibraryCard", new Type[] { typeof(int) });

//             Assert.NotNull(searchBooksByTitleMethod);
//         }

//         // [Test]
//         // public void Test_GetAvailableBooks_Action()
//         // {
//         //     Assembly assembly = Assembly.Load("dotnetapp");
//         //     controllerType = assembly.GetType("dotnetapp.Controllers.MovieRentalController");
//         //     var detailsMethod = GetMethod1(controllerType, "GetAvailableBooks", new Type[] {  });

//         //     Assert.NotNull(detailsMethod);
//         // }

//         [Test]
//         public void SearchBooksByTitle_ShouldReturnWithCorrectModel()
//         {
//             // Arrange
//             Assembly assembly = Assembly.Load("dotnetapp");
//             Type controllerType = assembly.GetType("dotnetapp.Controllers.MovieRentalController");
//             var controller = Activator.CreateInstance(controllerType, _context);
//             MethodInfo method = controllerType.GetMethod("SearchBooksByTitle", new Type[] { typeof(string) });
//             var result = method.Invoke(controller, new object[] { "demo" });
//             Assert.NotNull(result);
//         }

// [Test]
//         public void Test_AddBook_Action()
//         {
//             // Arrange
//             var book = new dotnetapp.Models.Book()
//             {
//                 Title = "Sample Book",
//                 Author = "Sample Author",
//                 PublishedYear = 2020
//             };

//             // Act
//             var result = _libraryController.AddBook(book);

//             // Assert
//             Assert.NotNull(result); // Assuming your AddBook method returns something meaningful, adjust this assertion accordingly
//         }



//     }
// }

using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using dotnetapp.Models;
using dotnetapp.Controllers;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class MovieRentalControllerTests
    {
        private Type _movieType;
        private Type _controllerType;
        private Assembly _assembly;
        private MovieRentalController _movieController;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;

            _context = new ApplicationDbContext(_dbContextOptions);
            _movieController = new MovieRentalController(_context);

            // Seed some initial data
            _context.Customers.Add(new Customer
            {
                Id = 1,
                Name = "Alice Johnson",
                Email = "alice.johnson@example.com",
                PhoneNumber = "123-456-7890"
            });
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        private static MethodInfo GetMethod(Type type, string methodName, Type[] parameterTypes)
        {
            return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance, null, parameterTypes, null);
        }

        [Test]
        public void TestCustomer_ClassExists()
        {
            _assembly = Assembly.Load("dotnetapp");
            Type customerType = _assembly.GetType("dotnetapp.Models.Customer");
            Assert.NotNull(customerType, "Customer class does not exist.");
        }

        [Test]
        public void TestMovie_ClassExists()
        {
            _assembly = Assembly.Load("dotnetapp");
            Type movieType = _assembly.GetType("dotnetapp.Models.Movie");
            Assert.NotNull(movieType, "Movie class does not exist.");
        }

        [Test]
        public void TestAppDbContext_ClassExists()
        {
            _assembly = Assembly.Load("dotnetapp");
            Type dbContextType = _assembly.GetType("dotnetapp.Models.ApplicationDbContext");
            Assert.NotNull(dbContextType, "ApplicationDbContext class does not exist.");
        }

        [Test]
        public void AppDbContext_ContainsDbSet_Movie()
        {
            var contextType = typeof(ApplicationDbContext);
            var propertyInfo = contextType.GetProperty("Movies");
            Assert.NotNull(propertyInfo, "Movies property not found in the DbContext");
            Assert.AreEqual(typeof(DbSet<Movie>), propertyInfo.PropertyType);
        }

        [Test]
        public void TestTitlePropertyType()
        {
            _assembly = Assembly.Load("dotnetapp");
            _movieType = _assembly.GetType("dotnetapp.Models.Movie");
            PropertyInfo titleProperty = _movieType.GetProperty("Title");
            Assert.NotNull(titleProperty, "Title property does not exist.");
            Assert.AreEqual(typeof(string), titleProperty.PropertyType, "Title property should be of type String.");
        }

        [Test]
        public void TestTitlePropertyMaxLength100()
        {
            _assembly = Assembly.Load("dotnetapp");
            _movieType = _assembly.GetType("dotnetapp.Models.Movie");
            PropertyInfo titleProperty = _movieType.GetProperty("Title");
            var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Title property.");
            Assert.AreEqual(100, maxLengthAttribute.Length, "Title property should have a max length of 100.");
        }

        [Test]
        public void TestDirectorPropertyMaxLength50()
        {
            _assembly = Assembly.Load("dotnetapp");
            _movieType = _assembly.GetType("dotnetapp.Models.Movie");
            PropertyInfo directorProperty = _movieType.GetProperty("Director");
            var maxLengthAttribute = directorProperty.GetCustomAttribute<MaxLengthAttribute>();
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Director property.");
            Assert.AreEqual(50, maxLengthAttribute.Length, "Director property should have a max length of 50.");
        }

        [Test]
        public void TestReleaseYearPropertyRange()
        {
            _assembly = Assembly.Load("dotnetapp");
            _movieType = _assembly.GetType("dotnetapp.Models.Movie");
            PropertyInfo releaseYearProperty = _movieType.GetProperty("ReleaseYear");
            var rangeAttribute = releaseYearProperty.GetCustomAttribute<RangeAttribute>();
            Assert.NotNull(rangeAttribute, "Range attribute not found on ReleaseYear property.");
            Assert.AreEqual(1900, rangeAttribute.Minimum, "ReleaseYear property should have a minimum value of 1900.");
            Assert.AreEqual(2024, rangeAttribute.Maximum, "ReleaseYear property should have a maximum value of 2024.");
        }

        [Test]
        public void Test_DisplayAllMovies_Action()
        {
            _assembly = Assembly.Load("dotnetapp");
            _controllerType = _assembly.GetType("dotnetapp.Controllers.MovieRentalController");
            var method = GetMethod(_controllerType, "DisplayAllMovies", new Type[] { });
            Assert.NotNull(method);
        }

        [Test]
        public void Test_SearchMoviesByTitle_Action()
        {
            _assembly = Assembly.Load("dotnetapp");
            _controllerType = _assembly.GetType("dotnetapp.Controllers.MovieRentalController");
            var method = GetMethod(_controllerType, "SearchMoviesByTitle", new Type[] { typeof(string) });
            Assert.NotNull(method);
        }

        [Test]
        public void Test_DisplayMoviesForCustomer_Action()
        {
            _assembly = Assembly.Load("dotnetapp");
            _controllerType = _assembly.GetType("dotnetapp.Controllers.MovieRentalController");
            var method = GetMethod(_controllerType, "DisplayMoviesForCustomer", new Type[] { typeof(int) });
            Assert.NotNull(method);
        }

        [Test]
        public async Task SearchMoviesByTitle_ShouldReturnCorrectModel()
        {
            // Arrange
            var movie = new Movie()
            {
                Title = "Inception",
                Director = "Christopher Nolan",
                ReleaseYear = 2010,
                CustomerId = 1
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            // Act
            var result = await _movieController.SearchMoviesByTitle("Inception") as OkObjectResult;
            var movies = result?.Value as IEnumerable<Movie>;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, movies?.Count());
            Assert.AreEqual("Inception", movies?.First().Title);
        }

        [Test]
        public async Task Test_AddMovie_Action()
        {
            // Arrange
            var movie = new Movie()
            {
                Title = "Sample Movie",
                Director = "Sample Director",
                ReleaseYear = 2024,
                CustomerId = 1
            };

            // Act
            var result = await _movieController.AddMovie(movie) as CreatedAtActionResult;
            var addedMovie = result?.Value as Movie;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("AddMovie", result?.ActionName);
            Assert.AreEqual(movie.Title, addedMovie?.Title);
            Assert.AreEqual(movie.Director, addedMovie?.Director);
        }
    }
}
