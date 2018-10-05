namespace Fanex.Result.Core.NumberGame.ViewModels
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;

    public sealed class NumberGameViewModel : BaseViewModel, IResultViewModel
    {
        private readonly DTO.NumberGame numberGame;

        public NumberGameViewModel(DTO.NumberGame numberGame, ResultSetting setting)
            : this(numberGame, setting, null)
        {
        }

        public NumberGameViewModel(DTO.NumberGame numberGame, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(setting, refDataHelper)
        {
            this.numberGame = numberGame;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
        }

        public override string KickOffTime => GetFormatedKickOffTime(numberGame.KickOffTime);

        public override int LeagueId => numberGame.LeagueId;

        public override string LeagueName => GetLeagueNameByLanguage(numberGame.LeagueId);

        public string MatchCode => numberGame.MatchCode;

        public string MatchId => numberGame.MatchId.ToString(CultureInfo.InvariantCulture);

        public string GameNo => numberGame.MatchCode;

        public string FirstBall
            => numberGame.Ball.Count > 0 ? Helper.FormatTwoDigit(numberGame.Ball[0]) : ConstantValue.Hyphen;

        public string LastBall
            => numberGame.Ball.Count > 0 ? Helper.FormatTwoDigit(numberGame.Ball[numberGame.Ball.Count - 1]) : ConstantValue.Hyphen;

        public string Warrior
            => numberGame.Ball.Count > 2 ? numberGame.Ball[1] > numberGame.Ball[2]
                       ? LangResource.Warrior2ndBall : LangResource.Warrior3rdBall :
                       ConstantValue.Hyphen;

        public string OEFT
            => (uint)numberGame.Ball.Sum() % 2 == 1 ? LangResource.Odd : LangResource.Even;

        public IList<string> Result
            => numberGame.Ball.Select(i => Helper.FormatTwoDigit(i)).ToSafeList();

        public override string EventStatus
            => MapEventStatus.MapEventStatusString(numberGame.SettlementStatus, numberGame.EventStatus, numberGame.BetStatus);

        public static string ShowBall(int ball)
            => Helper.FormatTwoDigit(ball);
    }
}