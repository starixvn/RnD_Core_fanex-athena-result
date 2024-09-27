namespace Fanex.Result.Core.VirtualRacing.ViewModels
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Base;
    using Base.ViewModels;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;

    public class VirtualRacingViewModel : BaseViewModel, IResultViewModel
    {
        public readonly DTO.VirtualRacing Racing;

        public VirtualRacingViewModel(DTO.VirtualRacing racing, ResultSetting setting)
            : this(racing, setting, null)
        {
        }

        public VirtualRacingViewModel(DTO.VirtualRacing racing, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(setting, refDataHelper)
        {
            Racing = racing;
            Racing.Details = Racing.Details.OrderBy(d => d.Placing).ToList();
            LanguageCode = setting.LanguageCode;
        }

        public int GetNumberOfDetail => IsStatusRaceRefund ? 1 : Racing.Details.Count;

        public override string KickOffTime => GetFormatedKickOffTime(Racing.EventTime);

        public string Status => IsStatusRaceRefund ? LangResource.Refund : LangResource.Completed;

        public override int LeagueId => Racing.LeagueId;

        public override string LeagueName => RefDataHelper.GetLeague3rdName(Racing.LeagueId, GetSupportedLanguage, LeagueId.ToString());

        public string GetRunnerName(int racerId = 0)
        => IsStatusRaceRefund ? ConstantValue.Hyphen : RefDataHelper.GetTeam3rdName(racerId, GetSupportedLanguage, racerId.ToString());

        public string GetLane(string lane = "")
        => IsStatusRaceRefund ? ConstantValue.Hyphen : lane;

        public string GetRunnerPrice(int placing = 0)
        {
            if (IsStatusRaceRefund)
            {
                return ConstantValue.Hyphen;
            }

            var ranking = new Dictionary<int, string>
            {
                {1, LangResource.Lbl1st},
                {2, LangResource.Lbl2nd},
                {3, LangResource.Lbl3rd}
            };

            return ranking.ContainsKey(placing) ? ranking[placing] : placing.ToString();
        }

        public string RaceInformation
        {
            get
            {
                var raceCode = "{49}" + $" {Racing.VendorMatchId} - ID {Racing.Distance}";

                return RefDataHelper.GetVirtualSportMatchCodeFormat(raceCode, "BVS_MatchCode", GetSupportedLanguage);
            }
        }

        public bool IsStatusRaceRefund
        => Racing.BetStatus.Equals(ConstantValue.Refund, StringComparison.InvariantCultureIgnoreCase);
    }
}