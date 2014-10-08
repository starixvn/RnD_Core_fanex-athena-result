// -----------------------------------------------------------------------
// <copyright file="IResource.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IResource
    {
        LangResources GetLanguage(string languageCode, string languagePath);
    }
}