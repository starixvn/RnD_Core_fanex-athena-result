// -----------------------------------------------------------------------
// <copyright file="Validate.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Base.Models
{
    using System;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class Validate : IValidate
    {
        /*singleton*/
        private static readonly object SynceObject = new object();
        private static IValidate instance;

        private Validate()
        {
        }

        public static IValidate Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SynceObject)
                    {
                        if (instance == null)
                        {
                            instance = new Validate();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Validate DTO and sport type.
        /// </summary>
        /// <param name="dtoName">The DTO name.</param>
        /// <param name="sportType">The sport type.</param>
        /// <returns>True if DTO name match with sport type.</returns>
        public bool ValidateNormal(string dtoName, SportType sportType)
        {
            return !string.IsNullOrEmpty(dtoName) &&
                Enum.IsDefined(typeof(SportType), sportType) && IsValidDTO(dtoName, sportType);
        }

        public bool ValidateOutRight(string dtoName)
        {
            return !string.IsNullOrEmpty(dtoName) && dtoName == DTOType.OUTRIGHT;
        }

        public bool ValidateParlay(string dtoName)
        {
            return !string.IsNullOrEmpty(dtoName) && dtoName == DTOType.PARLAY;
        }

        public bool ValidateResource(string languageCode)
        {
            return !string.IsNullOrWhiteSpace(languageCode);
        }

        private static bool IsValidDTO(string dtoName, SportType sportType)
        {
            switch (sportType)
            {
                case SportType.HorseRacing:
                case SportType.Greyhound:
                case SportType.Harness:
                case SportType.VirtualHorseRacing:
                case SportType.VirtualGreyhounds:
                case SportType.VirtualMotorRacing:
                case SportType.VirtualSpeedway:
                case SportType.VirtualCycling:
                    return dtoName == DTOType.RACING;
                case SportType.NumberGame:
                    return dtoName == DTOType.NUMBERGAME;
                case SportType.LiveCasino:
                    return dtoName == DTOType.LIVECASINO;
                case SportType.GoldDeluxe:
                    return dtoName == DTOType.GOLDDELUXE;
                default:
                    return dtoName == DTOType.SPORTSBOOK || dtoName == DTOType.METRICGAMING;
            }
        }
    }
}