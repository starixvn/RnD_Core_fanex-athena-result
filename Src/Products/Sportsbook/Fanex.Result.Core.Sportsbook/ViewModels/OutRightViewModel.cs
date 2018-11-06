namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using Base;
    using Base.Utilities;
    using Base.ViewModels;
    using DTO;
    using Utilities;

    public sealed class OutrightViewModel : BaseViewModel, IResultViewModel
    {
        private readonly Outright _outRight;

        public OutrightViewModel(Outright outRight, ResultSetting setting)
            : this(outRight, setting, null)
        {
        }

        public OutrightViewModel(Outright outRight, ResultSetting setting, IRefDataHelper refDataHelper)
            : base(setting, refDataHelper)
        {
            _outRight = outRight;
            LanguageCode = setting.LanguageCode;
        }

        public int SportId => _outRight.SportType;

        public override int LeagueId => _outRight.LeagueId;

        public override string LeagueName => GetLeagueNameByLanguage(_outRight.LeagueId);

        public override string SportName => GetSportNameByLanguage(_outRight.SportType);

        public string Team => RefDataHelper.GetTeamName(_outRight.TeamId, GetSupportedLanguage, string.Empty);

        public override string EventStatus => MapEventStatus.GetLogResource(_outRight.Status);

        public override string KickOffTime => Helper.FullDateFormat(_outRight.EventDate, DateTimeFormat);
    }
}