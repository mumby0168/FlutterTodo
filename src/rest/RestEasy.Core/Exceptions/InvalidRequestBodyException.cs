using System;

namespace RestEasy.Core.Exceptions
{
    public class InvalidRequestBodyException : Exception
    {
        public InvalidRequestBodyException(string message) : base(message)
        {
            
        }    
    }
}