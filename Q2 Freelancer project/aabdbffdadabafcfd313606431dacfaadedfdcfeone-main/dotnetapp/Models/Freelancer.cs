namespace dotnetapp.Models
{
    public class Project
    {
        public int ProjectID { get; set; } // Primary Key
        public string ProjectName { get; set; }
        public string NumberOfModules { get; set; }
        public string SubmissionDate { get; set; }

        public int FreelancerID { get; set; } // Foreign Key
        public Freelancer Freelancer { get; set; } // Navigation property
    }
}
