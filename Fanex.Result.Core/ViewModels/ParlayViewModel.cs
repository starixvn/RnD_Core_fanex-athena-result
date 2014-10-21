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
    using Fanex.Result.Core.Utilities;
    using RazorEngine;

    /// <summary>
    /// Parlay view model class.
    /// </summary>
    public sealed class ParlayViewModel : BaseViewModel, IResultViewModel
    {
        private readonly Parlay _parlay;

        public ParlayViewModel(Parlay parlay, ResultSetting setting, LangResources resource)
            : base(setting, resource)
        {
            this._parlay = parlay;
            this.LanguageCode = setting.LanguageCode;
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

        public bool VisibleTennisSetDetail
        {
            get
            {
                return this._parlay.SportId == (int)SportType.Tennis &&
                    this._parlay.BetTypes.Contains(BetTypeDefinition.SetXGameYWinner)
                    && this._parlay.BetIDs.Any(i => i.Length == ConstantValueDefinition.SETXGAMEYRESOURCEIDLENGTH)
                    && this._parlay.BetIDs.Any(i => Helper.IsSetXGameYBetID(i));
            }
        }

        public bool VisibleTennisResult
        {
            get
            {
                return
                    (this._parlay.BetTypes.Count == 0 || this._parlay.BetIDs.Count == 0) ||
                   (this._parlay.SportId == (int)SportType.Tennis
                    && this._parlay.BetTypes.Any(i => i != BetTypeDefinition.SetXGameYWinner));
            }
        }

        public List<string> SetXGameYBetIDs
        {
            get
            {
                return this._parlay.BetIDs.Where(i => Helper.IsSetXGameYBetID(i)).ToList();
            }
        }

        public IList<IResultViewModel> ItemView
        {
            get
            {
                var sportSetting = new ResultSetting
                {
                    IsPopUp = true,
                    LanguageCode = this.LanguageCode,
                    SportType = (SportType)this._parlay.SportId,
                    DateTimeFormat = this.DateTimeFormat,
                };

                return this._parlay.Sportbook.Select(i => ViewModelConcreteFactory.Instance.GetViewModel(
                    i, sportSetting, Lang)).ToList();
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
    }
}