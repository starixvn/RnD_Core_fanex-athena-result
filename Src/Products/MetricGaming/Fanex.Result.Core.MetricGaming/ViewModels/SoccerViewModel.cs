namespace Fanex.Result.Core.MetricGaming.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Base;
    using Base.DataDomain;
    using Base.Utilities;
    using Base.ViewModels;

    public class SoccerViewModel : BaseViewModel, IResultViewModel
    {
        private readonly DTO.MetricGaming _metricGaming;
        private string _eventName;
        private IList<string> _eventResult;

        public SoccerViewModel(DTO.MetricGaming metricGaming, ResultSetting setting)
            : this(metricGaming, setting, null)
        {
        }

        public SoccerViewModel(DTO.MetricGaming metricGaming, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(setting, refDataHelper)
        {
            _metricGaming = metricGaming;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
        }

        public override int LeagueId => _metricGaming.LeagueId;

        public string HomeTeam => RefDataHelper.GetTeamName(_metricGaming.HomeId, GetSupportedLanguage, string.Empty);

        public string AwayTeam => RefDataHelper.GetTeamName(_metricGaming.AwayId, GetSupportedLanguage, string.Empty);

        public int GameId => _metricGaming.GameId;

        public string EventName => _eventName ?? (_eventName = GetEventName());

        public IList<string> EventResult => _eventResult ?? (_eventResult = GetEventResult());

        public bool IsShowNoInfoRow
            => EventResult.Count == 0 || _metricGaming.SettlementStatusId == 0;

        private string GetEventName()
        {
            var gameName = RefDataHelper.GetBetTypeNameById(_metricGaming.GameId.ToString(CultureInfo.CurrentCulture), GetSupportedLanguage, string.Empty);

            return $"{gameName} {ComposeBetTypeTime(_metricGaming.BetTypeTime)}";
        }

        private string ComposeBetTypeTime(string bettypeTime)
        {
            if (string.IsNullOrEmpty(bettypeTime))
            {
                return string.Empty;
            }

            if (bettypeTime.IndexOf(",", StringComparison.InvariantCulture) > -1)
            {
                var tokens = bettypeTime.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                return RefDataHelper.GetTeamName(tokens[0], GetSupportedLanguage, string.Empty) + "," + tokens[1];
            }

            return bettypeTime;
        }

        private IList<string> GetEventResult()
        {
            var eventResult = new List<string>();

            if (!string.IsNullOrEmpty(_metricGaming.GameResult))
            {
                var gameResultToken = _metricGaming.GameResult.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var otherInforToken = GetOtherInfoList(_metricGaming.OtherInfo);

                for (var i = 0; i < gameResultToken.Length; i++)
                {
                    var resultItem = GetGameResult(gameResultToken[i]);
                    var timeOfResult = i < otherInforToken.Length ? otherInforToken[i] : string.Empty;
                    eventResult.Add(resultItem + " " + timeOfResult);
                }
            }

            return eventResult;
        }

        private string GetGameResult(string gameResourceId)
            => RefDataHelper.GetResourceDataByRefIdAndResourceId(ConstantValue.RefIDSuperLive, gameResourceId, GetSupportedLanguage, string.Empty);

        private static string[] GetOtherInfoList(string otherInfo)
        {
            if (!string.IsNullOrEmpty(otherInfo))
            {
                return otherInfo.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return new[] { string.Empty };
        }
    }
}