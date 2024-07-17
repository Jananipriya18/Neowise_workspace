using System;

namespace dotnetapp.Exceptions
{
public class OrphanageException : Exception
{
    public OrphanageException(string message) : base(message)
    {
    }
}

}