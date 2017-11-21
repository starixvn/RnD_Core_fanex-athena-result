namespace Fanex.Result.Core.GlobalGaming.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class GlobalGamingException : Exception
    {
        public GlobalGamingException(string message)
            : base(message)
        {
        }

        protected GlobalGamingException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Global Gaming Result API Exception. Exception: {base.Message}";
    }
}