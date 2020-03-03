using System;

namespace RestEasy.Core.Exceptions
{
    public class InvalidGuidException : Exception
    {
        public InvalidGuidException(string message) : base(message)
        {
            
        }
    }
}