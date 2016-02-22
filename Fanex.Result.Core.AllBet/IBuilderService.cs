using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.Result.Core.AllBetCasino
{
    public interface IBuilderService
    {
        string Render(string transID, string languageCode = "en");
    }
}
