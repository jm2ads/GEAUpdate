using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Commons.Exceptions
{
    public class LoginException : Exception
    {
        public string Error { get; set; }

        public string ErrorDescription { get; set; }

        public LoginException()
        {
        }

        public LoginException(string message) : base(message)
        {
        }
    }
}
