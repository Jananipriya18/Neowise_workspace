// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;

// namespace dotnetapp.Models
// {
//     public class Class
//     {
//         public int ClassID { get; set; }

//         [Required(ErrorMessage = "ClassName is required")]
//         public string ClassName { get; set; }

//         [Required(ErrorMessage = "StartTime is required")]
//         public string StartTime { get; set; }

//         [Required(ErrorMessage = "EndTime is required")]
//         public string EndTime { get; set; }

//         [Required(ErrorMessage = "Capacity is required")]
//         public int Capacity { get; set; }

//         public virtual ICollection<Student> Students { get; set; }
//     }
// }


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class HistoricalTour
    {
        public int HistoricalTourID { get; set; }

        [Required(ErrorMessage = "TourName is required")]
        public string TourName { get; set; }

        [Required(ErrorMessage = "StartTime is required")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required")]
        public string EndTime { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }
    }
}
