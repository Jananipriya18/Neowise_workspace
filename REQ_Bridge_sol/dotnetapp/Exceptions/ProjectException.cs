using System;

namespace dotnetapp.Exceptions
{
public class ProjectException : Exception
{
    public ProjectException(string message) : base(message)
    {
    }
}

}