// --------------------------------------------------------------------------
// <copyright file="Converter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
// <change who="anders.le" date="2012.2.22">Created</change>
// <change who="anders.le" date="2012.2.22">Add more param with default value</change>
// <change who="Anders.Le" date="2012.8.22">Fix bug convert to decimal</change>
// </history>

namespace Fanex.Result.Core.Extension
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Convert to specific type.
    /// </summary>
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

        /// <summary>
        /// Toes the byte array.
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <returns>The array byte.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static byte[] ToByteArray(object item)
        {
            return To<byte[]>(item);
        }
    }
}