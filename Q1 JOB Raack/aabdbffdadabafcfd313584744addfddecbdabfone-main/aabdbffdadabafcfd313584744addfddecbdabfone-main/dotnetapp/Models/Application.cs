namespace dotnetapp.Models
{
    public class Application
    {
        public int ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public string ContactNumber { get; set; }
        public string MailID { get; set; }
        public string JobTitle { get; set; } // This should match a JobTitle in Job entity
    }
}
