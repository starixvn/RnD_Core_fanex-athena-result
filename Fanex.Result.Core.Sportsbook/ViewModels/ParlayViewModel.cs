// -----------------------------------------------------------------------
// <copyright file="ParlayViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Core.Sportsbook.Models;
    using RazorEngine;
    using RazorEngine.Templating;

    /// <summary>
    /// Parlay view model class.
    /// </summary>
    public sealed class ParlayViewModel : BaseViewModel, IResultViewModel
    {
        private readonly Parlay _parlay;
        private readonly ResultSetting _sportSetting;
        private IList<IResultViewModel> _viewModelData = null;
        private IList<IResultViewModel> _onlyMatchItemViewModel = null;
        private IList<IResultViewModel> _onlyGameDetailItemViewModel = null;
        private IList<IResultViewModel> _bothMatchAndGameDetailItemViewModel = null;

        public ParlayViewModel(Parlay parlay, ResultSetting setting)
            : base(setting)
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
                if (_onlyMatchItemViewModel == null)
                {
                    CreateItemViewModel();
                }

                return _onlyMatchItemViewModel;
            }
        }

        public IList<IResultViewModel> GameDetailItemView
        {
            get
            {
                if (_onlyGameDetailItemViewModel == null)
                {
                    CreateItemViewModel();
                }

                return _onlyGameDetailItemViewModel;
            }
        }

        public IList<IResultViewModel> BothMatchAndDetailItemView
        {
            get
            {
                if (_bothMatchAndGameDetailItemViewModel == null)
                {
                    CreateItemViewModel();
                }

                return _bothMatchAndGameDetailItemViewModel;
            }
        }

        /// <summary>
        /// Render result by sport.
        /// </summary>
        /// <param name="model">The result model.</param>
        /// <returns>The sport  result.</returns>
        public string Render(RazorBinderModel model)
        {
            try
            {
                if (this._parlay.SportId == 0)
                {
                    return string.Empty;
                }

                var sportType = (SportType)this._parlay.SportId;
                var sportName = sportType.StringMe();
                var templateContent = Template.Instance.GetRazorTemplate(sportName);

                if (string.IsNullOrEmpty(templateContent))
                {
                    return string.Empty;
                }

                var result = new StringBuilder();
                using (var writer = new StringWriter(result))
                {
                    var typeOfBinderModel = typeof(RazorBinderModel);
                    if (!Engine.Razor.IsTemplateCached(sportName, typeOfBinderModel))
                    {
                        Engine.Razor.AddTemplate(sportName, templateContent);
                        Engine.Razor.Compile(sportName, typeOfBinderModel);
                    }

                    Engine.Razor.Run(sportName, writer, typeOfBinderModel, model);
                }

                return result.ToString();
            }
            catch (Exception exception)
            {
                throw new ResultCoreException(exception.Message, exception.InnerException);
            }
        }

        /// <summary>
        /// Create view model for parlay result.
        /// </summary>
        private void CreateItemViewModel()
        {
            if (_viewModelData == null)
            {
                _viewModelData = this._parlay.Sportbook.Select(i => ViewModelConcreteFactory.Instance.GetViewModel(
                    i, _sportSetting)).ToSafeList();
            }

            if (_bothMatchAndGameDetailItemViewModel == null)
            {
                _bothMatchAndGameDetailItemViewModel = _viewModelData.Where(i => i.VisibleMatchResult && i.VisibleGameDetail).ToSafeList();
            }

            if (_onlyMatchItemViewModel == null)
            {
                _onlyMatchItemViewModel = _viewModelData.Except(_bothMatchAndGameDetailItemViewModel).Where(i => i.VisibleMatchResult && !i.VisibleGameDetail).ToSafeList();
            }

            if (_onlyGameDetailItemViewModel == null)
            {
                _onlyGameDetailItemViewModel = _viewModelData.Except(_bothMatchAndGameDetailItemViewModel).Except(_onlyMatchItemViewModel).ToSafeList();
            }
        }
    }
}