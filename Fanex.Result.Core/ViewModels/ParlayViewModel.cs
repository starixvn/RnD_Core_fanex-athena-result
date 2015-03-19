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
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.Models;
    using RazorEngine;

    /// <summary>
    /// Parlay view model class.
    /// </summary>
    public sealed class ParlayViewModel : BaseViewModel, IResultViewModel
    {
        private readonly Parlay _parlay;
        private readonly ResultSetting _sportSetting;
        private IList<IResultViewModel> _viewModelData = null;

        public ParlayViewModel(Parlay parlay, ResultSetting setting, LangResources resource)
            : base(setting, resource)
        {
            this._parlay = parlay;
            this.LanguageCode = setting.LanguageCode;
            this._sportSetting = new ResultSetting
            {
                IsPopUp = true,
                LanguageCode = this.LanguageCode,
                SportType = (SportType)this._parlay.SportId,
                DateTimeFormat = this.DateTimeFormat,
                HighlightWinnerTeam = setting.HighlightWinnerTeam,
                VisibleMatchCodeIDColumn = setting.VisibleMatchCodeIDColumn
            };
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

        public override int LeagueId
        {
            get
            {
                return this._parlay.LeagueId;
            }
        }

        public int SportId
        {
            get
            {
                return this._parlay.SportId;
            }
        }

        public IList<IResultViewModel> MatchItemView
        {
            get
            {
                if (_viewModelData == null)
                {
                    CreateItemViewModel();
                }

                return this._viewModelData.Where(i => i.VisibleMatchResult).ToList();
            }
        }

        public IList<IResultViewModel> GameDetailItemView
        {
            get
            {
                if (_viewModelData == null)
                {
                    CreateItemViewModel();
                }

                return this._viewModelData.Where(i => i.VisibleGameDetail).ToList();
            }
        }

        /// <summary>
        /// Render result by sport.
        /// </summary>
        /// <param name="model">The result model.</param>
        /// <returns>The sport  result.</returns>
        public string Render(RazorBinderModel model)
        {
            if (this._parlay.SportId == 0)
            {
                return string.Empty;
            }

            var sportType = (SportType)this._parlay.SportId;
            var template = Template.Instance.GetRazorTemplate(sportType.StringMe());
            if (string.IsNullOrEmpty(template))
            {
                return string.Empty;
            }

            Razor.GetTemplate(template, sportType.StringMe());
            return Razor.Run(sportType.StringMe(), model);
        }

        private void CreateItemViewModel()
        {
            _viewModelData = this._parlay.Sportbook.Select(i => ViewModelConcreteFactory.Instance.GetViewModel(
                i, _sportSetting, Lang)).ToList();
        }
    }
}