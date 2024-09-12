using System;

namespace dotnetapp.Exceptions
{
    public class PriceException : Exception
    {
        public PriceException()
        {
        }

        public PriceException(string message)
            : base(message)
        {
        }

        public PriceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
