using System;

namespace dotnetapp.Exceptions
{

public class StudentException : Exception
{
    public StudentException(string message) : base(message)
    {
    }
}
}