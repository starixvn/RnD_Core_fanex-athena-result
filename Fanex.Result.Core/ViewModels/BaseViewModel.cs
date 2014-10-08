// -----------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Utilities;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class BaseViewModel
    {
        public const string NA = "N/A";
        public const string HYPHEN = "-";

        private const string EnglishLanguageCode = "en-US";

        private static Dictionary<string, string> supportedLanguage = new
     Dictionary<string, string>
             {
        { "en-US", "en" },
        { "zh-TW", "ch" },
        { "zh-CN", "cs" },
        { "ja-JP", "jp" },
        { "th-TH", "th" },
        { "ko-KR", "ko" },
        { "vi-VN", "vn" }
        };

        private string _dateTimeFormat = "{0:MM/dd/yyyy<br/>hh:mm:ss tt}";

        private string _languageCode = EnglishLanguageCode;

        private LangResources _lang;
        private SportType _sportType = SportType.Soccer;

        public BaseViewModel(ResultSetting _setting)
        {
            this.DateTimeFormat = _setting.DateTimeFormat;
        }

        public string DateTimeFormat
        {
            get { return this._dateTimeFormat; }
            set { _dateTimeFormat = value; }
        }

        public string GetSupportedLanguage
        {
            get
            {
                return supportedLanguage.ContainsKey(this.LanguageCode) ? supportedLanguage[this.LanguageCode] : supportedLanguage[EnglishLanguageCode];
            }
        }

        public virtual string EventStatus
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual string KickOffTime
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual int LeagueId
        {
            get { return 0; }
        }

        public virtual string LeagueName
        {
            get
            {
                return GetLeagueNameByLanguage(this.LeagueId);
            }
        }

        public virtual string SportName
        {
            get
            {
                return this.GetSportNameByLanguage((int)this._sportType);
            }
        }

        protected string LanguageCode
        {
            get { return _languageCode; }
            set { _languageCode = value; }
        }

        protected LangResources Lang
        {
            get { return _lang; }
            set { _lang = value; }
        }

        protected SportType Sport
        {
            get { return _sportType; }
            set { _sportType = value; }
        }

        public virtual string Color(int index, string bg, string oddbg)
        {
            return index % 2 == 0 ? bg : oddbg;
        }

        protected virtual string GetLeagueNameByLanguage(int leagueId)
        {
            return RefData.GetLeagueName(
                leagueId.ToString(CultureInfo.InvariantCulture),
                this.GetSupportedLanguage,
                string.Empty);
        }

        protected virtual string GetSportNameByLanguage(int sportType)
        {
            return RefData.GetSportName(
                sportType.ToString(CultureInfo.InvariantCulture),
                this.GetSupportedLanguage,
                string.Empty);
        }

        protected virtual string GetFormatedKickOffTime(DateTime kickOffTime)
        {
            return Helper.FullDateFormat(kickOffTime, this.DateTimeFormat);
        }
    }
}