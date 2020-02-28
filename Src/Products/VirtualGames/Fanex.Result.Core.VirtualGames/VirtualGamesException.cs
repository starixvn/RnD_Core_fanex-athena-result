using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.VirtualGames
{
    [Serializable]
    public class VirtualGamesException : Exception
    {
        public VirtualGamesException(string message)
            : base(message)
        {
        }

        protected VirtualGamesException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
