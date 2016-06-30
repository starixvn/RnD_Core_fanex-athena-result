namespace Fanex.Result.Core.Base.Exception
{
    using System;

    [Serializable]
    public class ObjectUnSupportedException : System.Exception
    {
        public ObjectUnSupportedException(string message)
            : base(message)
        {
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}