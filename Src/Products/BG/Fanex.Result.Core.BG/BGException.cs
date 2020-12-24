using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.BG
{
    [Serializable]
    public class BGException : Exception
    {
        public BGException(string message)
            : base(message)
        {
        }

        protected BGException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
