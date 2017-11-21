namespace Fanex.Result.Core.Voidbridge.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class VoidbridgeException : Exception
    {
        public VoidbridgeException(string message)
            : base(message)
        {
        }

        protected VoidbridgeException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Voidbridge Result API Exception. Exception: {base.Message}";
    }
}