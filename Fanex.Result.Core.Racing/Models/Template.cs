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
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class Template : BaseCore.BaseTemplate, BaseCore.ITemplate
    {

        private static readonly Lazy<BaseCore.ITemplate> lazy = new Lazy<BaseCore.ITemplate>(() => new Template());

        public static BaseCore.ITemplate Instance
        {
            get
            {
                return lazy.Value;
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