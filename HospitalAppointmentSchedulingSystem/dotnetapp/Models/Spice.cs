namespace dotnetapp.Models
{
    public class Spice
    {
        public int SpiceId { get; set; }  // Primary key
        public string Name { get; set; }  // Date and time of the appointment
        public string OriginCountry { get; set; }  // Name of the patient
        public string FlavorProfile { get; set; }  // Reason for the appointment
        public int StockQuantity { get; set; }  // Reason for the appointment
        public int? CustomerId { get; set; }  // Many-to-one: Each Spice belongs to one Customer
        public Customer? Customer { get; set; }  // Navigation property back to the Customer
    }
}
