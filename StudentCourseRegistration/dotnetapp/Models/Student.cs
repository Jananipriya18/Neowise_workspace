namespace dotnetapp.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Department {get;set;}
        public ICollection<Course>? Courses { get; set; }
    }
}