// -----------------------------------------------------------------------
// <copyright file="BaseBuilder.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.Models
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Models;
    using RazorEngine;
    using RazorEngine.Templating;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseBuilder
    {
        protected virtual RazorBinderModel GenerateViewModelData<T>(IList<T> dataList, Base.ResultSetting setting, ViewModelFactory viewModelFactory)
        {
            var lang = Resources.Instance.GetLanguage(setting.LanguageCode, setting.ResourcePath);
            var groupData = dataList.Select(i => viewModelFactory.GetViewModel(i, setting, lang))
                .GroupBy(i => i.LeagueId).Select(i => new GroupData { LeagueId = i.Key, Data = i.ToList() });
            return CreateModel(lang, groupData, setting);
        }

        /// <summary>
        /// Render html result.
        /// </summary>
        /// <param name="templateInstance">The template instance.</param>
        /// <param name="templateName">The template name.</param>
        /// <param name="model">The binder model.</param>
        /// <returns>The HTML result.</returns>
        protected virtual string HtmlRender(ITemplate templateInstance, string templateName, RazorBinderModel model)
        {
            var templateContent = templateInstance.GetRazorTemplate(templateName);
            var result = new StringBuilder();
            using (var writer = new StringWriter(result))
            {
                Engine.Razor.RunCompile(templateContent, templateName, writer, typeof(RazorBinderModel), model, null);
            }

            return result.ToString();
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
        protected virtual RazorBinderModel CreateModel(
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