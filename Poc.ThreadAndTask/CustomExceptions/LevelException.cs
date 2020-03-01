using System;
using System.Runtime.Serialization;

namespace Poc.ThreadAndTask.CustomExceptions
{
    public class LevelException : Exception
    {
        public LevelException()
        {
        }

        public LevelException(string message) : base(message)
        {
        }

        public LevelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LevelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }



    public class Level01Exception : LevelException
    {
        public Level01Exception()
        {
        }

        public Level01Exception(string message) : base(message)
        {
        }

        public Level01Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Level01Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class Level02Exception : LevelException
    {
        public Level02Exception()
        {
        }

        public Level02Exception(string message) : base(message)
        {
        }

        public Level02Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Level02Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class Level03Exception : LevelException
    {
        public Level03Exception()
        {
        }

        public Level03Exception(string message) : base(message)
        {
        }

        public Level03Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Level03Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}