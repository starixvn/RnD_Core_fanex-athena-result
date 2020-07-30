using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.SabaClub
{
    [Serializable]
    public class SabaClubException : Exception
    {
        public SabaClubException(string message)
            : base(message)
        {
        }

        protected SabaClubException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
