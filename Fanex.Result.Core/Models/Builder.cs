// -----------------------------------------------------------------------
// <copyright file="Builder.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.ExcelModels;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.ViewModels;
    using RazorEngine;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class Builder : IBuilder
    {
        #region Html-Builder

        public string HtmlBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            var groupData = dataList.Select(i => ViewModelConcreteFactory.Instance.GetViewModel(i, setting, lang))
                .GroupBy(i => i.LeagueId).Select(i => new GroupData { LeagueId = i.Key, Data = i.ToList() });
            var model = CreateModel(lang, groupData, setting);
            return HtmlRender(setting.SportType.StringMe(), model);
        }

        /// <summary>
        /// Generate game detail value.
        /// </summary>
        /// <param name="data">The SPORTSBOOK data.</param>
        /// <param name="setting">The result setting.</param>
        /// <returns>The game detail value.</returns>
        public string HtmlGameDetailBuilder(Sportsbook data, ResultSetting setting)
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
            return HtmlRender(setting.SportType.StringMe(), model);
        }

        /// <summary>
        /// Render html for popup page.
        /// </summary>
        /// <typeparam name="T">The DTO model.</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="setting">The setting.</param>
        /// <returns>
        /// The html string.
        /// </returns>
        public string PopUpHtmlBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            if (setting.IsOutRight)
            {
                var groupDataOutRight = dataList.Select(i => ViewModelConcreteFactory.Instance.GetOutRightViewModel(i, setting, lang))
                     .GroupBy(i => i.LeagueId).Select(i => new GroupData { LeagueId = i.Key, Data = i.ToList() });

                var modelOutRight = CreateModel(lang, groupDataOutRight, setting);

                return HtmlRender("OutRight", modelOutRight);
            }

            var groupData = dataList.Select(i => ViewModelConcreteFactory.Instance.GetViewModel(i, setting, lang))
                 .GroupBy(i => i.LeagueId).Select(i => new GroupData { LeagueId = i.Key, Data = i.ToList() });
            var model = CreateModel(lang, groupData, setting);
            return HtmlRender(setting.SportType.StringMe(), model);
        }

        public string PopUpParalyBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            var groupData = dataList.Select(i => ViewModelConcreteFactory.Instance.GetParlayViewModel(i, setting, lang))
                 .GroupBy(i => i.LeagueId).Select(i => new GroupData { LeagueId = i.Key, Data = i.ToList() });
            var model = CreateModel(lang, groupData, setting);
            return HtmlRender("Parlay", model);
        }

        public string OutRightHtmlBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            var groupData = dataList.Select(i => ViewModelConcreteFactory.Instance.GetOutRightViewModel(i, setting, lang))
                .GroupBy(i => i.LeagueId).Select(i => new GroupData { LeagueId = i.Key, Data = i.ToList() });
            var model = CreateModel(lang, groupData, setting);
            return HtmlRender("OutRight", model);
        }

        #endregion Html-Builder

        #region ExcelBuilder

        public MemoryStream ExcelBuilder<T>(IList<T> dataList, ResultSetting setting)
        {
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            var excelParser = ViewModelConcreteFactory.Instance.GetExcelViewModel<T>(dataList, setting, lang);
            return excelParser.Export();
        }

        /// <summary>
        /// Render excel for outright report.
        /// </summary>
        /// <typeparam name="T">The DTO object.</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="setting">The result setting.</param>
        /// <returns>The memory stream.</returns>
        public MemoryStream OutRightExcelBuilder<T>(IList<T> dataList, ResultSetting setting)
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

        #endregion ExcelBuilder

        /// <summary>
        /// Render html result.
        /// </summary>
        /// <param name="templateName">The template name.</param>
        /// <param name="model">The binder model.</param>
        /// <returns>The html result.</returns>
        private static string HtmlRender(string templateName, RazorBinderModel model)
        {
            // loading a template might be expensive, so be careful to cache content
            if (Razor.Resolve(templateName) == null)
            {
                // we've never seen this template before, so compile it and stick it in cache.
                var templateContent = Template.Instance.GetRazorTemplate(templateName);
                Razor.Compile(templateContent, templateName);
            }

            return Razor.Run(templateName, model);
        }

        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <param name="lang">The language.</param>
        /// <param name="groupData">The data list.</param>
        /// <param name="setting">The result setting.</param>
        /// <param name="isDetail">If true,all detailed game will be rendered.</param>
        /// <returns>
        /// Returns RAZOR model.
        /// </returns>
        private static RazorBinderModel CreateModel(
            LangResources lang,
            IEnumerable<GroupData> groupData,
            ResultSetting setting,
            bool isDetail = false)
        {
            var isPopUp = setting.IsParlayPopUp || setting.IsPopUp;
            var model = new RazorBinderModel
             {
                 Resource = lang,
                 IsPopUp = isPopUp,
                 VisiblePopUpTitle = setting.VisiblePopUpTitle,
                 SportType = setting.SportType,
                 VisibleMatchCodeIDColumn = setting.VisibleMatchCodeIDColumn,
                 VisibleDetailedLink = setting.VisibleDetailedLink,
                 VisibleFreeHeader = setting.VisibleFreeHeader && !isPopUp,
                 IsDetail = isDetail,
             };

            model.Data.AddRange(groupData);

            return model;
        }
    }
}