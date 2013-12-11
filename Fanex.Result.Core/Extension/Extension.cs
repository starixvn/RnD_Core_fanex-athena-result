// -----------------------------------------------------------------------
// <copyright file="Extension.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Extension
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class Extension
    {
        public static string StringMe(this SportType value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attribs != null && attribs.Length > 0 ? attribs[0].ToString() : null;
        }
    }
}