// -----------------------------------------------------------------------
// <copyright file="NumberGameViewModel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Utilities;

    /// <summary>
    /// Number game view model class.
    /// </summary>
    public sealed class NumberGameViewModel : BaseViewModel, IResultViewModel
    {
        private readonly NumberGame _numberGame;

        public NumberGameViewModel(NumberGame numberGame, ResultSetting setting, LangResources resources)
            : base(setting, resources)
        {
            this._numberGame = numberGame;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
        }

        public override string KickOffTime
        {
            get
            {
                return this.GetFormatedKickOffTime(this._numberGame.KickOffTime);
            }
        }

        public override int LeagueId
        {
            get { return this._numberGame.LeagueId; }
        }

        public override string LeagueName
        {
            get
            {
                return this.GetLeagueNameByLanguage(this._numberGame.LeagueId);
            }
        }

        public string MatchCode
        {
            get
            {
                return this._numberGame.MatchCode;
            }
        }

        public string MatchId
        {
            get
            {
                return this._numberGame.MatchId.ToString(CultureInfo.InvariantCulture);
            }
        }

        public string GameNo
        {
            get { return this._numberGame.MatchCode; }
        }

        public string FirstBall
        {
            get { return this._numberGame.Ball.Count > 0 ? Helper.FormatTwoDigit(this._numberGame.Ball[0]) : StringResources.NA; }
        }

        public string LastBall
        {
            get { return this._numberGame.Ball.Count > 0 ? Helper.FormatTwoDigit(this._numberGame.Ball[this._numberGame.Ball.Count - 1]) : StringResources.NA; }
        }

        public string Warrior
        {
            get
            {
                return this._numberGame.Ball.Count > 2 ? this._numberGame.Ball[1] > this._numberGame.Ball[2]
                       ? Lang.Warrior2ndBall : Lang.Warrior3rdBall :

                       StringResources.NA;
            }
        }

        public string OEFT
        {
            get { return this._numberGame.Ball.Sum() % 2 == 1 ? Lang.Odd : Lang.Even; }
        }

        public IList<string> Result
        {
            get { return this._numberGame.Ball.Select(i => Helper.FormatTwoDigit(i)).ToList(); }
        }

        public override string EventStatus
        {
            get
            {
                return MapEventStatus.MapEventStatusString(this._numberGame.EventStatus, this._numberGame.IsPending, false, false, this.Lang);
            }
        }

        public static string ShowBall(int ball)
        {
            return Helper.FormatTwoDigit(ball);
        }
    }
}