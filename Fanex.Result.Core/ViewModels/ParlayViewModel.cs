// -----------------------------------------------------------------------
// <copyright file="ParlayViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.Models;
    using RazorEngine;

    /// <summary>
    /// Parlay view model class.
    /// </summary>
    public sealed class ParlayViewModel : BaseViewModel
    {
        private readonly Parlay _parlay;
        private ResultSetting _setting;

        public ParlayViewModel(Parlay parlay, ResultSetting setting, LangResources resource)
            : base(setting)
        {
            this._parlay = parlay;
            this.LanguageCode = setting.LanguageCode;
            this.Lang = resource;
            this._setting = setting;
        }

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Reviewed")]
        public override string SportName
        {
            get
            {
                return this.GetSportNameByLanguage(this._parlay.SportId);
            }
        }

        public override string LeagueName
        {
            get
            {
                return this.GetLeagueNameByLanguage(this._parlay.LeagueId);
            }
        }

        public int SportId
        {
            get
            {
                return this._parlay.SportId;
            }
        }

        public IList<object> ItemView
        {
            get
            {
                return this._parlay.Sportbook.Select(i => ViewModelConcreteFactory.Instance.GetViewModel(
                    i, _setting, Lang)).ToList();
            }
        }

        public string Render(RazorBinderModel model)
        {
            var sportType = (SportType)this._parlay.SportId;
            var template = Template.Instance.GetRazorTemplate(sportType.StringMe());
            Razor.GetTemplate(template, sportType.StringMe());
            return Razor.Run(sportType.StringMe(), model);
        }
    }
}