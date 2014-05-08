// -----------------------------------------------------------------------
// <copyright file="Builder.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.ViewModels;
    using Newtonsoft.Json;
    using RazorEngine;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class Builder : IBuilder
    {
        #region Html-Builder

        public string HtmlBuilder<T>(IList<T> dataList, SportType sportType, string language)
        {
            var lang = Resources.Instance.GetLanguage(language);
            var template = Template.Instance.GetRazorTemplate(sportType.StringMe());
            var dataMaped = dataList.Select(i => ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang)).ToList();
            var model = CreateModel(lang, dataMaped, false, false);
            Razor.GetTemplate(template, sportType.StringMe());
            return Razor.Run(sportType.StringMe(), model);
        }

        /// <summary>
        /// Render html for popup page.
        /// </summary>
        /// <typeparam name="T">The DTO model.</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="sportType">The sport type.</param>
        /// <param name="isOutRight">Is outright.</param>
        /// <param name="language">The language code.</param>
        /// <returns>The html string.</returns>
        public string PopUpHtmlBuilder<T>(IList<T> dataList, SportType sportType, bool isOutRight, string language)
        {
            var lang = Resources.Instance.GetLanguage(language);
            string template;
            if (isOutRight)
            {
                template = Template.Instance.GetRazorTemplate("OutRight");
                var dataMapedOutRight = dataList.Select(i => ViewModelConcreteFactory.Instance.GetOutRightViewModel(i, language, lang)).ToList();
                var modelOutRight = CreateModel(lang, dataMapedOutRight, true, false);
                Razor.GetTemplate(template, "OutRight");
                return Razor.Run("OutRight", modelOutRight);
            }

            template = Template.Instance.GetRazorTemplate(sportType.StringMe());
            var dataMaped = dataList.Select(i => ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang)).ToList();
            var model = CreateModel(lang, dataMaped, true, false);
            Razor.GetTemplate(template, sportType.StringMe());
            return Razor.Run(sportType.StringMe(), model);
        }

        public string PopUpParalyBuilder<T>(IList<T> dataList, string language)
        {
            var lang = Resources.Instance.GetLanguage(language);
            var template = Template.Instance.GetRazorTemplate("Parlay");
            var dataMaped = dataList.Select(i => ViewModelConcreteFactory.Instance.GetParlayViewModel(i, language, lang)).ToList();
            var model = CreateModel(lang, dataMaped, true, true);
            Razor.GetTemplate(template, "Parlay");
            return Razor.Run("Parlay", model);
        }

        public string OutRightHtmlBuilder<T>(IList<T> dataList, string language)
        {
            var lang = Resources.Instance.GetLanguage(language);
            var template = Template.Instance.GetRazorTemplate("OutRight");
            var dataMaped = dataList.Select(i => ViewModelConcreteFactory.Instance.GetOutRightViewModel(i, language, lang)).ToList();
            var model = CreateModel(lang, dataMaped, false, false);
            Razor.GetTemplate(template, "OutRight");
            return Razor.Run("OutRight", model);
        }

        #endregion Html-Builder

        #region ExcelBuilder

        public MemoryStream ExcelBuilder<T>(IList<T> dataList, SportType sportType, string language, string templatePath, string excelTitle)
        {
            var excelParser = new ExcelParser();

            return excelParser.Parser(dataList, sportType, language, templatePath, excelTitle);
        }

        /// <summary>
        /// Render excel for outright report.
        /// </summary>
        /// <typeparam name="T">The DTO object.</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="language">The language code.</param>
        /// <param name="templatePath">The physical template path.</param>
        /// <param name="excelTitle">The excel title.</param>
        /// <returns>The memory stream.</returns>
        public MemoryStream OutRightExcelBuilder<T>(IList<T> dataList, string language, string templatePath, string excelTitle)
        {
            var excelParser = new ExcelParser();

            return excelParser.ParserOutRight(dataList, language, templatePath, excelTitle);
        }

        #endregion ExcelBuilder

        #region JsonBuilder

        /// <summary>
        /// Create view model by sport type.
        /// Only used for rendering JSON result.
        /// </summary>
        /// <typeparam name="T">The DTO object.</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="sportType">The sport type.</param>
        /// <param name="language">The language code.</param>
        /// <returns>The JSON result.</returns>
        public string JSONSportTypeBuilder<T>(IList<T> dataList, SportType sportType, string language)
        {
            var lang = Resources.Instance.GetLanguage(language);
            var items = dataList
                .Select(i => ViewModelConcreteFactory.Instance.GetViewModel(i, sportType, language, lang))
                .ToList();
            return JsonConvert.SerializeObject(items);
        }

        /// <summary>
        /// Create parlay view model.
        /// Only used for rendering JSON result.
        /// </summary>
        /// <typeparam name="T">The DTO object.</typeparam>
        /// <param name="dataList">The DTO list.</param>
        /// <param name="language">The language code.</param>
        /// <returns>The JSON result.</returns>
        public string JSONParlayBuilder<T>(IList<T> dataList, string language)
        {
            var lang = Resources.Instance.GetLanguage(language);
            var items = dataList
                .Select(i => ViewModelConcreteFactory.Instance.GetParlayViewModel(i, language, lang)).ToList();
            var parlayItems = items.Select(i => (ParlayViewModel)i);
            return JsonConvert.SerializeObject(parlayItems);
        }

        public string JSONOutRightBuilder<T>(IList<T> dataList, string language)
        {
            var lang = Resources.Instance.GetLanguage(language);
            var items = dataList
                .Select(i => ViewModelConcreteFactory.Instance.GetOutRightViewModel(i, language, lang))
                .ToList();
            return JsonConvert.SerializeObject(items);
        }

        #endregion JsonBuilder

        private static RazorBinderModel CreateModel(LangResources lang, dynamic data, bool isPopUp, bool isParlay)
        {
            var model = new RazorBinderModel
             {
                 Resource = lang,
                 IsPopUp = isPopUp,
                 IsParlay = isParlay,
             };
            model.Data.AddRange(data);
            return model;
        }
    }
}