using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Application
    {
        public int ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public string ContactNumber { get; set; }
        public string MailID { get; set; }
        public int JobID { get; set; }
        public Job Job { get; set; }
    }
}
