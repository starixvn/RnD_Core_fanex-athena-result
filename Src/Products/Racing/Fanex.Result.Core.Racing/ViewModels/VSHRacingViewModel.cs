namespace Fanex.Result.Core.Racing.ViewModels
{
    using System;
    using Base;
    using Base.ViewModels;
    using Fanex.Result.Core.Base.Utilities;

    public class VSHRacingViewModel : RacingViewModel, IResultViewModel
    {
        private readonly DTO.Racing _racing;

        public VSHRacingViewModel(DTO.Racing racing, ResultSetting setting)
            : this(racing, setting, null)
        {
        }

        public VSHRacingViewModel(DTO.Racing racing, ResultSetting setting, IRefDataHelper refDataHelper)
           : base(racing, setting, refDataHelper)
        {
            _racing = racing;
        }

        public override string KickOffTime => GetFormatedKickOffTime(_racing.KickOffTime);

        public DateTime EvenTime => _racing.KickOffTime;

        public string Status => _racing.Favourite;
    }
}