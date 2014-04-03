// -----------------------------------------------------------------------
// <copyright file="IKenoApi.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System;
    using Fanex.Result.Core.DTO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal interface IKenoApi
    {
        KenoAuthorization Authorize(KenoResultSetting setting, string userId);

        KenoExtendedToken ExtendToken(KenoResultSetting setting, string userId);
    }
}