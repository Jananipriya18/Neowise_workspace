using System;

namespace dotnetapp.Exceptions
{

public class PriceItemException : Exception
{
    public PriceItemException(string message) : base(message)
    {
    }
}
}

