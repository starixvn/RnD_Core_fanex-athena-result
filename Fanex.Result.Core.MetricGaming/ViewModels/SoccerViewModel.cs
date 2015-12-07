// -----------------------------------------------------------------------
// <copyright file="SoccerViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.MetricGaming.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ViewModels;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SoccerViewModel : BaseViewModel, IResultViewModel
    {
        private readonly DTO.MetricGaming _metricGaming;
        private string _eventName;
        private IList<string> _eventResult = null;

        public SoccerViewModel(DTO.MetricGaming metricGaming, ResultSetting setting)
            : base(setting)
        {
            this._metricGaming = metricGaming;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
        }

        public override int LeagueId
        {
            get
            {
                return _metricGaming.LeagueId;
            }
        }

        public string HomeTeam
        {
            get
            {
                return RefData.GetTeamName(_metricGaming.HomeId, GetSupportedLanguage, string.Empty);
            }
        }

        public string AwatTeam
        {
            get
            {
                return RefData.GetTeamName(_metricGaming.AwayId, GetSupportedLanguage, string.Empty);
            }
        }

        public int GameId
        {
            get
            {
                return this._metricGaming.GameId;
            }
        }

        public string EventName
        {
            get
            {
                if (string.IsNullOrEmpty(_eventName))
                {
                    var gameName = RefData.GetBetTypeName(this._metricGaming.GameId.ToString(CultureInfo.CurrentCulture), this.GetSupportedLanguage, string.Empty);
                    this._eventName = gameName + " " + ComposeBetTypeTime(this._metricGaming.BetTypeTime);
                }

                return this._eventName;
            }
        }

        public IList<string> EventResult
        {
            get
            {
                if (this._eventResult == null)
                {
                    GetEventResult();
                }

                return _eventResult;
            }
        }

        public bool IsShowNoInfoRow
        {
            get
            {
                return this.EventResult.Count == 0 || this._metricGaming.SettlementStatusId == 0;
            }
        }

        private string ComposeBetTypeTime(string bettypeTime)
        {
            if (string.IsNullOrEmpty(bettypeTime))
            {
                return string.Empty;
            }

            if (bettypeTime.IndexOf(",") > -1)
            {
                var tokens = bettypeTime.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                return RefData.GetTeamName(tokens[0], this.GetSupportedLanguage, string.Empty) + "," + tokens[1];
            }

            return bettypeTime;
        }

        private static string[] GetOtherInforList(string otherInfo)
        {
            if (!string.IsNullOrEmpty(otherInfo))
            {
                return otherInfo.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return new string[1] { string.Empty };
        }

        private string GetGameResult(string gameResourceId)
        {
            return RefData.GetResourceDataByRefIdAndResourceId(ConstantValue.RefIDSuperLive, gameResourceId, this.GetSupportedLanguage, string.Empty);
        }

        private void GetEventResult()
        {
            this._eventResult = new List<string>();

            if (!string.IsNullOrEmpty(this._metricGaming.GameResult))
            {
                var gameResultToken = this._metricGaming.GameResult.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                string[] otherInforToken = GetOtherInforList(this._metricGaming.OtherInfo);

                for (var i = 0; i < gameResultToken.Length; i++)
                {
                    var resultItem =
                       GetGameResult(gameResultToken[i]);
                    var timeOfResult = i < otherInforToken.Length ? otherInforToken[i] : string.Empty;
                    this._eventResult.Add(resultItem + " " + timeOfResult);
                }
            }
        }
    }
}