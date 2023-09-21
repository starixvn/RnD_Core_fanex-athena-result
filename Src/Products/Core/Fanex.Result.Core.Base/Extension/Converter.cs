using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fanex.Result.Core.Base.Tests")]
namespace Fanex.Result.Core.Base.Extension
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    internal static class Converter
    {
        /// <summary>
        /// If object type in T is null or DBNull.Value, return default(T).
        /// </summary>
        /// <typeparam name="T">Type of converted value.</typeparam>
        /// <param name="item">The original item value.</param>
        /// <returns>T or default(T).</returns>
        [DebuggerStepThrough]
        public static T To<T>(object item)
        {
            if (!ReferenceEquals(item, null) && item != DBNull.Value)
            {
                return (T)Convert.ChangeType(item, typeof(T));
            }

            return default(T);
        }

        /// <summary>
        /// Convert object to BOOL.
        /// If object is null or DBNull.Value then return default Value.
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <param name="defaultValue">If set to <c>true</c> [default value].</param>
        /// <returns>The BOOL value.</returns>
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