namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.Resources;
    using Base.ViewModels;
    using BaseDetail;
    using DataDomain;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Models;
    using RazorEngine;
    using RazorEngine.Templating;
    using SoccerDetail;
    using Utilities;

    public sealed class CricketViewModel : SportsbookBaseViewModel, IResultViewModel
    {
        private const string TemplateNameSpace = "CricketDetail.";

        private IList<SoccerGameDetailItem> _detailedItemModels;
        private readonly IResultDetail<SoccerGameDetailItem> _resultDetail;

        public CricketViewModel(Sportsbook cricket, ResultSetting setting)
            : this(cricket, setting, null, null)
        {
        }

        public CricketViewModel(
            Sportsbook cricket,
            ResultSetting setting,
            IResultDetail<SoccerGameDetailItem> resultDetail,
            IRefDataHelper refDataHelper)
            : base(cricket, setting, refDataHelper)
        {
            _resultDetail = resultDetail ?? new CricketResultDetail(cricket, setting);
            _detailedItemModels = new SafeList<SoccerGameDetailItem>();
        }

        public int HomeId => SPORTBOOK.HomeId;

        public int AwayId => SPORTBOOK.AwayId;

        public override bool VisibleDetailedLink
            => base.VisibleDetailedLink && HasValidGameData;

        public override bool VisibleGameDetail => HasValidGameData;

        public string HomeFirstRuns => GetRuns(CricketResourceID.HomeFirstScore, isAwayTeam: false, isFinalScore: false);

        public string HomeSecondRuns => GetRuns(CricketResourceID.HomeSecondScore, isAwayTeam: false, isFinalScore: false);

        public string AwayFirstRuns => GetRuns(CricketResourceID.AwayFirstScore, isAwayTeam: true, isFinalScore: false);

        public string AwaySecondRuns => GetRuns(CricketResourceID.AwaySecondScore, isAwayTeam: true, isFinalScore: false);

        public string HomeFinalRuns => GetRuns(CricketResourceID.FinalScore, isAwayTeam: false, isFinalScore: true);

        public string AwayFinalRuns => GetRuns(CricketResourceID.FinalScore, isAwayTeam: true, isFinalScore: true);

        public string DetailedResult
        {
            get
            {
                GetDetailedResult();

                var temp = new StringBuilder();
                foreach (var item in _detailedItemModels)
                {
                    var templateName = TemplateNameSpace + item.GroupCode;

                    var templateByGroup = Template.Instance.GetRazorTemplate(templateName);

                    if (!string.IsNullOrEmpty(templateByGroup))
                    {
                        var binderModel = new SoccerDetailViewModel
                        {
                            Name = item.GroupName,
                            DetailItems = new SafeList<SoccerGameDetailItem> { item }
                        };
                        var result = new StringBuilder();

                        using (var writer = new StringWriter(result))
                        {
                            Engine.Razor.RunCompile(templateByGroup, templateName, writer, typeof(SoccerDetailViewModel), binderModel);
                        }

                        temp.Append(result.ToString());
                    }
                }

                return temp.ToString();
            }
        }

        #region Utilities

        private bool HasValidGameData
            => SPORTBOOK.GameDetails.Any(i => !string.IsNullOrEmpty(i.ResourceId) && IsValidResourceId(i.ResourceId));

        private string GetRuns(string resourceId, bool isAwayTeam, bool isFinalScore)
        {
            var score = SPORTBOOK.GameDetails.FirstOrDefault(i => SportsbookHelper.IsValidResourceId(resourceId, i.ResourceId));
            if (score != null)
            {
                if (score.SettlementStatusID == EventStatusID.RefundSettlementStatusID)
                {
                    return LangResource.Refund;
                }

                if (score.SettlementStatusID == EventStatusID.RunningSettlementStatusID)
                {
                    return ConstantValue.Hyphen;
                }

                return isFinalScore && isAwayTeam ? score.AwayValue : score.HomeValue;
            }

            return GetOldData(isFinalScore, isAwayTeam);
        }

        private string GetOldData(bool isFinalScore, bool isAwayTeam)
        {
            if (isFinalScore)
            {
                return isAwayTeam ? GetScore(SPORTBOOK.FinalAwayScore) : GetScore(SPORTBOOK.FinalHomeScore);
            }

            return ConstantValue.Hyphen;
        }

        private static bool IsValidResourceId(string resourceId)
        {
            return CricketResourceID.MoreResultGroup.Values.Any(pattern => IsValidResourceIdByPattern(pattern, resourceId));

            bool IsValidResourceIdByPattern(List<string> patterns, string id)
                => patterns.Any(i => SportsbookHelper.IsValidResourceId(i, id));
        }

        private void GetDetailedResult()
        {
            _detailedItemModels = _resultDetail.GetDetailItems();
            _detailedItemModels = _detailedItemModels.OrderBy(item => item.ResourceID).ToList();
        }

        #endregion Utilities
    }
}