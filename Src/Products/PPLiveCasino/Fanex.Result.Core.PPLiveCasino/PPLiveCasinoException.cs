using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.PPLiveCasino
{
    [Serializable]
    public class PPLiveCasinoException : Exception
    {
        public PPLiveCasinoException(string message)
            : base(message)
        {
        }

        protected PPLiveCasinoException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
