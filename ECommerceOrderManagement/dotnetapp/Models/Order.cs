namespace dotnetapp.Models
{
    public class Order
    {
        public int OrderId { get; set; }  // Primary key
        public DateTime OrderDate { get; set; }  // Date when the order was placed
        public decimal TotalAmount { get; set; }  // Total amount of the order
        public int? CustomerId { get; set; }  // Foreign key for the customer
        public Customer? Customer { get; set; }  // Navigation property to Customer
    }
}
