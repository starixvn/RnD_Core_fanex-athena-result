namespace Fanex.Result.Core.Base.Extension
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Justification = "Reviewed!")]
    [SuppressMessage("Microsoft.Design", "CA1018:MarkAttributesWithAttributeUsage", Justification = "Reviewed!")]
    public sealed class StringValueAttribute : Attribute
    {
        private readonly string _value;

        public StringValueAttribute(string value)
        {
            this._value = value;
        }

        public override string ToString()
        {
            return this._value;
        }
    }
}