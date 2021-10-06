namespace Fanex.Result.Core.Happy5.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.Utilities;
    using Base.ViewModels;
    using DataDomain;
    using Fanex.Result.Core.Happy5.Resources;

    public sealed class Happy5ViewModel : BaseViewModel, IResultViewModel
    {
        private const string RedBallColor = "red";
        private const string BlueBallColor = "blue";
        private const string Big = "B";
        private const string Small = "S";
        private const string Even = "E";
        private const string Odd = "O";
        private const string MoreOdd = "MO";
        private const string MoreEven = "ME";
        private const string Spring = "Sp";
        private const string Summer = "Su";
        private const string Autumn = "Au";
        private const string Winter = "Wi";

        private readonly DTO.Happy5 resultData;

        public Happy5ViewModel(DTO.Happy5 resultData, ResultSetting setting, IRefDataHelper refDataHelper)
            : base(setting, refDataHelper)
        {
            this.resultData = resultData;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
        }

        public override string KickOffTime => GetFormatedKickOffTime(resultData.KickOffTime);

        public override int LeagueId => resultData.LeagueId;

        public string Match => $"{Happy5Resources.No} {resultData.MatchCode}";

        public int FirstBall => GetBall(0);

        public string FirstBallColor => GetBallColor(FirstBall);

        public int SecondBall => GetBall(1);

        public string SecondBallColor => GetBallColor(SecondBall);

        public int ThirdBall => GetBall(2);

        public string ThirdBallColor => GetBallColor(ThirdBall);

        public int FourthBall => GetBall(3);

        public string FourthBallColor => GetBallColor(FourthBall);

        public int FifthBall => GetBall(4);

        public string FifthBallColor => GetBallColor(FifthBall);

        public string TotalBall => EventStatusIsRefundOrPending ? Happy5Resources.NA : Total.ToString();

        public string BigSmall => EventStatusIsRefundOrPending ? Happy5Resources.NA : GetBigSmallResult();

        public string OddEven => EventStatusIsRefundOrPending ? Happy5Resources.NA : GetEvenOddResult();

        public string FourSeason => EventStatusIsRefundOrPending ? Happy5Resources.NA : GetFourSeasonResult(Total);

        public string MoreOddMoreEven => EventStatusIsRefundOrPending ? Happy5Resources.NA : GetMoreOddMoreEvenResult();

        public string Soccer5 => EventStatusIsRefundOrPending ? Happy5Resources.NA : string.Empty;

        public string Soccer5Class => EventStatusIsRefundOrPending ? string.Empty : "info";

        public string Soccer5Function
            => EventStatusIsRefundOrPending ? 
            string.Empty : 
            $"onclick=\"showSoccer5Result({resultData.MatchId}, '{Balls}');\"";

        public string Combo => EventStatusIsRefundOrPending ? Happy5Resources.NA : BigSmall + OddEven;

        public override string EventStatus
            => MapEventStatus.MapEventStatusString(resultData.EventStatus, resultData.IsPending);

        public bool EventStatusIsRefundOrPending
            => resultData.EventStatus.Equals(ConstantValue.Refund, StringComparison.OrdinalIgnoreCase) ||
                resultData.EventStatus.Equals(ConstantValue.RefundFT, StringComparison.OrdinalIgnoreCase) ||
                resultData.EventStatus.Equals(ConstantValue.RefundHT, StringComparison.OrdinalIgnoreCase) ||
                resultData.IsPending;

        public string MatchCode => resultData.MatchCode;

        public string MatchId => resultData.MatchId.ToString(CultureInfo.InvariantCulture);

        public override string LeagueName => GetLeagueNameByLanguage(resultData.LeagueId);

        public override bool IsPending => resultData.IsPending;

        private static string GetBallColor(int ballNumber)
             => Constant.Happy5RedBalls.Contains(ballNumber) ? RedBallColor : BlueBallColor;

        private int GetBall(int index)
           => (resultData.Ball.Count > 0 && index < resultData.Ball.Count) ?
                resultData.Ball[index] : 0;

        public string Balls => string.Join(", ", resultData.Ball);

        private string GetBigSmallResult() => Total >= 53 ? Big : Small;

        private string GetEvenOddResult() => Total % 2 == 0 ? Even : Odd;

        private string GetMoreOddMoreEvenResult() => CountOdd(resultData.Ball) > 2 ? MoreOdd : MoreEven;

        private static string GetFourSeasonResult(int totalBall)
        {
            if (totalBall >= 15 && totalBall <= 33)
            {
                return Spring;
            }

            if (totalBall >= 34 && totalBall <= 52)
            {
                return Summer;
            }

            if (totalBall >= 53 && totalBall <= 71)
            {
                return Autumn;
            }

            if (totalBall >= 72 && totalBall <= 90)
            {
                return Winter;
            }

            return Happy5Resources.NA;
        }

        private static int CountOdd(IList<int> ball)
        {
            int result = 0;

            for (int i = 0; i < ball.Count; i++)
            {
                if (ball[i] % 2 != 0)
                {
                    result++;
                }
            }

            return result;
        }

        private int Total => FirstBall + SecondBall + ThirdBall + FourthBall + FifthBall;
    }
}