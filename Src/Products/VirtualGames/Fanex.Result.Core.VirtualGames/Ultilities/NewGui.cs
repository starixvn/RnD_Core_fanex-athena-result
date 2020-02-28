using System;

namespace Fanex.Result.Core.VirtualGames.Ultilities
{
    public interface INewGui
    {
        string CreateNewId();
    }

    public class NewGui : INewGui
    {
        public string CreateNewId()
            => Guid.NewGuid().ToString();
    }
}
