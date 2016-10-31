namespace Fanex.Result.Core.Base.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using DataDomain;
    using RemotingRef;
    using Resources;
    using Utilities;

    public abstract class BaseViewModel
    {
        private static Dictionary<string, string> _supportedLanguage = new Dictionary<string, string>
        {
            { "en-US", "en" },
            { "zh-TW", "ch" },
            { "zh-CN", "cs" },
            { "zh-Hans", "zhcn" },
            { "ja-JP", "jp" },
            { "th-TH", "th" },
            { "ko-KR", "ko" },
            { "vi-VN", "vn" }
        };

        protected BaseViewModel(ResultSetting setting)
        {
            this.DateTimeFormat = setting.DateTimeFormat;
            this.VisibleInternalEventStatus = setting.VisibleInternalEventStatus;
        }

        public string DateTimeFormat { get; set; } = "{0:MM/dd/yyyy<br/>hh:mm:ss tt}";

        public bool VisibleInternalEventStatus { get; private set; }

        public string GetSupportedLanguage
        {
            get
            {
                return _supportedLanguage.ContainsKey(this.LanguageCode) ? _supportedLanguage[this.LanguageCode] : _supportedLanguage[ConstantValue.EnglishLanguageCode];
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
                return this.GetSportNameByLanguage((int)Sport);
            }
        }

        protected string LanguageCode { get; set; } = ConstantValue.EnglishLanguageCode;

        protected SportType Sport { get; set; } = SportType.Soccer;

        public virtual string Color(int index, string bg, string oddbg)
        {
            return index % 2 == 0 ? bg : oddbg;
        }

        protected virtual string GetLeagueNameByLanguage(int leagueId)
        {
            return RefData.GetLeagueName(leagueId.ToString(CultureInfo.InvariantCulture), GetSupportedLanguage, string.Empty);
        }

        protected virtual string GetSportNameByLanguage(int sportType)
        {
            return RefData.GetSportName(sportType.ToString(CultureInfo.InvariantCulture), GetSupportedLanguage, string.Empty);
        }

        protected virtual string GetFormatedKickOffTime(DateTime kickOffTime)
        {
            return Helper.FullDateFormat(kickOffTime, this.DateTimeFormat);
        }
    }
}