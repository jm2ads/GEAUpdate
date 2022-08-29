using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Commons.Exceptions
{
    public class NoConnectionException : Exception
    {
        public NoConnectionException() { 
        
        }
        public NoConnectionException(string message) : base(message)
        {

        }
    }
}
