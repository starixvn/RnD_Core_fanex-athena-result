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
        ///   <see cref="ToBool(object item, bool defaultValue = false)"/>
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The integer value.</returns>
        [DebuggerStepThrough]
        public static int ToInt(object item, int defaultValue = 0)
        {
            int result = defaultValue;
            bool b = false;

            if (!ReferenceEquals(item, null) && item != DBNull.Value)
            {
                b = int.TryParse(item.ToString(), out result);
            }

            return b ? result : defaultValue;
        }

        /// <summary>
        /// Convert object to BOOL.
        /// If object is null or DBNull.Value then return default Value.
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <param name="defaultValue">If set to <c>true</c> [default value].</param>
        /// <returns>The BOOL value.</returns>
        [DebuggerStepThrough]
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
        ///   <see cref="ToBool(object item, bool defaultValue = false)"/>
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The decimal value.</returns>
        [DebuggerStepThrough]
        public static decimal ToDecimal(object item, decimal defaultValue = 0)
        {
            decimal result = defaultValue;
            bool b = false;

            if (!ReferenceEquals(item, null) && item != DBNull.Value)
            {
                b = decimal.TryParse(item.ToString(), out result);
            }

            return b ? result : defaultValue;
        }

        /// <summary>
        /// Return new GUIDE if item is null or DBNULL.
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <returns>The GUIDE value.</returns>
        [DebuggerStepThrough]
        public static Guid ToGuid(object item)
        {
            if (!ReferenceEquals(item, null) && item != DBNull.Value)
            {
                return Guid.Parse(item.ToString());
            }

            return new Guid();
        }

        /// <summary>
        /// Return DateTime Min Value if item is null or DBNULL Value.
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <returns>The date time value.</returns>
        [DebuggerStepThrough]
        public static DateTime ToDateTime(object item)
        {
            return ToDateTime(item, DateTime.MinValue);
        }

        /// <summary>
        ///   <see cref="ToBool(object item, bool defaultValue = false)"/>
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The date time value.</returns>
        public static DateTime ToDateTime(object item, DateTime defaultValue)
        {
            DateTime result = defaultValue;
            bool b = false;

            if (!ReferenceEquals(item, null) && item != DBNull.Value)
            {
                b = DateTime.TryParse(item.ToString(), out result);
            }

            return b ? result : defaultValue;
        }

        /// <summary>
        /// Return empty if object is null or DBNULL value.
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The char value.</returns>
        [DebuggerStepThrough]
        public static char ToChar(object item, char defaultValue = char.MinValue)
        {
            char result = defaultValue;
            bool b = false;

            if (!ReferenceEquals(item, null) && item != DBNull.Value)
            {
                b = char.TryParse(item.ToString(), out result);
            }

            return b ? result : defaultValue;
        }

        /// <summary>
        /// Toes the byte array.
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <returns>The array byte.</returns>
        [DebuggerStepThrough]
        public static byte[] ToByteArray(object item)
        {
            return To<byte[]>(item);
        }

        /// <summary>
        /// If object item in type T equal null, return string empty.
        /// </summary>
        /// <param name="item">The original item value.</param>
        /// <returns>The string value.</returns>
        [DebuggerStepThrough]
        public static string ToString2(object item)
        {
            if (!ReferenceEquals(item, null) && item != DBNull.Value)
            {
                return item.ToString();
            }

            return string.Empty;
        }
    }
}