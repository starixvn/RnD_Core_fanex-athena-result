// -----------------------------------------------------------------------
// <copyright file="VSSpeedWayViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Racing.ViewModels
{
    using System;
    using System.Globalization;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class VSSpeedWayViewModel : BaseViewModel, IResultViewModel
    {
        private readonly DTO.Racing _racing;

        public VSSpeedWayViewModel(DTO.Racing racing, ResultSetting setting)
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