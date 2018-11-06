namespace Fanex.Result.Core.Happy5.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Base;
    using Base.Extension;
    using Base.Models;

    public class Builder : BaseBuilder, IBuilder
    {
        public string HtmlBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var viewModel = GenerateViewModelData(dataList, setting, ViewModelConcreteFactory.Instance);

            return HtmlRender(Template.Instance, setting.SportType.StringMe(), viewModel);
        }

        public MemoryStream ExcelBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var excelViewModel = ViewModelConcreteFactory.Instance.GetExcelViewModel(dataList, setting);

            return excelViewModel.Export();
        }

        public string HtmlGameDetailBuilder(dynamic data, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public MemoryStream OutrightExcelBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public string OutrightHtmlBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public string PopUpParalyBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            throw new NotImplementedException();
        }
    }
}