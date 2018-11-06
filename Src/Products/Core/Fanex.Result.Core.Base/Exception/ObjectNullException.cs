namespace Fanex.Result.Core.Base.Exception
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ObjectNullException : Exception
    {
        public ObjectNullException(string message)
            : base(message)
        {
        }

        protected ObjectNullException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}