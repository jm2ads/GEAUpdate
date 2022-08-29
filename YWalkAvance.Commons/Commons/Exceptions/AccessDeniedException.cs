using System;

namespace Commons.Commons.Exceptions
{
    public class AccessDeniedException : Exception
    {

        public string Error { get; set; }

        public string ErrorDescription { get; set; }

        public AccessDeniedException()
        {
        }

        public AccessDeniedException(string message) : base(message)
        {
        }

    }
}
