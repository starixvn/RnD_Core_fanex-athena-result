namespace Fanex.Result.Core.Sportsbook.ViewModels.BaseDetail
{
    using Base.Extension;

    public interface IResultDetail<T>
    {
        string HomeTeam { get; }

        string AwayTeam { get; }

        SafeList<T> GetDetailItems();
    }
}