namespace dotnetapp.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }  // Primary key
        public DateTime AppointmentDate { get; set; }  // Date and time of the appointment
        public string PatientName { get; set; }  // Name of the patient
        public string Reason { get; set; }  // Reason for the appointment
        public int? DoctorId { get; set; }  // Foreign key for the doctor
        public Doctor? Doctor { get; set; }  // Navigation property to Doctor
    }
}
