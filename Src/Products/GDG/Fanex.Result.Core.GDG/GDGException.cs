using System;
using System.Runtime.Serialization;

namespace Fanex.Result.Core.GDG
{
    [Serializable]
    public class GdgException : Exception
    {
        public GdgException(string message)
            : base(message)
        {
        }

        protected GdgException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result API Exception. Exception: {base.Message}";
    }
}
