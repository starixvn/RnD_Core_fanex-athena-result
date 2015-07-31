// -----------------------------------------------------------------------
// <copyright file="Template.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Racing.Models
{
    using System.IO;
    using System.Reflection;
    using BaseCore = Fanex.Result.Core.Base.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class Template : BaseCore.BaseTemplate, BaseCore.ITemplate
    {
        private static readonly object SynceObject = new object();

        private static BaseCore.ITemplate instance;

        public static BaseCore.ITemplate Instance
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

        protected override Stream GetViewAbsemblyName(string templateName)
        {
            return Assembly.GetExecutingAssembly()
             .GetManifestResourceStream(string.Format(VIEWDOMAINPATTERN, GetProductName, templateName));
        }

        protected override string GetProductName
        {
            get { return "Racing"; }
        }
    }
}