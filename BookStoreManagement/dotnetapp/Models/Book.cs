namespace dotnetapp.Models
{
     public class Book
    {
        public int BookId { get; set; }  
        public string Title { get; set; }  
        public string Genre { get; set; }  
        public decimal Price { get; set; } 
        public int? AuthorId { get; set; }  
        public Author? Author { get; set; }
    }
}
