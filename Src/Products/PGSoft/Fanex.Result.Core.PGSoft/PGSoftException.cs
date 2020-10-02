using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.PGSoft
{
    [Serializable]
    public class PGSoftException : Exception
    {
        public PGSoftException(string message)
            : base(message)
        {
        }

        protected PGSoftException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
