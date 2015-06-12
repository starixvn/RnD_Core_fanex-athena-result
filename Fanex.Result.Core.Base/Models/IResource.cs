// -----------------------------------------------------------------------
// <copyright file="IResource.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.Models
{
    using Fanex.Result.Core.Base.DataDomain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IResource
    {
        LangResources GetLanguage(string languageCode, string languagePath);
    }
}