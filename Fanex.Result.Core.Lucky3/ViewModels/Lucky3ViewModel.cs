namespace Fanex.Result.Core.Lucky3.ViewModels
{
    using System;
    using System.Linq;
    using Base.ViewModels;
    using Base;
    using Base.Utilities;
    using Base.DataDomain;
    using DataDomain;

    public sealed class Lucky3ViewModel : BaseViewModel, IResultViewModel
    {
        private readonly DTO.Lucky3 resultData;

        public Lucky3ViewModel(DTO.Lucky3 resultData, ResultSetting setting) : base(setting)
        {
            this.resultData = resultData;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
        }

        public override string KickOffTime => GetFormatedKickOffTime(resultData.KickOffTime);

        public string Match => $"Lucky 3 Game No {resultData.MatchCode}";

        public int FirstBall => GetBall(0);

        public int SecondBall => GetBall(1);

        public int ThirdBall => GetBall(2);

        public string GetBallColor(int index)
            => Constant.RedBalls.Contains(index) ? "red" : "blue";

        public string FirstPrize => EventStatusIsRefund ? "N/A" : resultData.LiveHomeScore.ToString();

        public string SecondPrize => EventStatusIsRefund ? "N/A" : resultData.LiveAwayScore.ToString();

        public string ThirdPrize => EventStatusIsRefund ? "N/A" : resultData.FinalHomeScore.ToString();

        public override string EventStatus => MapEventStatus.MapEventStatusString(resultData.EventStatus, resultData.IsPending, VisibleInternalEventStatus);

        public bool EventStatusIsRefund => resultData.EventStatus.Equals(ConstantValue.Refund, StringComparison.OrdinalIgnoreCase);

        public int GetBall(int index)
           => resultData.Ball.Count > 0 ? resultData.Ball[index] : 0;
    }
}