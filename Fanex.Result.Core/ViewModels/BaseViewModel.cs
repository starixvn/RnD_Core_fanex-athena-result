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

        protected static readonly Dictionary<string, string> TWLanguage = new
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

        private string _languageCode = "en-US";

        private LangResources _lang;

        protected BaseViewModel()
        {
        }

        public string GetTWLanguage
        {
            get
            {
                return TWLanguage[this.LanguageCode];
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
                return string.Empty;
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

        public virtual string Color(int index, string bg, string oddbg)
        {
            return index % 2 == 0 ? bg : oddbg;
        }

        public virtual string GetLeagueName(int leagueId)
        {
            return RefData.GetLeagueName(
                leagueId.ToString(CultureInfo.InvariantCulture),
                this.GetTWLanguage,
                string.Empty);
        }

        public virtual string GetKickOffTime(DateTime kickOffTime)
        {
            return Helper.FullDateFormat2Ln(kickOffTime);
        }
    }
}