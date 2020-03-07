using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Exceptions
{
    public class PersistenceException : Exception
    {
        public PersistenceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
