namespace dotnetapp.Controllers
{  
 
        public IActionResult DisplayBooksForLibraryCard(int libraryCardId) 
        {
	    // Method to display books associated with a library card
            // Handle the case where the library card with the given ID doesn't exist
            // Find books associated with the library card ID
            // Return the list of books as a JSON response
        }

        public IActionResult AddBook([FromBody] Book book)
        {
	    // Method to add a book - Bind the Book object from the request body
            // Check if the model state is valid
            // Add the book to the DbSet and Save changes to the database
            // Return a 201 Created response with the URI of the newly created book
            // Return the model validation errors if the model state is not valid
        }
        
        public IActionResult DisplayAllBooks()
        {
	    // Method to display all books in the library
            // Retrieve all books from the DbSet
            // Return the list of books as a JSON response
        }

        public IActionResult SearchBooksByTitle([FromQuery] string query)
        {
	    // Method to search for books by title
            // If the query is null or empty, return all books
            // Filter books by title
            // Return the filtered list of books as a JSON response
        }

}
