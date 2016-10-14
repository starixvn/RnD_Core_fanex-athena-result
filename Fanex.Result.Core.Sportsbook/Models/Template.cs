namespace Fanex.Result.Core.Sportsbook.Models
{
    using System;
    using Base.Models;

    internal class Template : BaseTemplate, ITemplate
    {
        private static readonly Lazy<ITemplate> LazyTemplateService = new Lazy<ITemplate>(() => new Template());

        public static ITemplate Instance
        {
            get
            {
                return LazyTemplateService.Value;
            }
        }

        protected override string GetProductName
        {
            get { return "Sportsbook"; }
        }
    }
}