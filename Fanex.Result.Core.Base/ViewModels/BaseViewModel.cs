// -----------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class BaseViewModel
    {
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

        private string _languageCode = ConstantValue.EnglishLanguageCode;

        private SportType _sportType = SportType.Soccer;

        protected BaseViewModel(ResultSetting setting)
        {
            this.DateTimeFormat = setting.DateTimeFormat;
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
                return supportedLanguage.ContainsKey(this.LanguageCode) ? supportedLanguage[this.LanguageCode] : supportedLanguage[ConstantValue.EnglishLanguageCode];
            }
        }

        public virtual string EventStatus
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual bool IsPending
        {
            get
            {
                return false;
            }
        }

        public bool VisibleMatchResult
        {
            get { return true; }
        }

        public bool VisibleGameDetail
        {
            get { return false; }
        }

        public virtual string EventStatusBackground
        {
            get
            {
                if (this.EventStatus.Equals(LangResource.PostPoned, StringComparison.OrdinalIgnoreCase))
                {
                    return ConstantValue.BackgroundPosponed;
                }
                else
                {
                    return this.IsPending ? ConstantValue.BackgroundPending : string.Empty;
                }
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