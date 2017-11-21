namespace Fanex.Result.Core.Racing.ViewModels
{
    using System.Globalization;
    using Base;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using Utilities;

    public class RacingViewModel : BaseViewModel, IResultViewModel
    {
        private readonly DTO.Racing _racing;

        public RacingViewModel(DTO.Racing racing, ResultSetting setting)
            : this(racing, setting, null)
        {
        }

        public RacingViewModel(DTO.Racing racing, ResultSetting setting, IRacingRefDataHelper refDataHelper)
        : base(setting, refDataHelper)
        {
            _racing = racing;
            LanguageCode = setting.LanguageCode;
            Sport = setting.SportType;
            RacingRefDataHelper = refDataHelper ?? new RacingRefDataHelper();
        }

        public IRacingRefDataHelper RacingRefDataHelper { get; }

        public override int LeagueId => _racing.LeagueId;

        public override string LeagueName => GetLeagueNameByLanguage(_racing.LeagueId);

        public string RaceName => _racing.RaceName.ToString(CultureInfo.InvariantCulture);

        public int Place => _racing.Place;

        public string Placing
        {
            get
            {
                switch (_racing.Place.ToString(CultureInfo.InvariantCulture))
                {
                    case "01":
                    case "1":
                        return LangResource.Lbl1st;

                    case "02":
                    case "2":
                        return LangResource.Lbl2nd;

                    case "03":
                    case "3":
                        return LangResource.Lbl3rd;

                    case "04":
                    case "4":
                        return LangResource.Lbl4th;

                    default:
                        return _racing.Place + LangResource.Noothers;
                }
            }
        }

        public string HorseNo => _racing.EngineNo;

        public string Result => RacingRefDataHelper.GetHorseTeamName(_racing.EngineId, GetSupportedLanguage, string.Empty);

        public string WinOdd
            => _racing.WinOdds > 0 ? Helper.DecFormat(_racing.WinOdds, 2) : string.Empty;

        public string PlaceOdd
            => _racing.PlaceOdds > 0 ? Helper.DecFormat(_racing.PlaceOdds, 2) : string.Empty;

        public bool IsSCR => _racing.IsSCR;

        public bool IsFavor => _racing.IsFavor;

        public string SCR => _racing.SCROfMatch;

        public string TF => _racing.TFOfMatch;

        public int RowCount => _racing.RowCount;
    }
}