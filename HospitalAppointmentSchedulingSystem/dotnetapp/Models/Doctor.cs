using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }  // Primary key
        public string Name { get; set; }  // Doctor's name
        public string Specialty { get; set; }  // Doctor's specialty
        public string ContactInfo { get; set; }  // Doctor's contact information

        // Navigation property for appointments scheduled with this doctor
         [JsonIgnore]
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
