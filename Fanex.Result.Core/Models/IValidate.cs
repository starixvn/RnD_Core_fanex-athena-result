// -----------------------------------------------------------------------
// <copyright file="IValidate.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal interface IValidate
    {
        bool ValidateNormal(string dtoName, SportType sportType);

        bool ValidateOutRight(string dtoName);

        bool ValidateParlay(string dtoName);
    }
}