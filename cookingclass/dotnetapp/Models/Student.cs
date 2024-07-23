using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Student
    {
         public int StudentID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ClassID is required")]
        public int ClassID { get; set; }

        public virtual Class? Class { get; set; }
    }
}
