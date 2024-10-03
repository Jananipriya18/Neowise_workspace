using System;

namespace dotnetapp.Exceptions
{

public class StockQuantityException : Exception
{
    public StockQuantityException(string message) : base(message)
    {
    }   
}
}