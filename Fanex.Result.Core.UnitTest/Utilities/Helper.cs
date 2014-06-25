namespace Fanex.Result.Core.UnitTest.Utilities
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Helper for testing.
    /// </summary>
    public class Helper
    {
        private Helper()
        {
        }

        #region Run Method

        /// <summary>
        /// Runs a method on a type, given its parameters. This is useful for calling private methods.
        /// </summary>
        /// <param name="type">Type name.</param>
        /// <param name="method">Method name.</param>
        /// <param name="paramName">Input parameter.</param>
        /// <returns>The return value of the called method.</returns>
        public static object RunStaticMethod(System.Type type, string method, object[] paramName)
        {
            var flag = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            return RunMethod(type, method, null, paramName, flag);
        }

        public static object RunInstanceMethod(System.Type t, string strMethod, object objInstance, object[] param)
        {
            var flag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return RunMethod(t, strMethod, objInstance, param, flag);
        }

        /// <summary>
        /// Run method.
        /// </summary>
        /// <param name="type">Type name.</param>
        /// <param name="method">Method name.</param>
        /// <param name="objInstance">Object Instance.</param>
        /// <param name="paramName">Input parameter.</param>
        /// <param name="flag">Flag name.</param>
        /// <returns>Return object.</returns>
        private static object RunMethod(System.Type type, string method, object objInstance, object[] paramName, BindingFlags flag)
        {
            MethodInfo m;
            try
            {
                m = type.GetMethod(method, flag);
                if (m == null)
                {
                    throw new ArgumentException("There is no method '" + method + "' for type '" + type.ToString() + "'.");
                }

                return m.Invoke(objInstance, paramName);
            }
            catch
            {
                throw;
            }
        }

        #endregion Run Method
    }
}