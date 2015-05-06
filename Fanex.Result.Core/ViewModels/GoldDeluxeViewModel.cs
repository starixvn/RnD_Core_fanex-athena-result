// -----------------------------------------------------------------------
// <copyright file="GoldDeluxeViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.DataDomain;
    using RemotingRef;
    using System.Globalization;
    using Fanex.Result.Core.Utilities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GoldDeluxeViewModel : BaseViewModel, IResultViewModel
    {
        private readonly GoldDeluxe _goldDeluxe;
        private string _result;
        private string _gameId;
        private string _htmlResult;
        public GoldDeluxeViewModel(GoldDeluxe goldDeluxe, ResultSetting setting, LangResources resource)
            : base(setting, resource)
        {
            this._goldDeluxe = goldDeluxe;
            this.LanguageCode = setting.LanguageCode;
            this.Sport = setting.SportType;
        }

        public override string LeagueName
        {
            get
            {
                return RefData.GetBetTypeName(
               this._goldDeluxe.Bettype.ToString(CultureInfo.InvariantCulture),
               this.GetSupportedLanguage,
               string.Empty);
            }
        }
        public override string EventStatus
        {
            get
            {
                return Lang.Completed;
            }
        }
        public string GameID
        {
            get
            {
                if (string.IsNullOrEmpty(_gameId))
                {
                    BindingResult();
                }

                return _gameId;
            }
        }

        public string ResultInHtml
        {
            get
            {
                if (string.IsNullOrEmpty(_htmlResult))
                {
                    BindingResult();
                }

                return _htmlResult;
            }
        }

        public string ResultInExcel
        {
            get
            {
                if (string.IsNullOrEmpty(_result))
                {
                    BindingResult();
                }

                return _result;
            }
        }

        private void BindingResult()
        {
            if (!string.IsNullOrEmpty(this._goldDeluxe.Result))
            {
                try
                {
                    var resultTokens = this._goldDeluxe.Result.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    _gameId = resultTokens[0].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    _result = resultTokens[1].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    if (this._goldDeluxe.Bettype == GoldDeluxeBettype.Roulette)
                    {
                        _htmlResult = GoldDeluxeHelper.GetRoutletResult(_result);
                    }
                    else
                    {
                        _htmlResult = GoldDeluxeHelper.GetBacaratResult(_result, Lang.Banker, Lang.Player);
                    }
                }
                catch (IndexOutOfRangeException exception)
                {
                    throw new IndexOutOfRangeException("The result value is invalid", exception);
                }

            }
        }


    }
}
