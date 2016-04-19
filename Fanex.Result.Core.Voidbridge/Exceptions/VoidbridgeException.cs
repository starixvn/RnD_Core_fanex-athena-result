namespace Fanex.Result.Core.Voidbridge.Exceptions
{
    using System;

    [Serializable]
    public class VoidbridgeException : Exception
    {
        public VoidbridgeException(string message)
            : base(message)
        {
        }

        public override string Message
        {
            get
            {
                return $"Voidbridge Result API Exception. Exception: {base.Message}";
            }
        }
    }
}