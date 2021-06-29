namespace Fanex.Result.Core.Sportsbook.ViewModels.Badminton
{
    using Base;
    using BaseDetail;
    using DTO;
    using Fanex.Result.Core.Base.Utilities;
    using Utilities;

    public class BeachVolleyballResultDetail : BaseResultDetail<GameDetailItem>
    {
        public BeachVolleyballResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper,
            ISportsbookHelperWraper sportsBookHelper)
            : base(sportsbook, setting, refDataHelper, sportsBookHelper)
        {
        }
    }
}