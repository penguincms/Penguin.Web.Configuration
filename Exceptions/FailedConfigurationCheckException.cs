using System;
using System.Runtime.Serialization;

namespace Penguin.Web.Configuration.Exceptions
{
    public class FailedConfigurationCheckException : Exception
    {
        public FailedConfigurationCheckException()
        {
        }

        public FailedConfigurationCheckException(string message) : base(message)
        {
        }

        public FailedConfigurationCheckException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FailedConfigurationCheckException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}