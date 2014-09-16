// -----------------------------------------------------------------------
// <copyright file="ResultPopupMetalModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace MvcExample.Models.MetaModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Serializable]
    public class ResultPopupMetaModel
    {
        [Required]
        public int MatchId
        {
            get;
            set;
        }

        [Required]
        public int Bettype { get; set; }

        public int? RaceNo { get; set; }

        [Required]
        public int SportType { get; set; }

        public int? IsOutRight { get; set; }

        public long? RefNo { get; set; }

        public DateTime? WinLossDate { get; set; }
        public string LanguageCode { get; set; }
    }
}