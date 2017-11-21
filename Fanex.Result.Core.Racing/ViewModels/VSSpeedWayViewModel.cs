namespace Fanex.Result.Core.Racing.ViewModels
{
    using Base;
    using Base.ViewModels;
    using Utilities;

    public class VSSpeedWayViewModel : VSHRacingViewModel, IResultViewModel
    {
        public VSSpeedWayViewModel(DTO.Racing racing, ResultSetting setting)
            : this(racing, setting, null)
        {
        }

        public VSSpeedWayViewModel(DTO.Racing racing, ResultSetting setting, IRacingRefDataHelper refDataHelper)
           : base(racing, setting, refDataHelper)
        {
        }
    }
}