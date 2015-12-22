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
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Extension;
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
            var groupData = dataList.Select(i => viewModelFactory.GetViewModel(i, setting))
                .GroupBy(i => i.LeagueId).Select(i => new GroupData { LeagueId = i.Key, Data = i.ToSafeList() }).ToSafeList();
            return CreateModel(groupData, setting);
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
            try
            {
                var templateContent = templateInstance.GetRazorTemplate(templateName);
                var result = new StringBuilder();
                using (var writer = new StringWriter(result))
                {
                    var typeOfBinderModel = typeof(RazorBinderModel);
                    if (!Engine.Razor.IsTemplateCached(templateName, typeOfBinderModel))
                    {
                        Engine.Razor.AddTemplate(templateName, templateContent);
                        Engine.Razor.Compile(templateName, typeOfBinderModel);
                    }

                    Engine.Razor.Run(templateName, writer, typeOfBinderModel, model);
                }

                return result.ToString();
            }
            catch (System.Exception exception)
            {
                throw new ResultCoreException(exception.Message, exception.InnerException);
            }
        }

        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <param name="groupData">The data list.</param>
        /// <param name="setting">The result setting.</param>
        /// <param name="isDetail">If true,all detailed game will be rendered.</param>
        /// <returns>
        /// Returns RAZOR model.
        /// </returns>
        protected virtual RazorBinderModel CreateModel(
            IList<GroupData> groupData,
            ResultSetting setting,
            bool isDetail = false)
        {
            var isPopUp = setting.IsParlayPopUp || setting.IsPopUp;
            var model = new RazorBinderModel
            {
                IsPopUp = isPopUp,
                VisiblePopUpTitle = setting.VisiblePopUpTitle,
                SportType = setting.SportType,
                VisibleMatchCodeIDColumn = setting.VisibleMatchCodeIDColumn,
                VisibleDetailedLink = setting.VisibleDetailedLink,
                VisibleFreeHeader = setting.VisibleFreeHeader && !isPopUp,
                IsDetail = isDetail,
            };

            model.Data = groupData;

            return model;
        }
    }
}