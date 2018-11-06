namespace Fanex.Result.Core.AllBetCasino
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AllBetCasinoException : Exception
    {
        public AllBetCasinoException()
        {
        }

        public AllBetCasinoException(string message)
            : base(message)
        {
        }

        public AllBetCasinoException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected AllBetCasinoException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message => string.Format("AllBetCasino Result API Exception. Exception: {0}", base.Message);

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}