namespace Fanex.Result.Core.Base.Models
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Core.Models;
    using Exception;
    using Extension;    
    using ViewModels;
    using RazorEngine;
    using RazorEngine.Templating;
    
    public class BaseBuilder
    {
        protected virtual RazorBinderModel GenerateViewModelData<T>(IList<T> dataList, ResultSetting setting, ViewModelFactory viewModelFactory)
        {
            var groupData = dataList
                .Select(i => viewModelFactory.GetViewModel(i, setting))
                .GroupBy(i => i.LeagueId)
                .Select(i => new GroupData { LeagueId = i.Key, Data = i.ToSafeList() })
                .ToSafeList();
            return CreateModel(groupData, setting);
        }

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
                throw new ResultCoreException(
                    exception.Message, 
                    exception.InnerException,
                    templateName,
                    model);
            }
        }

        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <param name="groupData">The data list.</param>
        /// <param name="setting">The result setting.</param>
        /// <param name="isDetail">If true, all detailed games will be rendered.</param>
        /// <returns>
        /// Returns RAZOR model.
        /// </returns>
        protected virtual RazorBinderModel CreateModel(IList<GroupData> groupData, ResultSetting setting, bool isDetail = false)
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
                IsDetail = isDetail
            };

            model.Data = groupData;

            return model;
        }
    }
}