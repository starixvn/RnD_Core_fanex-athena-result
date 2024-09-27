namespace Fanex.Result.Core.Base.Models
{
    using System;
    using Base;
    using DataDomain;
    using Org.BouncyCastle.Crypto.Prng;

    public sealed class Validate : IValidate
    {
        private static readonly Lazy<IValidate> LazyValidationService = new Lazy<IValidate>(() => new Validate());

        public static IValidate Instance
        {
            get
            {
                return LazyValidationService.Value;
            }
        }

        public bool ValidateNormal(string dtoName, SportType sportType)
        {
            return !string.IsNullOrEmpty(dtoName) &&
                Enum.IsDefined(typeof(SportType), sportType) && IsValidDTO(dtoName, sportType);
        }

        public bool ValidateOutright(string dtoName)
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

#pragma warning disable S1541 // Methods and properties should not be too complex

        private static bool IsValidDTO(string dtoName, SportType sportType)
#pragma warning restore S1541 // Methods and properties should not be too complex
        {
            switch (sportType)
            {
                case SportType.VirtualGreyhounds:
                case SportType.VirtualMotorRacing:
                case SportType.VirtualSpeedway:
                case SportType.VirtualCycling:
                    return dtoName == DTOType.RACING;

                case SportType.VirtualHorseRacing:
                case SportType.VirtualDogRacing:
                    return dtoName == DTOType.VirtualRacing;

                case SportType.LeapGamingVirtualSports:
                    return dtoName == DTOType.LEAPGAMING;

                case SportType.NumberGame:
                    return dtoName == DTOType.NUMBERGAME;

                case SportType.GoldDeluxe:
                    return dtoName == DTOType.GOLDDELUXE;

                case SportType.AGCasino:
                    return dtoName == DTOType.AGCASINO;

                case SportType.AllBetCasino:
                    return dtoName == DTOType.ALLBETCASINO;

                case SportType.Happy5:
                    return dtoName == DTOType.HAPPY5;

                case SportType.Bitcoin:
                    return dtoName == DTOType.BITCOIN;
                default:
                    return dtoName == DTOType.SPORTSBOOK || dtoName == DTOType.METRICGAMING;
            }
        }
    }
}