namespace Fanex.Result.Core.Base.Models
{
    using System.Collections.Generic;
    using System.IO;

    public interface IBuilder
    {
        MemoryStream ExcelBuilder<T>(IList<T> dataList, ResultSetting setting);

        string HtmlBuilder<T>(IList<T> dataList, ResultSetting setting);

        string OutrightHtmlBuilder<T>(IList<T> dataList, ResultSetting setting);

        string PopUpParalyBuilder<T>(IList<T> dataList, ResultSetting setting);

        string HtmlGameDetailBuilder(dynamic data, ResultSetting setting);

        MemoryStream OutrightExcelBuilder<T>(IList<T> dataList, ResultSetting setting);
    }
}