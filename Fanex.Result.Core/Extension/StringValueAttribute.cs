// -----------------------------------------------------------------------
// <copyright file="StringValueAttribute.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Extension
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class StringValueAttribute : Attribute
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