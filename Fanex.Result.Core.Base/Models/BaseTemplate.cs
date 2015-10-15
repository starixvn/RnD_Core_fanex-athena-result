// -----------------------------------------------------------------------
// <copyright file="BaseTemplate.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.Models
{
    using System;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class BaseTemplate
    {
        protected const string VIEWDOMAINPATTERN = "Fanex.Result.Core.{0}.Views.{1}.cshtml";

        protected abstract string GetProductName { get; }

        /// <summary>
        /// Get razor template.
        /// </summary>
        /// <param name="templateName">The template name.</param>
        /// <returns>The html string.</returns>
        public virtual string GetRazorTemplate(string templateName)
        {
            if (string.IsNullOrEmpty(templateName) || string.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentNullException("templateName");
            }

            using (var stream = GetViewAbsemblyName(templateName))
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

        protected abstract Stream GetViewAbsemblyName(string templateName);
    }
}