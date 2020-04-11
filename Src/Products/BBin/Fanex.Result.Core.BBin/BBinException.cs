namespace Fanex.Result.Core.BBin
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class BBinException : Exception
    {
        public BBinException()
        {
        }

        public BBinException(string message)
            : base(message)
        {
        }

        public BBinException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected BBinException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message => string.Format("BBin Result API Exception. Exception: {0}", base.Message);

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}