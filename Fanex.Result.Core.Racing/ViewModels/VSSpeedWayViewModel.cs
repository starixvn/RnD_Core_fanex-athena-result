namespace Fanex.Result.Core.Racing.ViewModels
{
    using Base;
    using Base.ViewModels;
    using Fanex.Result.Core.Base.Utilities;

    public class VSSpeedWayViewModel : VSHRacingViewModel, IResultViewModel
    {
        public VSSpeedWayViewModel(DTO.Racing racing, ResultSetting setting)
            : this(racing, setting, null)
        {
        }

        public VSSpeedWayViewModel(DTO.Racing racing, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(racing, setting, refDataHelper)
        {
        }
    }
}