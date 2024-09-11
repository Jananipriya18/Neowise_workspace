using System;

namespace dotnetapp.Exceptions
{

public class BookLoanException : Exception
{
    public BookLoanException(string message) : base(message)
    {
    }
}
}