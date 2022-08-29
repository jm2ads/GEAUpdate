using System;

namespace Commons.Commons.Exceptions
{
    public class InvalidCredentialsException : Exception
    {

        public string Error { get; set; }

        public string ErrorDescription { get; set; }

        public InvalidCredentialsException()
        {
        }

        public InvalidCredentialsException(string message) : base(message)
        {
        }

    }
}
