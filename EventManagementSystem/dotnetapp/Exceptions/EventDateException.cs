using System;

namespace dotnetapp.Exceptions
{

public class EventDateException : Exception
{
    public EventDateException(string message) : base(message)
    {
    }   
}
}