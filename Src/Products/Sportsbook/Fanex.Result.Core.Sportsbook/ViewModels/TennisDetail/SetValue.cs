namespace Fanex.Result.Core.Sportsbook.ViewModels.TennisDetail
{
    using System.Collections.Generic;

    public class SetValue
    {
        public int SetId { get; set; }

        public IList<GameValue> Games { get; set; }
    }
}