namespace dotnetapp.Models
{
    public class BookLoan
    {
        public int BookLoanId { get; set; } 
        public string BookTitle { get; set; } 
        public DateTime LoanDate { get; set; }  
        public DateTime? ReturnDate { get; set; }  
        public int? LibraryManagerId { get; set; }  
        public LibraryManager? LibraryManager { get; set; }  
    }
}
