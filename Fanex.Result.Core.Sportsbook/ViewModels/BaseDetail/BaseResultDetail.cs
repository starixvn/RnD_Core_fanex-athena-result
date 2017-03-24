namespace Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail
{
    using System.Linq;
    using Base;
    using Base.DataDomain;
    using Base.Extension;
    using Base.Resources;
    using Base.ViewModels;
    using DTO;
    using Utilities;

    public class BaseResultDetail<T> : BaseViewModel where T : IGameDetailItem, new()
    {
        public BaseResultDetail(
            Sportsbook sportsbook,
            ResultSetting setting,
            ISportsbookRefDataHelper refDataHelper = null,
            ISportsbookHelperWraper sportsBookHelper = null) : base(setting)
        {
            Sportsbook = sportsbook;
            RefDataHelper = refDataHelper ?? new SportsbookRefDataHelper();
            SportsBooksHelper = sportsBookHelper ?? new SportsbookHelperWraper();
        }

        protected Sportsbook Sportsbook { get; }

        protected ISportsbookRefDataHelper RefDataHelper { get; }

        protected ISportsbookHelperWraper SportsBooksHelper { get; }

        public string HomeTeam
            => RefDataHelper.GetTeamName(Sportsbook.HomeId, GetSupportedLanguage, string.Empty) +
                (Sportsbook.IsNeutral ? LangResource.IsNeutral : string.Empty);

        public string AwayTeam
            => RefDataHelper.GetTeamName(Sportsbook.AwayId, GetSupportedLanguage, string.Empty);

        public virtual SafeList<T> GetDetailItems()
        {
            var detailItems = Sportsbook.GameDetails?.Select(item => CreateGameDetailItem(item));

            return detailItems.ToSafeList();
        }

        protected virtual T CreateGameDetailItem(GameDetail detailItem)
        {
            var item = new T
            {
                ResourceID = detailItem?.ResourceId,
            };

            return MapGameValueByStatusId(item, detailItem);
        }

        protected virtual T MapGameValueByStatusId(T item, GameDetail detailItem)
        {
            switch (detailItem.SettlementStatusID)
            {
                case EventStatusID.CompletedSettlementStatusID:
                case EventStatusID.RunningSettlementStatusID:
                    MapGameValueByPattern(item, detailItem);
                    break;

                case EventStatusID.RefundSettlementStatusID:
                    item.HomeValue = LangResource.Refund;
                    item.AwayValue = LangResource.Refund;
                    break;

                case EventStatusID.RefundHTSettlementStatusID:
                    item.HomeValue = LangResource.Refundht;
                    item.AwayValue = LangResource.Refundht;

                    break;

                case EventStatusID.RefundFTSettlementStatusID:
                    item.HomeValue = LangResource.Refundft;
                    item.AwayValue = LangResource.Refundft;

                    break;
            }

            return item;
        }

        protected virtual void MapGameValueByPattern(T item, GameDetail detailItem)
        {
            item.HomeValue = detailItem?.HomeValue;
            item.AwayValue = detailItem?.AwayValue;
        }
    }
}