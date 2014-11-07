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
                    case ConstantValueDefinition.Running:
                        return Lang.Running;
                    case ConstantValueDefinition.Completed:
                        return Lang.Completed;
                    case ConstantValueDefinition.Closed:
                        return Lang.Closed;
                    case ConstantValueDefinition.Postponed:

                        return Lang.PostPoned;
                    case ConstantValueDefinition.InternalString:
                        return Lang.Internal;
                    case ConstantValueDefinition.Refund:
                        return Lang.Refund;
                    case ConstantValueDefinition.Refundft:
                        return Lang.Refundft;
                    case ConstantValueDefinition.Refundht:
                        return Lang.Refundht;
                    case ConstantValueDefinition.Won:
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