using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SecurityBot.Command
{
    [Serializable]
    public class TransitionException : Exception
    {
        public TransitionException() : base()
        {

        }

        public TransitionException(string message) : base(message)
        {

        }

        public TransitionException(string message, Exception innerException) : base(message, innerException) { }

        protected TransitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
