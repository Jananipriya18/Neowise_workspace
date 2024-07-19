using System;

namespace dotnetapp.Exceptions
{
public class CollegeApplicationException : Exception
{
    public CollegeApplicationException(string message) : base(message)
    {
    }
}

}