using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Mobile.Commons.Exceptions
{
    public class SAPException : Exception
    {
        public SAPException(string message) : base(message)
        {

        }
    }
}
