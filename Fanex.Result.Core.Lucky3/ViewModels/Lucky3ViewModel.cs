namespace Fanex.Result.Core.Lucky3.ViewModels
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.Utilities;
    using Base.ViewModels;
    using DataDomain;
    using Resources;

    public sealed class Lucky3ViewModel : BaseViewModel, IResultViewModel
    {
        private const string RedBallColor = "red";
        private const string BlueBallColor = "blue";
        private readonly DTO.Lucky3 resultData;

        public Lucky3ViewModel(DTO.Lucky3 resultData, ResultSetting setting, IRefDataHelper refDataHelper)
            : base(setting, refDataHelper)
        {
            this.resultData = resultData;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
        }

        public override string KickOffTime => GetFormatedKickOffTime(resultData.KickOffTime);

        public override int LeagueId => resultData.LeagueId;

        public string Match => $"{Lucky3Resources.Lucky3GameNo} {resultData.MatchCode}";

        public int FirstBall => GetBall(0);

        public string FirstBallColor => GetBallColor(FirstBall);

        public int SecondBall => GetBall(1);

        public string SecondBallColor => GetBallColor(SecondBall);

        public int ThirdBall => GetBall(2);

        public string ThirdBallColor => GetBallColor(ThirdBall);

        public string FirstPrize => EventStatusIsRefund ? ConstantValue.Hyphen : resultData.LiveHomeScore.ToString();

        public string SecondPrize => EventStatusIsRefund ? ConstantValue.Hyphen : resultData.LiveAwayScore.ToString();

        public string ThirdPrize => EventStatusIsRefund ? ConstantValue.Hyphen : resultData.FinalHomeScore.ToString();

        public override string EventStatus
            => MapEventStatus.MapEventStatusString(resultData.SettlementStatus, resultData.EventStatus, resultData.BetStatus);

        public bool EventStatusIsRefund
            => EventStatus.Equals(ConstantValue.Refund, StringComparison.OrdinalIgnoreCase) ||
                EventStatus.Equals(ConstantValue.RefundFT, StringComparison.OrdinalIgnoreCase) ||
                EventStatus.Equals(ConstantValue.RefundHT, StringComparison.OrdinalIgnoreCase);

        public string MatchCode => resultData.MatchCode;

        public string MatchId => resultData.MatchId.ToString(CultureInfo.InvariantCulture);

        public override string LeagueName => GetLeagueNameByLanguage(resultData.LeagueId);

        private static string GetBallColor(int ballNumber)
             => Constant.Lucky3RedBalls.Contains(ballNumber) ? RedBallColor : BlueBallColor;

        private int GetBall(int index)
           => (resultData.Ball.Count > 0 && index < resultData.Ball.Count) ?
                resultData.Ball[index] : 0;
    }
}