// -----------------------------------------------------------------------
// <copyright file="MicroGamingException.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.MicroGaming.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class MicroGamingException : System.Exception
    {
        public MicroGamingException()
        {
        }

        public MicroGamingException(string message)
            : base(message)
        {
        }

        public MicroGamingException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected MicroGamingException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
        {
            get
            {
                return
                    string.Format("MicroGaming Result API Exception. Exception: {0}", base.Message);
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}