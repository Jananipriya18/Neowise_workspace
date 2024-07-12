using System;

namespace dotnetapp.Exceptions
{
    public class StockItemException : Exception
    {
        public StockItemException(string message) : base(message)
        {
        }
    }
}
