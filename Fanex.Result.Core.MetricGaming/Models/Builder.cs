namespace Fanex.Result.Core.MetricGaming.Models
{
    using System.Collections.Generic;
    using Base.Models;

    public class Builder : BaseBuilder, IBuilder
    {
        private const string TemplateName = "Soccer3rd";

        public string HtmlBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            var model = GenerateViewModelData(dataList, setting, ViewModelConcreteFactory.Instance);
            return HtmlRender(Template.Instance, TemplateName, model);
        }

        public System.IO.MemoryStream ExcelBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            throw new System.NotImplementedException();
        }

        public string OutrightHtmlBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            throw new System.NotImplementedException();
        }

        public string PopUpParalyBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            throw new System.NotImplementedException();
        }

        public string HtmlGameDetailBuilder(dynamic data, Base.ResultSetting setting)
        {
            throw new System.NotImplementedException();
        }

        public System.IO.MemoryStream OutrightExcelBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            throw new System.NotImplementedException();
        }
    }
}