namespace Fanex.Result.Core.Sportsbook.Models
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Base;
    using Base.Extension;
    using Base.Models;
    using Base.ViewModels;
    using ExcelModels;

    public class Builder : BaseBuilder, IBuilder
    {
        public string HtmlBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var model = GenerateViewModelData(dataList, setting, ViewModelConcreteFactory.Instance);
            return HtmlRender(Template.Instance, setting.SportType.StringMe(), model);
        }

        public MemoryStream ExcelBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var excelParser = ViewModelConcreteFactory.Instance.GetExcelViewModel<T>(dataList, setting);
            return excelParser.Export();
        }

        public string OutrightHtmlBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var groupData = dataList.Select(i => ViewModelConcreteFactory.Instance.GetOutrightViewModel(i, setting))
                .GroupBy(i => i.LeagueId)
                .Select(i => new GroupData { LeagueId = i.Key, Data = i.ToSafeList() }).ToSafeList();
            var model = CreateModel(groupData, setting);
            return HtmlRender(Template.Instance, "Outright", model);
        }

        public string PopUpParalyBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var groupData = dataList.Select(i => ViewModelConcreteFactory.Instance.GetParlayViewModel(i, setting))
                 .GroupBy(i => i.LeagueId)
                 .Select(i => new GroupData { LeagueId = i.Key, Data = i.ToSafeList() }).ToSafeList();
            var model = CreateModel(groupData, setting);
            return HtmlRender(Template.Instance, "Parlay", model);
        }

        public string HtmlGameDetailBuilder(dynamic data, ResultSetting setting)
        {
            var viewModelData = ViewModelConcreteFactory.Instance.GetViewModel(data, setting);

            IList<GroupData> groupData = new SafeList<GroupData>()
            {
                new GroupData
                {
                    LeagueId = viewModelData.LeagueId,
                    Data = new SafeList<IResultViewModel> { viewModelData }
                }
            };

            var model = CreateModel(groupData, setting, true);
            return HtmlRender(Template.Instance, setting.SportType.StringMe(), model);
        }

        public MemoryStream OutrightExcelBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var outRightData = dataList
                .Select(i => ViewModelConcreteFactory.Instance.GetOutrightViewModel(i, setting))
                .ToSafeList();
            var realTemplatePath = setting.ExcelTemplatePath + "Outright.xls";
            var excelParser = new OutrightExcel(realTemplatePath, setting, outRightData);
            return excelParser.Export();
        }
    }
}