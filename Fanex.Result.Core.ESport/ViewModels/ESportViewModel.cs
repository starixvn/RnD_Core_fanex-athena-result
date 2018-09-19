namespace Fanex.Result.Core.ESport.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.ESport.DTO;

    public class ESportViewModel : BaseViewModel, IResultViewModel
    {
        public const int MaxNumberOfMaps = 9;

        public ESportViewModel(
            ESport eSportModel,
            ResultSetting setting,
            IRefDataHelper refDataHelper = null)
            : base(setting, refDataHelper)
        {
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
            VisibleDetailedLink = setting.VisibleDetailedLink;
            Setting = setting;
            ESportModel = eSportModel;
        }

        public bool VisibleDetailedLink { get; }

        public ResultSetting Setting { get; }

        public ESport ESportModel { get; set; }

        public IDictionary<int, IEnumerable<MapResult>> MoreResults { get; set; } 
            = new Dictionary<int, IEnumerable<MapResult>>();

        public override string EventStatus
            => MapEventStatus.MapEventStatusString(
                    ESportModel.EventStatus,
                    ESportModel.IsPending,
                    VisibleInternalEventStatus);

        private void BuildMoreResults()
        {
            if(ESportModel.Maps.Any())
            {

            }
        }

    }
}