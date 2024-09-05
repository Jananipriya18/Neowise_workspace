namespace dotnetapp.Models
{
    public class BookLoan
    {
        public int BookLoanId { get; set; } 
        public string BookTitle { get; set; } 
        public string LoanDate { get; set; }  
        public string? ReturnDate { get; set; }  
        public int? LibraryManagerId { get; set; }  
        public LibraryManager? LibraryManager { get; set; }  
    }
}
