namespace Fanex.Result.Core.Sportsbook.ViewModels.SoccerDetail
{
    using System.Collections.Generic;
    using System.Linq;
    using Base.Utilities;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.DTO;

    public class BetBuilderResultViewModel : BaseViewModel
    {
        public BetBuilderResultViewModel(
            Sportsbook sportsbook,
            ResultSetting setting,
            IRefDataHelper refDataHelper = null)
            : base(setting, refDataHelper)
        {
            LanguageCode = setting.LanguageCode;
            Detail = sportsbook.BetBuilderDetails
                .Select(FormatBetBuilderContent)
                .Where(IsDetailRow);

            Total = sportsbook.BetBuilderDetails
                .Select(FormatBetBuilderContent)
                .FirstOrDefault(IsTotalRow);
        }

        public IEnumerable<BetBuilderDetail> Detail { get; }

        public BetBuilderDetail Total { get; }

        private static bool IsTotalRow(BetBuilderDetail row) => row != null && row.SelectionType == 0;

        private static bool IsDetailRow(BetBuilderDetail row) => !IsTotalRow(row);

        private BetBuilderDetail FormatBetBuilderContent(BetBuilderDetail detail)
        {
            detail.Result = MapEventStatus.GetLogResource(detail.Result);
            detail.SelectionName = RefDataHelper.GetBetBuilderSelectionName(
                detail.SelectionType,
                GetSupportedLanguage,
                detail.Result);

            return detail;
        }
    }
}