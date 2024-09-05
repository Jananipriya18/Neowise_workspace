using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Author
    {
        public int AuthorId { get; set; }  
        public string Name { get; set; }  
        public string Biography { get; set; }  
        [JsonIgnore]
        public ICollection<Book>? Books { get; set; }
    }
}
