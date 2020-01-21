using System;
using System.Runtime.Serialization;

namespace Test.StackThrow.CustomExceptions
{
    public class TgpException : Exception
    {
        public TgpException()
        { }

        public TgpException(string message) : base(message)
        { }

        public TgpException(string message, Exception innerException) : base(message, innerException)
        { }

        

        protected TgpException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        { }
    }
}
