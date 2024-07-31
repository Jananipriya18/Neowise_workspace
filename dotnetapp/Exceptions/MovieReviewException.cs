using System;

namespace dotnetapp.Exceptions
{
    public class MovieReviewException : Exception
    {
        public MovieReviewException(string message) : base(message) { }

       
    }
}
