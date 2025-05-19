using Fanex.Result.Core.Base.Extension;

namespace Fanex.Result.Core.WCasino
{
    public enum BetType
    {
        [StringValue("Baccarat")]
        SuperMB111 = 12000,

        [StringValue("Baccarat")]
        SuperMB309 = 12014,

        [StringValue("DragonTiger")]
        SuperDT221 = 12015,

        [StringValue("DragonTiger")]
        SuperDT301 = 12016,

        [StringValue("Roulette")]
        WRoulette301 = 12017,

        [StringValue("SeDie")]
        WXD301 = 12018,

        [StringValue("Sicbo")]
        WSicbo301 = 12019,
        
        [StringValue("Sicbo")]
        WThaiHiLo = 12020,

        [StringValue("Sicbo")]
        WVietTaiXiu = 12021
    }
}
