using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.LinkedGames
{
    [Serializable]
    public class LinkedGamesException : Exception
    {
        public LinkedGamesException(string message)
            : base(message)
        {
        }

        protected LinkedGamesException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
