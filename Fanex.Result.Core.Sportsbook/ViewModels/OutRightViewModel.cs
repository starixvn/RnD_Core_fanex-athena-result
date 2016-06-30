namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System.Globalization;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.DTO;
    using RemotingRef;

    public sealed class OutrightViewModel : BaseViewModel, IResultViewModel
    {
        private readonly Outright _outRight;

        public OutrightViewModel(Outright outRight, ResultSetting setting)
            : base(setting)
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
                return MapEventStatus.GetLogResource(this._outRight.Status);
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