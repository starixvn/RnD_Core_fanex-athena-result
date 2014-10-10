// -----------------------------------------------------------------------
// <copyright file="VSHRacingViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Utilities;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class VSHRacingViewModel : BaseViewModel,IResultViewModel
    {
        private readonly Racing _racing;

        public VSHRacingViewModel(Racing racing, ResultSetting setting, LangResources resources)
            : base(setting, resources)
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

        public override string KickOffTime
        {
            get
            {
                return GetFormatedKickOffTime(this._racing.KickOffTime);
            }
        }

        public DateTime EvenTime
        {
            get
            {
                return this._racing.KickOffTime;
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
            get { return this._racing.EngineNo; }
        }

        public string Result
        {
            get { return RefData.GetHorseTeamName(this._racing.EngineId, this.GetSupportedLanguage, string.Empty); }
        }

        public string WinOdd
        {
            get { return this._racing.WinOdds > 0 ? Helper.DecFormat(this._racing.WinOdds, 2) : string.Empty; }
        }

        public string PlaceOdd
        {
            get
            {
                return
                    this._racing.PlaceOdds > 0 ? Helper.DecFormat(this._racing.PlaceOdds, 2) : string.Empty;
            }
        }

        public string Status
        {
            get { return this._racing.Favourite; }
        }

        public int RowCount
        {
            get { return this._racing.RowCount; }
        }
    }
}