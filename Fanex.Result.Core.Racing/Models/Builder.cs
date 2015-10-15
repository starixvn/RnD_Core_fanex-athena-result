// -----------------------------------------------------------------------
// <copyright file="Builder.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Racing.Models
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Models;
    using Fanex.Result.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Builder : BaseBuilder, IBuilder
    {
        public string HtmlBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            var model = GenerateViewModelData(dataList, setting, ViewModelConcreteFactory.Instance);
            return HtmlRender(Template.Instance, setting.SportType.StringMe(), model);
        }

        public System.IO.MemoryStream ExcelBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
          
            var excelParser = ViewModelConcreteFactory.Instance.GetExcelViewModel<T>(dataList, setting);
            return excelParser.Export();
        }

        public string OutRightHtmlBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public string PopUpParalyBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public string HtmlGameDetailBuilder(dynamic data, Base.ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public System.IO.MemoryStream OutRightExcelBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            throw new NotImplementedException();
        }
    }
}