using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.Casino
{
    [Serializable]
    public class CasinoException : Exception
    {
        public CasinoException(string message)
            : base(message)
        {
        }

        protected CasinoException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
