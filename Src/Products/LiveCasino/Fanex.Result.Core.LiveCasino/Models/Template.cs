namespace Fanex.Result.Core.LiveCasino.Models
{
    using System;
    using BaseCore = Base.Models;

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
            get { return "LiveCasino"; }
        }
    }
}