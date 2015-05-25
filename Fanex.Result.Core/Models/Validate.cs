// -----------------------------------------------------------------------
// <copyright file="Validate.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class Validate : IValidate
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
        [SuppressMessage(
           "StyleCop.CSharp.Nexcel.NexcelCustomRules",
           "SP2101:MethodMustNotContainMoreLinesThan",
           Justification = "Reviewed. Suppression is OK here.")]
        public bool ValidateNormal(string dtoName, SportType sportType)
        {
            if (string.IsNullOrEmpty(dtoName))
            {
                return false;
            }

            if ((short)sportType < 1)
            {
                return false;
            }

            var sportTypeValid = Enum.IsDefined(typeof(SportType), sportType);
            if (sportTypeValid)
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
                        if (dtoName == DTOType.RACING)
                        {
                            return true;
                        }

                        return false;
                    case SportType.NumberGame:
                        if (dtoName == DTOType.NUMBERGAME)
                        {
                            return true;
                        }

                        return false;
                    case SportType.LiveCasino:
                        if (dtoName == DTOType.LIVECASINO)
                        {
                            return true;
                        }

                        return false;

                    default:
                        if (dtoName == DTOType.SPORTSBOOK)
                        {
                            return true;
                        }

                        return false;
                }
            }

            return false;
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
            return CoreResourceConfig.LangCodes.Contains(languageCode);
        }
    }
}