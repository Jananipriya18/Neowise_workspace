using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Project
    {
        public int ProjectID { get; set; } 
        public string ProjectName { get; set; }
        public string NumberOfModules { get; set; }
        public string SubmissionDate { get; set; }
        // public int FreelancerID { get; set; } 
        // public Freelancer Freelancer { get; set; }
    }
}
