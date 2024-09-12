using System;

namespace dotnetapp.Exceptions
{

public class PriceException : Exception
{
    public PriceException(string message) : base(message)
    {
    }
}
}