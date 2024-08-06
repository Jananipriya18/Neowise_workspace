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
        private MusicController _musicController;
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

            _musicController = new MusicController(dbContext);
           
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
        public void TestSong_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type postType = assembly.GetType("dotnetapp.Models.Song");
            Assert.NotNull(postType, "Song class does not exist.");
        }
        [Test]
        public void TestPlaylist_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type postType = assembly.GetType("dotnetapp.Models.Playlist");
            Assert.NotNull(postType, "Playlist class does not exist.");
        }
        [Test]
        public void TestAppDbContext_ClassExists_in_Models()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type postType = assembly.GetType("dotnetapp.Models.AppDbContext");
            Assert.NotNull(postType, "AppDbContext class does not exist.");
        }

        // Test to check that AppDbContext Contains DbSet for model Song
        [Test]
        public void AppDbContext_ContainsDbSet_Song()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(AppDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type SongType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Song");
            if (SongType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Songs");
            if (propertyInfo == null)
            {
                Assert.Fail("Songs property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(SongType), propertyInfo.PropertyType);
            }
        }

        // Test to check that AppDbContext Contains DbSet for model Playlist
        [Test]
        public void AppDbContext_ContainsDbSet_Playlist()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(AppDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type PlaylistType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Playlist");
            if (PlaylistType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Playlists");
            if (propertyInfo == null)
            {
                Assert.Fail("Playlists property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(PlaylistType), propertyInfo.PropertyType);
            }
        }

        [Test]
        public void TestTitlePropertyType()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Song");
            PropertyInfo UnitPriceProperty = _productType.GetProperty("Title");
            Assert.NotNull(UnitPriceProperty, "Title property does not exist.");
            Assert.AreEqual(typeof(string), UnitPriceProperty.PropertyType, "Title property should be of type String.");
        }

        [Test]
        public void TestTitlePropertyMaxLength100()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Song");
            PropertyInfo titleProperty = _productType.GetProperty("Title");
            var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Title property.");
            Assert.AreEqual(100, maxLengthAttribute.Length, "Title property should have a max length of 100.");
        }

        [Test]
        public void TestArtistPropertyMaxLength50()
        {            
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Song");
            PropertyInfo titleProperty = _productType.GetProperty("Artist");
            var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Artist property.");
            Assert.AreEqual(50, maxLengthAttribute.Length, "Artist property should have a max length of 50.");
        }

        [Test]
        public void TestReleaseYearPropertyRange()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Song");
            PropertyInfo publishedYearProperty = _productType.GetProperty("ReleaseYear");
            var rangeAttribute = publishedYearProperty.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
            
            Assert.NotNull(rangeAttribute, "Range attribute not found on ReleaseYear property.");
            Assert.AreEqual(1900, rangeAttribute.Minimum, "ReleaseYear property should have a minimum value of 1900.");
            Assert.AreEqual(2024, rangeAttribute.Maximum, "ReleaseYear property should have a maximum value of 2024");
        }

        // [Test]
        // public void TestNamePropertyRegularExpressionAttribute()
        // {
        //     Assembly assembly = Assembly.Load("dotnetapp");
        //     _productType = assembly.GetType("dotnetapp.Models.Playlist");            
        //     PropertyInfo cardNumberProperty = _productType.GetProperty("Name");
        //     var regexAttribute = cardNumberProperty.GetCustomAttribute<RegularExpressionAttribute>();

        //     Assert.NotNull(regexAttribute, "RegularExpression attribute not found on Name property.");
        //     Assert.AreEqual(@"LC-\d{5}", regexAttribute.Pattern, "Name property should have the correct regular expression pattern.");
        // }

        [Test]
        public void TestNamePropertyMaxLength100()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Playlist");
            PropertyInfo titleProperty = _productType.GetProperty("Name");
            var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Name property.");
            Assert.AreEqual(100, maxLengthAttribute.Length, "Name property should have a max length of 100.");
        }

        [Test]
        public void TestDescriptionPropertyMaxLength100()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productType = assembly.GetType("dotnetapp.Models.Playlist");
            PropertyInfo titleProperty = _productType.GetProperty("Description");
            var maxLengthAttribute = titleProperty.GetCustomAttribute<MaxLengthAttribute>();
            
            Assert.NotNull(maxLengthAttribute, "MaxLength attribute not found on Description property.");
            Assert.AreEqual(200, maxLengthAttribute.Length, "Name Description should have a max length of 200.");
        }

        [Test]
        public void TestMigrationExists()
        {
            bool viewsFolderExists = Directory.Exists(@"/home/coder/project/workspace/dotnetapp/Migrations");

            Assert.IsTrue(viewsFolderExists, "Migrations does not exist.");
        }

        [Test]
        public void Test_DisplayAllSongs_Action()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            controllerType = assembly.GetType("dotnetapp.Controllers.MusicController");
            var detailsMethod = GetMethod1(controllerType, "DisplayAllSongs", new Type[] {  });

            Assert.NotNull(detailsMethod);
        }

        [Test]
        public void Test_SearchSongsByTitle_Action()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            Type controllerType = assembly.GetType("dotnetapp.Controllers.MusicController");
            var searchSongsByTitleMethod = GetMethod1(controllerType, "SearchSongsByTitle", new Type[] { typeof(string) });

            Assert.NotNull(searchSongsByTitleMethod);
        }

        [Test]
        public void Test_DisplaySongsForPlaylist_Action()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            Type controllerType = assembly.GetType("dotnetapp.Controllers.MusicController");
            var searchSongsByTitleMethod = GetMethod1(controllerType, "DisplaySongsForPlaylist", new Type[] { typeof(int) });

            Assert.NotNull(searchSongsByTitleMethod);
        }

        [Test]
        public void Test_GetAvailableSongs_Action()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            controllerType = assembly.GetType("dotnetapp.Controllers.MusicController");
            var detailsMethod = GetMethod1(controllerType, "GetAvailableSongs", new Type[] {  });

            Assert.NotNull(detailsMethod);
        }

        [Test]
        public void SearchSongsByTitle_ShouldReturnWithCorrectModel()
        {
            // Arrange
            Assembly assembly = Assembly.Load("dotnetapp");
            Type controllerType = assembly.GetType("dotnetapp.Controllers.MusicController");
            var controller = Activator.CreateInstance(controllerType, _context);
            MethodInfo method = controllerType.GetMethod("SearchSongsByTitle", new Type[] { typeof(string) });
            var result = method.Invoke(controller, new object[] { "demo" });
            Assert.NotNull(result);
        }

[Test]
        public void Test_AddSong_Action()
        {
            // Arrange
            var song = new dotnetapp.Models.Song()
            {
                Title = "Sample Song",
                Artist = "Sample Artist",
                ReleaseYear = 2020
            };

            // Act
            var result = _musicController.AddSong(song);

            // Assert
            Assert.NotNull(result); // Assuming your AddSong method returns something meaningful, adjust this assertion accordingly
        }



    }
}
