// -----------------------------------------------------------------------
// <copyright file="RacingViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Racing.ViewModels
{
    using System.Globalization;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using RemotingRef;

    /// <summary>
    /// Racing view model class.
    /// </summary>
    public sealed class RacingViewModel : BaseViewModel, IResultViewModel
    {
        private readonly DTO.Racing _racing;

        public RacingViewModel(DTO.Racing racing, ResultSetting setting)
            : base(setting)
        {
            this._racing = racing;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
        }

        public override int LeagueId
        {
            get
            {
                return this._racing.LeagueId;
            }
        }

        public override string LeagueName
        {
            get
            {
                return this.GetLeagueNameByLanguage(this._racing.LeagueId);
            }
        }

        public string RaceName
        {
            get
            {
                return this._racing.RaceName.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int Place
        {
            get
            {
                return this._racing.Place;
            }
        }

        public string Placing
        {
            get
            {
                switch (this._racing.Place.ToString(CultureInfo.InvariantCulture))
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
                        return this._racing.Place + LangResource.Noothers;
                }
            }
        }

        public string HorseNo
        {
            get
            {
                return this._racing.EngineNo;
            }
        }

        public string Result
        {
            get
            {
                return RefData.GetHorseTeamName(this._racing.EngineId, this.GetSupportedLanguage, string.Empty);
            }
        }

        public string WinOdd
        {
            get
            {
                return this._racing.WinOdds > 0
                    ? Helper.DecFormat(this._racing.WinOdds, 2)
                    : string.Empty;
            }
        }

        public string PlaceOdd
        {
            get
            {
                return
                    this._racing.PlaceOdds > 0 ? Helper.DecFormat(this._racing.PlaceOdds, 2) : string.Empty;
            }
        }

        public bool IsSCR
        {
            get
            {
                return this._racing.IsSCR;
            }
        }

        public bool IsFavor
        {
            get
            {
                return this._racing.IsFavor;
            }
        }

        public string SCR
        {
            get
            {
                return this._racing.SCROfMatch;
            }
        }

        public string TF
        {
            get
            {
                return this._racing.TFOfMatch;
            }
        }

        public int RowCount
        {
            get { return this._racing.RowCount; }
        }
    }
}