namespace Fanex.Result.Core.Base.ViewModels
{
    using System.Collections.Generic;
    using Extension;

    public class GroupData
    {
        private IList<IResultViewModel> _data = new SafeList<IResultViewModel>();

        public int LeagueId { get; set; }

        public IList<IResultViewModel> Data
        {
            get
            {
                return _data;
            }

            set
            {
                if (value != null)
                {
                    _data = value;
                }
            }
        }
    }
}