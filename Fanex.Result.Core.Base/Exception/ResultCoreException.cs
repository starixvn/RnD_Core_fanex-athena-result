// -----------------------------------------------------------------------
// <copyright file="ResultCoreException.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.Exception
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class ResultCoreException : System.Exception
    {
        public ResultCoreException()
        {
        }

        public ResultCoreException(string message)
            : base(message)
        {
        }

        public ResultCoreException(string mesasge, Exception innerException)
            : base(mesasge, innerException)
        {
        }

        protected ResultCoreException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
        {
            get
            {
                return
                    string.Format("Result.Core Exception. Exception: {0}", base.Message);
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}