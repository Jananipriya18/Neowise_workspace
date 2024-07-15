using System;

namespace dotnetapp.Exceptions
{
    public class PartyHallException : Exception
    {
        public PartyHallException(string message) : base(message)
        {
        }
    }
}
