using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Freelancer
    {
        public int FreelancerID { get; set; } // Primary Key
        public string FreelancerName { get; set; }
        public string Specialization { get; set; }
        public decimal CommercialPerHour { get; set; }
        public string MailID { get; set; }
        public string ContactNumber { get; set; }
    }
}
