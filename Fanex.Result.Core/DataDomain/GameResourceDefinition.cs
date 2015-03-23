// -----------------------------------------------------------------------
// <copyright file="GameResourceDefinition.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DataDomain
{
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal static class GameResourceDefinition
    {
        private static IDictionary<string, List<string>> _betRada = new Dictionary<string, List<string>> 
        {
        { "Goals", new List<string> { "176" } }, 
        { "GoalSequence", new List<string> { "08" } }, 
        { "GoalTime", new List<string> { "192", "193", "^169\\d{2}$" } }, 
        { "GoalScorer", new List<string> { "217", "^216\\d{2}$" } }, 
        { "Corner", new List<string> { "05", "207", "209", "04", "206", "208" } }, 
        { "PlayerSentOff", new List<string> { "211", "210" } }, 
        { "HomeAwayPlayerSentOff", new List<string> { "213", "215", "212", "214" } }, 
        { "Special", new List<string> { "173", "135", "175", "168" } } 
        };

        public static IDictionary<string, List<string>> BetRada
        {
            get
            {
                return _betRada;
            }
        }
    }
}