namespace Fanex.Result.Core.Happy5.Models
{
    using System;
    using Base.Models;

    internal class Template : BaseTemplate, ITemplate
    {
        private static readonly Lazy<ITemplate> LazyTemplateService = new Lazy<ITemplate>(() => new Template());

        public static ITemplate Instance => LazyTemplateService.Value;

        protected override string GetProductName => "Happy5";
    }
}