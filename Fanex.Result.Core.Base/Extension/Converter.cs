namespace Fanex.Result.Core.Base.Extension
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    internal static class Converter
    {
        [DebuggerStepThrough]
        public static T To<T>(object item)
        {
            if (!ReferenceEquals(item, null) && item != DBNull.Value)
            {
                return (T)Convert.ChangeType(item, typeof(T));
            }

            return default(T);
        }

        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public static bool ToBool(object item, bool defaultValue = false)
        {
            bool result = defaultValue;
            bool b = false;

            if (!ReferenceEquals(item, null) && item != DBNull.Value)
            {
                b = bool.TryParse(item.ToString(), out result);
            }

            return b ? result : defaultValue;
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static byte[] ToByteArray(object item)
        {
            return To<byte[]>(item);
        }
    }
}