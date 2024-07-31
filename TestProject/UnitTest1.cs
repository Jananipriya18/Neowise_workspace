using dotnetapp.Controllers;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class MovieControllerTests
    {
        private ApplicationDbContext _context;
        private MovieReviewController _controller;

        [SetUp]
        public void Setup()
        {
            // Set up the test database context
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();

            _controller = new MovieReviewController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the test database context
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        // Test if ReviewForm action with valid MovieID and valid MovieReview redirects to ReviewConfirmation action with correct route values
        
        [Test]
        public async Task ReviewForm_Post_Method_ValidMovieID_ValidMovieReview_RedirectsToReviewConfirmation()
        {
            // Arrange
            var movie = new Movie
            {
                MovieID = 100,
                Title = "Inception",
                Director = "Christopher Nolan",
                ReleaseYear = DateTime.Parse("2010-07-16"),
                Reviews = new List<MovieReview>()
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var movieReview = new MovieReview
            {
                 MovieID = movie.MovieID,
                ReviewerName = "John Doe",
                Email = "john@example.com",
                Rating = 3,
                ReviewText = "Amazing movie with a complex plot.",
                ReviewDate = DateTime.UtcNow
            };

            // Act
            var result = _controller.ReviewForm(movie.MovieID, movieReview) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("ReviewConfirmation", result.ActionName); // Ensure the correct action is redirected to
        }

    
        // This test checks if the ReviewConfirmation action with an invalid reviewId returns NotFoundResult
        [Test]
        public void ReviewConfirmation_Get_Method_InvalidReviewId_ReturnsNotFound()
        {
            // Arrange
            var reviewId = 999; // An ID that does not exist

            // Act
            var result = _controller.ReviewConfirmation(reviewId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }


        // // Test if ExperienceEnrollmentForm action with valid data creates an attendee and redirects to EnrollmentConfirmation
        // [Test]
        // public async Task ExperienceEnrollmentForm_Post_Method_ValidData_CreatesAttendeeAndRedirects()
        // {
        //     // Arrange
        //     var vrExperience = new VRExperience
        //     {
        //         VRExperienceID = 100,
        //         ExperienceName = "Virtual Space Exploration",
        //         StartTime = "10:00 AM",
        //         EndTime = "12:00 PM",
        //         MaxCapacity = 1,
        //         Location = "Virtual",
        //         Description = "Explore the wonders of space in a fully immersive virtual reality experience."
        //     };
        //     _context.VRExperiences.Add(vrExperience);
        //     await _context.SaveChangesAsync();

        //     // Act
        //     var result = await _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com", PhoneNumber = "9876543210" }) as RedirectToActionResult;

        //     // Assert
        //     Assert.IsNotNull(result);
        //     Assert.AreEqual("EnrollmentConfirmation", result.ActionName);
        // }



[Test]
public async Task ReviewForm_Post_Method_InvalidRating_ThrowsException()
{
    // Arrange
    var movie = new Movie
    {
        MovieID = 100,
        Title = "Inception",
        Director = "Christopher Nolan",
        ReleaseYear = new DateTime(2010, 7, 16)
    };
    _context.Movies.Add(movie);
    await _context.SaveChangesAsync();

    var controller = new MovieReviewController(_context);
    var invalidReview = new MovieReview
    {
        MovieID = movie.MovieID,
        ReviewerName = "John Doe",
        Email = "john@example.com",
        Rating = 6, // Invalid rating (should be between 1 and 5)
        ReviewText = "Amazing movie!",
        ReviewDate = DateTime.Now
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<MovieReviewException>(async () =>
    {
        // Call the ReviewForm action and check for exception
        var result = controller.ReviewForm(movie.MovieID, invalidReview);
        // The exception should be thrown by the controller method
    });

    // Assert
    Assert.AreEqual("The rating must be between 1 and 5.", exception.Message);
}



// // This test checks if VRExperienceBookingException throws the message "Maximum Attendees Registered" or not
// // Test if ExperienceEnrollmentForm action throws VRExperienceBookingException with correct message after reaching capacity 0
// [Test]
// public void ExperienceEnrollmentForm_Post_Method_ThrowsException_With_Message()
// {
//     // Arrange
//     var vrExperience = new VRExperience
//     {
//         VRExperienceID = 100,
//         ExperienceName = "Virtual Space Exploration",
//         StartTime = "10:00 AM",
//         EndTime = "12:00 PM",
//         MaxCapacity = 0,
//         Location = "Virtual",
//         Description = "Explore the wonders of space in a fully immersive virtual reality experience."
//     };
//     _context.VRExperiences.Add(vrExperience);
//     _context.SaveChanges();

//     // Act and Assert
//      var exception = Assert.ThrowsAsync<VRExperienceBookingException>(async () =>
//     {
//         await _controller.ExperienceEnrollmentForm(vrExperience.VRExperienceID, new Attendee { Name = "John Doe", Email = "john@example.com", PhoneNumber = "9876543210" });
//     });

//     // Assert
//     // Assert.AreEqual("Maximum Attendees Registered", exception.Message);
// }

    
// // This test checks if EnrollmentConfirmation action returns NotFound for a non-existent attendee ID
//         [Test]
//         public async Task EnrollmentConfirmation_Get_Method_NonexistentAttendeeID_ReturnsNotFound()
//         {
//             // Arrange
//             var nonExistentAttendeeId = 999; // An ID that does not exist in the database

//             // Act
//             var result = await _controller.EnrollmentConfirmation(nonExistentAttendeeId) as NotFoundResult;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual(404, result.StatusCode);
//         }


        // This test checks the existence of the Movie class
        [Test]
        public void MovieClassExists()
        {
            // Arrange
            var movie = new Movie();

            // Assert
            Assert.IsNotNull(movie);
        }

        // This test checks the existence of the Class class
        [Test]
        public void MovieReviewExists()
        {
            // Arrange
            var classEntity = new MovieReview();

            // Assert
            Assert.IsNotNull(classEntity);
        }
 
 //This test check the exists of ApplicationDbContext class has DbSet of MovieReviews
 [Test]
        public void ApplicationDbContextContainsDbSetMovieReviewProperty()
        {

            var propertyInfo = _context.GetType().GetProperty("MovieReviews");
        
            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<MovieReview>), propertyInfo.PropertyType);
                   
        }

        //This test check the exists of ApplicationDbContext class has DbSet of MovieReviews
 [Test]
        public void ApplicationDbContextContainsDbSetMovieProperty()
        {

            var propertyInfo = _context.GetType().GetProperty("Movies");
        
            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<Movie>), propertyInfo.PropertyType);
                   
        }
    //     // This test checks the MovieID of Movie property is int
       [Test]
        public void Movie_Properties_MovieID_ReturnExpectedDataTypes()
        {
            Movie classEntity = new Movie();
            Assert.That(classEntity.MovieID, Is.TypeOf<int>());
        }

      // This test checks the Title of Movie property is string
        [Test]
        public void Movie_Properties_Title_ReturnExpectedDataTypes()
        {
            // Arrange
            Movie classEntity = new Movie { Title = "Demo Title" };

            // Assert
            Assert.That(classEntity.Title, Is.TypeOf<string>());
        }

      // This test checks the Director of Movie property is string
        [Test]
        public void Movie_Properties_Director_ReturnExpectedDataTypes()
        {
            // Arrange
            Movie classEntity = new Movie { Director = "Demo Director" };

            // Assert
            Assert.That(classEntity.Director, Is.TypeOf<string>());
        }

        // This test checks the ReleaseYear of Movie property is DateTime
        [Test]
        public void Movie_Properties_ReleaseYear_ReturnExpectedDataTypes()
        {
            Movie classEntity = new Movie();
            Assert.That(classEntity.ReleaseYear, Is.TypeOf<DateTime>());
        }

       // This test checks the expected value of MovieID
        [Test]
        public void Movie_Properties_MovieID_ReturnExpectedValues()
        {
            // Arrange
            int expectedMovieID = 100;

            Movie classEntity = new Movie
            {
                MovieID = expectedMovieID
            };
            Assert.AreEqual(expectedMovieID, classEntity.MovieID);
        }

        // This test checks the expected value of Title
        [Test]
        public void Movie_Properties_Title_ReturnExpectedValues()
        {
            string expectedTitle= "Demo Title";

            Movie classEntity = new Movie
            {
                Title = expectedTitle
            };
            Assert.AreEqual(expectedTitle, classEntity.Title);
        }

         // This test checks the expected value of Director
        [Test]
        public void Movie_Properties_Director_ReturnExpectedValues()
        {
            string expectedDirector = "Demo Director";

            Movie classEntity = new Movie
            {
                Director = expectedDirector
            };
            Assert.AreEqual(expectedDirector, classEntity.Director);
        }

        // This test checks the expected value of ReleaseYear
        [Test]
        public void Movie_Properties_ReleaseYear_ReturnExpectedValues()
        {
            DateTime expectedReleaseYear = new DateTime(2015, 1, 1);
            Movie classEntity = new Movie
            {
                ReleaseYear = expectedReleaseYear
            };
            Assert.AreEqual(expectedReleaseYear, classEntity.ReleaseYear);
        }

        // This test checks the expected value of MovieReviewID in MovieReview class is int
        [Test]
        public void MovieReview_Properties_MovieReviewID_ReturnExpectedDataTypes()
        {
            MovieReview moviereview = new MovieReview();
            Assert.That(moviereview.MovieReviewID, Is.TypeOf<int>());
        }

        // This test checks the expected value of Name in MovieReview class is string
        [Test]
        public void MovieReview_Properties_ReviewText_ReturnExpectedDataTypes()
        {
            MovieReview moviereview = new MovieReview();
            moviereview.ReviewText = "Demo ReviewText";
            Assert.That(moviereview.ReviewText, Is.TypeOf<string>());
        }

        // This test checks the expected value of Rating in MovieReview class is string
        [Test]
        public void MovieReview_Properties_Email_ReturnExpectedDataTypes()
        {
            MovieReview moviereview = new MovieReview();
            moviereview.Rating = 3;
            Assert.That(moviereview.Rating, Is.TypeOf<int>());
        }

        // This test checks the expected value of ReviewerName in MovieReview class is string
        [Test]
        public void MovieReview_Properties_ReviewerName_ReturnExpectedDataTypes()
        {
            MovieReview moviereview = new MovieReview();
            moviereview.ReviewerName = "John";
            Assert.That(moviereview.ReviewerName, Is.TypeOf<string>());
        }

        // This test checks the expected value of VRExperienceID in Attendee class is int
        [Test]
        public void MovieReview_Properties_MovieID_ReturnExpectedDataTypes()
        {
            MovieReview moviereview = new MovieReview();
            Assert.That(moviereview.MovieID, Is.TypeOf<int>());
        }

        // This test checks the expected value of Email in moviereview class is string
        [Test]
        public void MovieReview_Properties_Email_ReturnExpectedValues()
        {
            string expectedEmail = "john@example.com";

            MovieReview moviereview = new MovieReview
            {
                Email = expectedEmail
            };
            Assert.AreEqual(expectedEmail, moviereview.Email);
        }

        [Test]
        public void MovieReview_Properties_ReviewDate_ReturnExpectedValues()
        {
             DateTime expectedReviewDate = new DateTime(2024, 7, 31); 

            MovieReview moviereview = new MovieReview
            {
                ReviewDate = expectedReviewDate
            };
            Assert.AreEqual(expectedReviewDate, moviereview.ReviewDate);
        }

        [Test]
        public void MovieReview_Properties_Returns_Movie_ExpectedValues()
        {
            // Arrange
            var expectedMovie = new Movie
            {
                MovieID = 1,
                Title = "Sample Movie",
                Director = "Sample Director",
                ReleaseYear = DateTime.Parse("2024-01-01")
            };

            var movieReview = new MovieReview
            {
                Movie = expectedMovie
            };

            // Act & Assert
            Assert.AreEqual(expectedMovie, movieReview.Movie);
        }


        [Test]
        public async Task DeleteMovieConfirmed_Post_Method_ValidMovieID_RemovesMovieFromDatabase()
        {
            // Arrange
            // Create a new Movie instance
            var movie = new Movie 
            { 
                MovieID = 100, 
                Title = "Test Movie", 
                Director = "Test Director", 
                ReleaseYear = DateTime.Parse("2022-01-01") 
            };
            
            // Add the movie to the context
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            
            // Create an instance of the MovieController
            var controller = new MovieController(_context);

            // Act
            // Call the DeleteMovieConfirmed action
            var result = await controller.DeleteMovieConfirmed(movie.MovieID) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AvailableMovies", result.ActionName); // Check if redirected to AvailableMovies action

            // Check if the movie was removed from the database
            var deletedMovie = await _context.Movies.FindAsync(movie.MovieID);
            Assert.IsNull(deletedMovie);
        }


[Test]
public async Task AvailableMovies_SearchByTitle_ReturnsFilteredMovies()
{
    // Arrange
    _context.Movies.AddRange(
        new Movie { MovieID = 121, Title = "Inception", Director = "Christopher Nolan", ReleaseYear = DateTime.Parse("2010-07-16") },
        new Movie { MovieID = 122, Title = "Interstellar", Director = "Christopher Nolan", ReleaseYear = DateTime.Parse("2014-11-07") },
        new Movie { MovieID = 123, Title = "The Dark Knight", Director = "Christopher Nolan", ReleaseYear = DateTime.Parse("2008-07-18") },
        new Movie { MovieID = 124, Title = "Batman", Director = "Christopher Nolan", ReleaseYear = DateTime.Parse("2005-06-15") },
        new Movie { MovieID = 125, Title = "Superman", Director = "Zack Snyder", ReleaseYear = DateTime.Parse("2013-06-14") }
    );
    await _context.SaveChangesAsync();

    var movieController = new MovieController(_context);

    // Act
    var result = await movieController.AvailableMovies("n") as ViewResult;
    var movies = result?.Model as List<Movie>;

    // Log the titles for debugging purposes
    Console.WriteLine("Movies Returned:");
    if (movies != null)
    {
        foreach (var movie in movies)
        {
            Console.WriteLine(movie.Title);
        }
    }

    // Assert
    Assert.IsNotNull(result);
    Assert.IsInstanceOf<ViewResult>(result);
    Assert.IsNotNull(movies);
    Assert.IsTrue(movies.Count >= 1);  // There should be at least one movie ending with "n"
    Assert.IsTrue(movies.All(m => m.Title.EndsWith("n")));  // All movies should end with "n"
}


     }
 }