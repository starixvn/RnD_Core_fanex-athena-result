// -----------------------------------------------------------------------
// <copyright file="RacingViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System.Globalization;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Utilities;
    using RemotingRef;

    /// <summary>
    /// Racing view model class.
    /// </summary>
    public sealed class RacingViewModel : BaseViewModel
    {
        private readonly Racing _racing;

        public RacingViewModel(Racing racing, SportType sport, string languageCode, LangResources resouces)
        {
            this._racing = racing;
            this.LanguageCode = languageCode;
            this.Lang = resouces;
            this.Sport = sport;
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
                        return this.Lang.Lbl1st;
                    case "02":
                    case "2":
                        return this.Lang.Lbl2nd;
                    case "03":
                    case "3":
                        return this.Lang.Lbl3rd;
                    case "04":
                    case "4":
                        return this.Lang.Lbl4th;
                    default:
                        return this._racing.Place + this.Lang.Noothers;
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