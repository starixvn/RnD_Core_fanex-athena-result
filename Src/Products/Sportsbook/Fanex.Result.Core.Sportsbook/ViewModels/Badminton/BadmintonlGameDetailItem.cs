namespace Fanex.Result.Core.Sportsbook.ViewModels.Badminton
{
    using BaseDetail;

    public class BadmintonlGameDetailItem : GameDetailItem
    {
        public bool IsHome { get; set; }

        public bool IsAway { get; set; }

        public string GameName { get; set; }

        public string GameNumber { get; set; }
             
    }
}