// -----------------------------------------------------------------------
// <copyright file="OutRightViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System.Globalization;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Utilities;
    using RemotingRef;

    /// <summary>
    /// Outright view model class.
    /// </summary>
    public sealed class OutRightViewModel : BaseViewModel,IResultViewModel
    {
        private readonly OutRight _outRight;

        public OutRightViewModel(OutRight outRight, ResultSetting setting, LangResources resource)
            : base(setting, resource)
        {
            this._outRight = outRight;
            this.LanguageCode = setting.LanguageCode;
        }

        public int SportId
        {
            get { return this._outRight.SportType; }
        }

        public override int LeagueId
        {
            get
            {
                return this._outRight.LeagueId;
            }
        }

        public override string LeagueName
        {
            get
            {
                return this.GetLeagueNameByLanguage(this._outRight.LeagueId);
            }
        }

        public override string SportName
        {
            get
            {
                return this.GetSportNameByLanguage(this._outRight.SportType);
            }
        }

        public string Team
        {
            get
            {
                return RefData.GetTeamName(this._outRight.TeamId.ToString(CultureInfo.InvariantCulture), this.GetSupportedLanguage, string.Empty);
            }
        }

        public override string EventStatus
        {
            get
            {
                switch (this._outRight.Status.ToLower())
                {
                    case "running":
                        return Lang.Running;
                    case "completed":
                        return Lang.Completed;
                    case "postponed":
                        return Lang.PostPoned;
                    case "internal":
                        return Lang.Internal;
                    case "refund":
                        return Lang.Refund;
                    case "refund ft":
                        return Lang.Refundft;
                    case "refund ht":
                        return Lang.Refundht;
                    case "won":
                        return Lang.Won;
                    default:
                        return this._outRight.Status;
                }
            }
        }

        public override string KickOffTime
        {
            get
            {
                return Helper.FullDateFormat(this._outRight.EventDate, this.DateTimeFormat);
            }
        }
    }
}