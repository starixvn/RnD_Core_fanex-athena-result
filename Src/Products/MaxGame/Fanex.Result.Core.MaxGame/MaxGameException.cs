using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.MaxGame
{
    [Serializable]
    public class MaxGameException : Exception
    {
        public MaxGameException(string message)
            : base(message)
        {
        }

        protected MaxGameException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
