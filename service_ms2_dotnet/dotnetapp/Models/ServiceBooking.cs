namespace dotnetapp.Models
{
    public class ServiceBooking
    {
        public int ServiceBookingId { get; set; }  // Primary key
        public string VehicleModel { get; set; }  // Required, represents the model of the vehicle
        public string ServiceDate { get; set; }  // Required, represents the date of the service booking
        public string Status { get; set; }  // Represents the current status of the service
        public int ServiceCost { get; set; }  // Required, represents the total cost of the service
        public int? ServiceCenterId { get; set; }  // Foreign key linking the service booking to a ServiceCenter
        public ServiceCenter? ServiceCenter { get; set; }  // Navigation property
    }
}
