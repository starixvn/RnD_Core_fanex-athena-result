namespace Fanex.Result.Core.LeapGaming.ViewModels
{
    using System.Collections.Generic;
    using Base;
    using Base.Utilities;
    using Base.ViewModels;
    using DTO;

    public class LeapGamingViewModel : BaseViewModel, IResultViewModel
    {
        private readonly LeapGaming _leapGaming;

        public LeapGamingViewModel(LeapGaming leapGaming, ResultSetting setting)
            : this(leapGaming, setting, null)
        {
            Details = new List<LeapGamingDetail>();
            if (!string.IsNullOrEmpty(_leapGaming.TransDesc))
            {
                foreach (var matchInfo in _leapGaming.TransDesc.Split('|'))
                {
                    Details.Add(new LeapGamingDetail(matchInfo));
                }
            }
        }

        public LeapGamingViewModel(LeapGaming leapGaming, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(setting, refDataHelper)
        {
            _leapGaming = leapGaming;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
        }

        public List<LeapGamingDetail> Details { get; }
    }
}