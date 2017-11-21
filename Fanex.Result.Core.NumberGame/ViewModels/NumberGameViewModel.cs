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
        private readonly DTO.NumberGame _numberGame;

        public NumberGameViewModel(DTO.NumberGame numberGame, ResultSetting setting)
            : this(numberGame, setting, null)
        {
        }

        public NumberGameViewModel(DTO.NumberGame numberGame, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(setting, refDataHelper)
        {
            _numberGame = numberGame;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
        }

        public override string KickOffTime => GetFormatedKickOffTime(_numberGame.KickOffTime);

        public override bool IsPending => _numberGame.IsPending;

        public override int LeagueId => _numberGame.LeagueId;

        public override string LeagueName => GetLeagueNameByLanguage(_numberGame.LeagueId);

        public string MatchCode => _numberGame.MatchCode;

        public string MatchId => _numberGame.MatchId.ToString(CultureInfo.InvariantCulture);

        public string GameNo => _numberGame.MatchCode;

        public string FirstBall
            => _numberGame.Ball.Count > 0 ? Helper.FormatTwoDigit(_numberGame.Ball[0]) : ConstantValue.Hyphen;

        public string LastBall
            => _numberGame.Ball.Count > 0 ? Helper.FormatTwoDigit(_numberGame.Ball[_numberGame.Ball.Count - 1]) : ConstantValue.Hyphen;

        public string Warrior
            => _numberGame.Ball.Count > 2 ? _numberGame.Ball[1] > _numberGame.Ball[2]
                       ? LangResource.Warrior2ndBall : LangResource.Warrior3rdBall :
                       ConstantValue.Hyphen;

        public string OEFT
            => (uint)_numberGame.Ball.Sum() % 2 == 1 ? LangResource.Odd : LangResource.Even;

        public IList<string> Result
            => _numberGame.Ball.Select(i => Helper.FormatTwoDigit(i)).ToSafeList();

        public override string EventStatus
            => MapEventStatus.MapEventStatusString(_numberGame.EventStatus, _numberGame.IsPending, VisibleInternalEventStatus);

        public static string ShowBall(int ball)
            => Helper.FormatTwoDigit(ball);
    }
}