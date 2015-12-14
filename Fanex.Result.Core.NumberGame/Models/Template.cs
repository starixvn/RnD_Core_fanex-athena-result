// -----------------------------------------------------------------------
// <copyright file="Template.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.NumberGame.Models
{
    using System;
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
            get { return "NumberGame"; }
        }
    }
}