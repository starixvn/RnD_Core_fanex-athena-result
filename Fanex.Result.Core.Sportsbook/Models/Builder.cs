// -----------------------------------------------------------------------
// <copyright file="Builder.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Models;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Models;
    using Fanex.Result.Core.Sportsbook.ExcelModels;

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
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            var excelParser = ViewModelConcreteFactory.Instance.GetExcelViewModel<T>(dataList, setting, lang);
            return excelParser.Export();
        }

        public string OutRightHtmlBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            var groupData = dataList.Select(i => ViewModelConcreteFactory.Instance.GetOutRightViewModel(i, setting, lang))
                .GroupBy(i => i.LeagueId).Select(i => new GroupData { LeagueId = i.Key, Data = i.ToList() });
            var model = CreateModel(lang, groupData, setting);
            return HtmlRender(Template.Instance, "OutRight", model);
        }

        public string PopUpParalyBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            var groupData = dataList.Select(i => ViewModelConcreteFactory.Instance.GetParlayViewModel(i, setting, lang))
                 .GroupBy(i => i.LeagueId).Select(i => new GroupData { LeagueId = i.Key, Data = i.ToList() });
            var model = CreateModel(lang, groupData, setting);
            return HtmlRender(Template.Instance, "Parlay", model);
        }

        public string HtmlGameDetailBuilder(dynamic data, Base.ResultSetting setting)
        {
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            var viewModelData = ViewModelConcreteFactory.Instance.GetViewModel(data, setting, lang);

            IList<GroupData> groupData = new List<GroupData>()
            {
                new GroupData
                {
                    LeagueId = viewModelData.LeagueId,
                    Data = new List<IResultViewModel>
                    {
                        viewModelData
                    }
                }
            };

            var model = CreateModel(lang, groupData, setting, true);
            return HtmlRender(Template.Instance, setting.SportType.StringMe(), model);
        }

        public System.IO.MemoryStream OutRightExcelBuilder<T>(IList<T> dataList, Base.ResultSetting setting)
        {
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            var outRightData =
                dataList
                .Select(i => ViewModelConcreteFactory.Instance.GetOutRightViewModel(i, setting, lang))
                .ToList();
            var realTemplatePath = setting.ExcelTemplatePath + "Outright.xls";
            var excelParser = new OutRightExcel(realTemplatePath, setting, outRightData, lang);
            return excelParser.Export();
        }
    }
}