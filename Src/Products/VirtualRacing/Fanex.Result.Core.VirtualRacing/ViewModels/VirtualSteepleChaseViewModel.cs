namespace Fanex.Result.Core.VirtualRacing.ViewModels
{
    using Base;
    using Base.ViewModels;
    using Fanex.Result.Core.Base.Utilities;

    public class VirtualSteepleChaseViewModel : VirtualRacingViewModel, IResultViewModel
    {
        public VirtualSteepleChaseViewModel(DTO.VirtualRacing racing, ResultSetting setting)
            : this(racing, setting, null)
        {
        }

        public VirtualSteepleChaseViewModel(DTO.VirtualRacing racing, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(racing, setting, refDataHelper)
        {
        }                

        public string RaceInformation
        {
            get
            {
                var raceCode = "{0}" + $" {Racing.VendorMatchId} - ID {Racing.Distance}";

                return RefDataHelper.GetVirtualSportMatchCodeFormat(raceCode, "KVS_MatchCode", GetSupportedLanguage);
            }
        }
    }
}