// -----------------------------------------------------------------------
// <copyright file="KenoAuthorization.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.DTO
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class KenoAuthorization
    {
        public int ErrorCode { get; set; }

        public string ErrorMsg { get; set; }

        public KenoAuthorizedData Data { get; set; }
    }
}