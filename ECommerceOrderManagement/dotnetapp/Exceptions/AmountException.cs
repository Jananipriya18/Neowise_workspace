using System;

namespace dotnetapp.Exceptions
{

public class AmountException : Exception
{
    public AmountException(string message) : base(message)
    {
    }
}
}