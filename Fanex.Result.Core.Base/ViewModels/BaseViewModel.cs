namespace Fanex.Result.Core.Base.ViewModels
{
    using System;
    using System.Collections.Generic;
    using DataDomain;
    using Resources;
    using Utilities;

#pragma warning disable S1694 // An abstract class should have both abstract and concrete methods

    public abstract class BaseViewModel
#pragma warning restore S1694 // An abstract class should have both abstract and concrete methods
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

        protected BaseViewModel(ResultSetting setting) : this(setting, null)
        {
        }

        protected BaseViewModel(ResultSetting setting, IRefDataHelper refDataHelper)
        {
            DateTimeFormat = setting.DateTimeFormat;
            VisibleInternalEventStatus = setting.VisibleInternalEventStatus;
            RefDataHelper = refDataHelper ?? ResultCoreManager.RefDataHelper;
        }

        public string DateTimeFormat { get; set; }

        public bool VisibleInternalEventStatus { get; private set; }

        public IRefDataHelper RefDataHelper { get; }

        public string GetSupportedLanguage
            => _supportedLanguage.ContainsKey(LanguageCode) ?
            _supportedLanguage[LanguageCode] :
            _supportedLanguage[ConstantValue.EnglishLanguageCode];

        public virtual string EventStatus => string.Empty;

        public virtual bool IsPending => false;

        public virtual bool VisibleMatchResult => true;

        public virtual bool VisibleGameDetail => false;

        public virtual string EventStatusBackground
            => EventStatus.Equals(LangResource.PostPoned, StringComparison.OrdinalIgnoreCase) ?
                    ConstantValue.BackgroundPosponed :
                        IsPending ? ConstantValue.BackgroundPending : string.Empty;

        public virtual string KickOffTime => string.Empty;

        public virtual int LeagueId => 0;

        public virtual string LeagueName => GetLeagueNameByLanguage(LeagueId);

        public virtual string SportName => GetSportNameByLanguage((int)Sport);

        protected string LanguageCode { get; set; } = ConstantValue.EnglishLanguageCode;

        protected SportType Sport { get; set; } = SportType.Soccer;

        public virtual string Color(int index, string bg, string oddbg)
            => index % 2 == 0 ? bg : oddbg;

        protected virtual string GetLeagueNameByLanguage(int leagueId)
            => RefDataHelper.GetLeagueName(leagueId, GetSupportedLanguage, string.Empty);

        protected virtual string GetSportNameByLanguage(int sportType)
            => RefDataHelper.GetSportName(sportType, GetSupportedLanguage, string.Empty);

        protected virtual string GetFormatedKickOffTime(DateTime kickOffTime)
            => Helper.FullDateFormat(kickOffTime, DateTimeFormat);
    }
}