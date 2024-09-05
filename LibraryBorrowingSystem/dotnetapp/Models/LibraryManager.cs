using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class LibraryManager
    {
        public int LibraryManagerId { get; set; }  
        public string Name { get; set; }  
        public string ContactInfo { get; set; } 
        [JsonIgnore]
        public ICollection<BookLoan>? BookLoans { get; set; } 
    }
}
