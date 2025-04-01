namespace Fanex.Result.Core.VirtualRacing.ExcelModels
{
    using Base;
    using Base.ViewModels;
    using System.Collections.Generic;

    internal sealed class VirtualSteepleChaseExcel : VirtualRacingExcel
    {
        public VirtualSteepleChaseExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
        }
    }
}