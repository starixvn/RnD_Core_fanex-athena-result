namespace Fanex.Result.Core.Base.Extension
{
    using System.Collections.Generic;

    public static class Extension
    {
        public static string StringMe(this SportType value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];
            return attribs != null && attribs.Length > 0 ? attribs[0].ToString() : null;
        }

        public static SafeList<T> ToSafeList<T>(this IEnumerable<T> value)
        {
            return new SafeList<T>(value);
        }
    }
}