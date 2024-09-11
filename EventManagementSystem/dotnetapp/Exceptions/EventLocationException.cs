using System;

namespace dotnetapp.Exceptions
{

public class EventLocationException : Exception
{
    public EventLocationException(string message) : base(message)
    {
    }   
}
}