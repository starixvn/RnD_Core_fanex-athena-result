// -----------------------------------------------------------------------
// <copyright file="Template.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.AGCasino.Models
{
    using System;
    using System.IO;
    using System.Reflection;
    using BaseCore = Fanex.Result.Core.Base.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class Template : BaseCore.BaseTemplate, BaseCore.ITemplate
    {
        private static readonly Lazy<BaseCore.ITemplate> LazyTemplateService = new Lazy<BaseCore.ITemplate>(() => new Template());

        public static BaseCore.ITemplate Instance
        {
            get
            {
                return LazyTemplateService.Value;
            }
        }

        protected override string GetProductName
        {
            get { return "AGCasino"; }
        }

        protected override Stream GetViewAbsemblyName(string templateName)
        {
            return Assembly.GetExecutingAssembly()
             .GetManifestResourceStream(string.Format(VIEWDOMAINPATTERN, GetProductName, templateName));
        }
    }
}