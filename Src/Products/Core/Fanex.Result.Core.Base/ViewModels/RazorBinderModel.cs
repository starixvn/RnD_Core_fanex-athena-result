namespace Fanex.Result.Core.Base.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Extension;

    public class RazorBinderModel
    {
        public IList<GroupData> Data { get; set; } = new SafeList<GroupData>();

        public bool IsPopUp { get; set; }

        public bool VisiblePopUpTitle { get; set; }

        public SportType SportType { get; set; }

        public bool VisibleMatchCodeIDColumn { get; set; }

        public bool VisibleDetailedLink { get; set; }

        public bool VisibleFreeHeader { get; set; }

        public bool IsDetail { get; set; }

        public bool VisibleMainResult
        {
            get
            {
                if (!IsDetail && IsPopUp && Data.Count == 1)
                {
                    return Data[0].Data.Any(i => i.VisibleMatchResult);
                }

                return true;
            }
        }

        public bool VisibleDetailResult => IsDetail || CheckVisibleDetailResult;

        private bool CheckVisibleDetailResult
        {
            get
            {
                if (IsPopUp && Data.Count == 1)
                {
                    return Data[0].Data.Any(i => i.VisibleGameDetail);
                }

                return false;
            }
        }
    }
}