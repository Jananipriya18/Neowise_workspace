using System;

namespace dotnetapp.Exceptions
{

public class BookNameException : Exception
{
    public BookNameException(string message) : base(message)
    {
    }
}
}