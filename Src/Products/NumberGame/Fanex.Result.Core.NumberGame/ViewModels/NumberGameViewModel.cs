namespace Fanex.Result.Core.NumberGame.ViewModels
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Base.DataDomain;
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
            => numberGame.Ball.Count > 0 ? Helper.FormatTwoDigit(numberGame.Ball[0]) : ConstantValue.DoubleZero;

        public string SecondBall
        {
            get
            {
                const int secondBallIndex = 2;

                return numberGame.Ball.Count >= secondBallIndex ? Helper.FormatTwoDigit(numberGame.Ball[secondBallIndex - 1]) : ConstantValue.DoubleZero;
            }
        }

        public string ThirdBall
        {
            get
            {
                const int thirdBallIndex = 3;

                return numberGame.Ball.Count >= thirdBallIndex ? Helper.FormatTwoDigit(numberGame.Ball[thirdBallIndex - 1]) : ConstantValue.DoubleZero;
            }
        }
        
        public string OEFT
            => (uint)numberGame.Ball.Sum() % 2 == 1 ? LangResource.Odd : LangResource.Even;        

        public override string EventStatus
            => MapEventStatus.MapEventStatusString(numberGame.EventStatus, numberGame.IsPending);

        public static string GetBallColor(string ballNumber)
        {
            if (ballNumber.Equals(ConstantValue.DoubleZero))
            {
                return "disable";
            }

            var redBall = new List<string> { "01", "03", "04", "06", "08", "10", "12", "15", "17", "20",
                "21", "23", "26", "29", "31", "33", "34", "35", "38", "40", "42", "44", "45", "47", "49",
                "51", "53", "56", "58", "59", "62", "65", "67", "70", "71", "73", "74" };

            return redBall.Contains(ballNumber) ? "red" : "blue";
        }
    }
}