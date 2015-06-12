// -----------------------------------------------------------------------
// <copyright file="BuilderSportsbook.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Result.Core.Base.Models;
    using Fanex.Result.Core.Models;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Base.Extension;
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




        public string PopUpHtmlBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            throw new NotImplementedException();
        }

        public System.IO.MemoryStream ExcelBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            throw new NotImplementedException();
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
