namespace dotnetapp.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? StudentId { get; set; }  
        public Student? Student { get; set; }
    }
}