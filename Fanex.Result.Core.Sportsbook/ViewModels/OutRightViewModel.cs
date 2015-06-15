// -----------------------------------------------------------------------
// <copyright file="OutRightViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Globalization;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.DTO;
    using RemotingRef;

    /// <summary>
    /// Outright view model class.
    /// </summary>
    public sealed class OutRightViewModel : BaseViewModel, IResultViewModel
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
                if (string.IsNullOrEmpty(this._outRight.Status))
                {
                    return string.Empty;
                }

                var keyStatus = this._outRight.Status.Trim().ToLower();
                switch (keyStatus)
                {
                    case ConstantValue.RunningStatus:
                        return Lang.Running;
                    case ConstantValue.CompletedStatus:
                        return Lang.Completed;
                    case ConstantValue.ClosedStatus:
                        return Lang.Closed;
                    case ConstantValue.PostponedStatus:

                        return Lang.PostPoned;
                    case ConstantValue.InternalString:
                        return Lang.Internal;
                    case ConstantValue.RefundStatus:
                        return Lang.Refund;
                    case ConstantValue.RefundftStatus:
                        return Lang.Refundft;
                    case ConstantValue.RefundhtStatus:
                        return Lang.Refundht;
                    case ConstantValue.Won:
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