// -----------------------------------------------------------------------
// <copyright file="ViewModelFactory.cs" company="Nexcel">
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
    internal abstract class ViewModelFactory
    {
        public abstract object GetViewModel(object obj, SportType sportType, string languageCode, LangResources resource);
        
        public abstract object GetOutRightViewModel(object obj, string languageCode, LangResources resource);
        
        public abstract object GetParlayViewModel(object obj, string languageCode, LangResources resource);
    }
}
