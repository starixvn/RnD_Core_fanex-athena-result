namespace Fanex.Result.Core.Sportsbook.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Base;
    using Base.Exception;
    using Base.Extension;
    using Base.Utilities;
    using Base.ViewModels;
    using DTO;
    using Models;
    using RazorEngine;
    using RazorEngine.Templating;

    public sealed class ParlayViewModel : BaseViewModel, IResultViewModel
    {
        private readonly Parlay _parlay;
        private readonly ResultSetting _sportSetting;
        private IList<IResultViewModel> _viewModelData;
        private IList<IResultViewModel> _onlyMatchItemViewModel;
        private IList<IResultViewModel> _onlyGameDetailItemViewModel;
        private IList<IResultViewModel> _bothMatchAndGameDetailItemViewModel;

        public ParlayViewModel(Parlay parlay, ResultSetting setting)
            : this(parlay, setting, null)
        {
        }

        public ParlayViewModel(Parlay parlay, ResultSetting setting, IRefDataHelper refDataHelper)
            : base(setting, refDataHelper)
        {
            _parlay = parlay;
            LanguageCode = setting.LanguageCode;
            _sportSetting = new ResultSetting
            {
                IsPopupOpenFromBetList = setting.IsPopupOpenFromBetList,
                IsPopUp = true,
                IsParlayPopUp = true,
                LanguageCode = LanguageCode,
                SportType = (SportType)_parlay.SportId,
                DateTimeFormat = DateTimeFormat,
                HighlightWinnerTeam = setting.HighlightWinnerTeam,
                VisibleMatchCodeIDColumn = setting.VisibleMatchCodeIDColumn
            };
        }

        public override string SportName => GetSportNameByLanguage(_parlay.SportId);

        public override string LeagueName => GetLeagueNameByLanguage(_parlay.LeagueId);

        public override int LeagueId => _parlay.LeagueId;

        public int SportId => _parlay.SportId;

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
                if (_parlay.SportId == 0)
                {
                    return string.Empty;
                }

                var sportType = (SportType)_parlay.SportId;
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
                _viewModelData = _parlay.Sportbook.Select(i => ViewModelConcreteFactory.Instance.GetViewModel(
                    i, _sportSetting)).ToSafeList();
            }

            if (_bothMatchAndGameDetailItemViewModel == null)
            {
                _bothMatchAndGameDetailItemViewModel = _viewModelData.Where(i => i.VisibleMatchResult && i.VisibleGameDetail).ToSafeList();
            }

            if (_onlyMatchItemViewModel == null)
            {
                _onlyMatchItemViewModel = _viewModelData.Where(i => i.VisibleMatchResult && !i.VisibleGameDetail).ToSafeList();
            }

            if (_onlyGameDetailItemViewModel == null)
            {
                _onlyGameDetailItemViewModel = _viewModelData.Where(i => !i.VisibleMatchResult && i.VisibleGameDetail).ToSafeList();
            }
        }
    }
}