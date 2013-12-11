// -----------------------------------------------------------------------
// <copyright file="Template.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class Template : ITemplate
    {
        private static readonly object SynceObject = new object();

        private static ITemplate instance;

        private Template()
        {
        }

        public static ITemplate Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SynceObject)
                    {
                        if (instance == null)
                        {
                            instance = new Template();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Get razor template.
        /// </summary>
        /// <param name="templateName">The template name.</param>
        /// <returns>The html string.</returns>
        public string GetRazorTemplate(string templateName)
        {
            if (string.IsNullOrEmpty(templateName) || string.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentNullException("templateName");
            }

            using (var stream = Assembly.GetExecutingAssembly()
           .GetManifestResourceStream("Fanex.Result.Core.Views." + templateName + ".cshtml"))
            {
                if (stream == null)
                {
                    throw new ArgumentNullException(string.Format("The {0} was not found", templateName));
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
