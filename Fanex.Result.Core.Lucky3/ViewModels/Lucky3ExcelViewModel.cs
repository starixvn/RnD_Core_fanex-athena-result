namespace Fanex.Result.Core.Lucky3.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Base.ExcelModels;
    using Base;
    using Base.ViewModels;

    internal sealed class Lucky3ExcelViewModel : ExcelBase, IExcelModel
    {
        public Lucky3ExcelViewModel(string templatePath, ResultSetting setting, IList<IResultViewModel> data) : base(templatePath, setting, data)
        {
        }

        protected override void AssignHeader(global::NPOI.SS.UserModel.ISheet sheet)
        {
            throw new NotImplementedException();
        }
    }
}