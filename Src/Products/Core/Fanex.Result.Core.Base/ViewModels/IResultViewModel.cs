namespace Fanex.Result.Core.Base.ViewModels
{
    public interface IResultViewModel
    {
        int LeagueId { get; }

        string LeagueName { get; }

        string SportName { get; }

        bool VisibleMatchResult { get; }

        bool VisibleGameDetail { get; }
    }
}