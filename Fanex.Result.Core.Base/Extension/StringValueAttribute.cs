namespace Fanex.Result.Core.Base.Extension
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public sealed class StringValueAttribute : Attribute
    {
        private readonly string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}