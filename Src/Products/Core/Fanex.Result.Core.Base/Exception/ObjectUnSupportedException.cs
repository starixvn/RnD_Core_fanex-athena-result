namespace Fanex.Result.Core.Base.Exception
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ObjectUnSupportedException : Exception
    {
        public ObjectUnSupportedException(string message)
            : base(message)
        {
        }

        protected ObjectUnSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}