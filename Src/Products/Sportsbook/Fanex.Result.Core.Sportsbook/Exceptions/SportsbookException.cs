namespace Fanex.Result.Core.Sportsbook.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class SportsbookException : Exception
    {
        public SportsbookException(string message)
            : base(message)
        {
        }

        protected SportsbookException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message => $"Sportsbook Result Exception. Exception: {base.Message}";
    }
}