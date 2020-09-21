using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.Joker
{
    [Serializable]
    public class JokerException : Exception
    {
        public JokerException(string message)
            : base(message)
        {
        }

        protected JokerException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
