namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.AGCasino.DTO;

    public class CasinoResultService : IResultService<AGCasino>
    {
        public IEnumerable<AGCasino> GetResult(ICriteria criteria)
        {
            var result = new List<AGCasino>();
            result.Add(new AGCasino
            {
                Bettype = 1801,
                Result = "round=DSP*tableCode=va21*game=BAC*type=1*val=*result=B;H5;CK;DA:P;HK;DQ;HJ*winResult=1;6*result2=B;5:P;0"
            });
            result.Add(new AGCasino
            {
                Bettype = 1802,
                Result = "round=DSP*tableCode=va21*game=ROU*type=101*val=6*result=20*winResult=108;112;115;116;119*result2="
            });
            result.Add(new AGCasino
            {
                Bettype = 1802,
                Result = "round=DSP*tableCode=va21*game=ROU*type=101*val=6*result=*winResult=108;112;115;116;119*result2="
            });
            result.Add(new AGCasino
            {
                Bettype = 1803,
                Result = "round=DSP*tableCode=va21*game=DT*type=21*val=*result=D;H3:T;C9*winResult=22*result2=D;3:T;9"
            });
            result.Add(new AGCasino
            {
                Bettype = 1804,
                Result = "{\"round\":\"AGQ\",\"tableCode\":\"va21\",\"game\":\"SHB\",\"type\":\"41\",\"result\":\"4;3;3\",\"winResult\":\"42;44;54;55;61;73;85\"}"
            });
            result.Add(new AGCasino
            {
                Bettype = 1804,
                Result = "round=AGQ*tableCode=va21*game=SHB*type=41*val=*result=4;3;3*winResult=42;44;54;55;61;73;85*result2="
            });
            result.Add(new AGCasino
            {
                Bettype = 1804,
                Result = "round=AGQ*tableCode=va21*game=SHB*type=41*val=*winResult=42;44;54;55;61;73;85*result2="
            });
            return result;
        }

        public static string GetWMTransDesc(int betType)
        {
            switch (betType)
            {
                case 3402:
                    return "choice=11001*comms=0*site=IBC*tableCode=31*state=0*result=D;D6:T;DK*results=*winResult=*ForcedCompleted=0*ReasonCode=0*enterType=0";
                case 3403:
                    return "choice=12151*comms=0*site=IBC*tableCode=1*state=0*result=3*results=*winResult=*ForcedCompleted=0*ReasonCode=0*enterType=0";
                case 3404:
                    return "choice=13004*comms=0*site=IBC*tableCode=1*state=0*result=5;5;2*results=*winResult=*ForcedCompleted=0*ReasonCode=0*enterType=0";
                case 3407:
                    return "choice=16009*comms=0*site=IBC*tableCode=1*state=0*result=1*results=*winResult=*ForcedCompleted=0*ReasonCode=0*enterType=0";
                case 3408:
                    return "choice=17008*comms=0*site=IBC*tableCode=1*state=0*result=W;W;W;R*results=*winResult=*ForcedCompleted=0*ReasonCode=0*enterType=0";
                case 3409:
                    return "choice=18005*comms=0*site=IBC*tableCode=1*state=0*result=6;6;4*results=*winResult=*ForcedCompleted=0*ReasonCode=0*enterType=0";
                case 3410:
                    return "site=spondemo*tableCode=B001*state=0*result=B;S6;H2:P1;D10;D5:P2;D2;D1:P3;D3;D4*results=B;8:P;5*winResult=2;244";
                case 3406:
                    return "choice=15002*comms=0*site=IBC*tableCode=1*state=0*result=B;DQ;C8;D2:P1;H10;C3;CQ:P2;SQ;CK;D9:P3;HJ;HA;H6*results=*winResult=*ForcedCompleted=0*ReasonCode=0*enterType=0";
                case 3405:
                    return "choice=14002*comms=0*site=IBC*tableCode=31*state=0*result=B;S8;DA;SA;DQ;C6:P1;C4;S6;C2;H5;C7:P2;H9;S5;S9;D4;CK:P3;DJ;D10;SQ;H8;HJ*results=*winResult=*ForcedCompleted=0*ReasonCode=0*enterType=0";
                case 3411:
                    return "choice=20008*comms=0*site=IBC*tableCode=1*state=0*result=B;7;2:P1;4;6:P2;4;8:P3;2;W*results=*winResult=*ForcedCompleted=0*ReasonCode=0*enterType=0";
                default:
                    return "choice=10002*comms=0*site=IBC*tableCode=502*state=0*result=B;D3;SQ;H8:P;CK;HQ;H7*results=*winResult=*ForcedCompleted=0*ReasonCode=0*enterType=0";
            }
        }

        public static string GetSAGamingTransDesc(int betType)
        {
            switch (betType)
            {
                case 3002:
                    return "site=spondemo*tableCode=B001*state=0*result=D;C2:T;D3*results=D;2:T;3*winResult=2";
                case 3003:
                    return "site=spondemo*tableCode=B001*state=0*result=1;2;3;6;5*results=B;8:P;5*winResult=0;3";
                case 3004:
                    return "site=733333*tableCode=632*result=4*results=*winResult=2;3;1;4;10*ForcedCompleted=0*ReasonCode=0";
                case 3005:
                    return "site=spondemo*tableCode=B001*state=0*result=20*results=B;8:P;5*winResult=150;154";
                case 3006:
                    return "site=733333*tableCode=660*result=M;5:S;11*results=*winResult=5;11*ForcedCompleted=0*ReasonCode=0";

                default:
                    return "site=spondemo*tableCode=B001*state=0*result=B;S6;H2:P;D10;D5*results=B;8:P;5*winResult=2;244";
            }
        }

        public static string GetAllBetTransDesc(int betType)
        {
            switch (betType)
            {
                case 2403:
                    return "choice=4001*comms=101*site=spondemo*tableCode=B001*state=0*result=20*results=B;8:P;5*winResult=4001";

                case 2404:
                    return "choice=3001*comms=101*site=spondemo*tableCode=B001*state=0*result=1;2;3;6;5*results=B;8:P;5*winResult=3001";

                case 2405:
                    return "choice=2001*comms=101*site=spondemo*tableCode=B001*state=0*result=D;C2:T;D3*results=D;2:T;3*winResult=2001";

                case 2406:
                    return "choice=9001*comms=100*site=IBC*tableCode=W001*state=0*result=D;D4;H10;H3:P;CA;H9;D9*results=*winResult=9003;9001*ForcedCompleted=0*ReasonCode=0*enterType=100";

                case 2407:
                    return "choice=8001*comms=100*site=IBC*tableCode=BB001*state=0*result= C9:B;H8;C6;D4;S9;DQ:P1;HJ;S8;D7;H3;H4:P2;C2;CQ;D1;C8;H7:P3;C5;D3;S4;D8;H1*results= B;7:P1; 2:P2; 7:P3;0*winResult= 8112;8001;8003;8102;8011;8013*ForcedCompleted=0*ReasonCode=0*enterType=100";

                case 2408:
                    return "choice=5001*comms=100*site=IBC*tableCode=B001*state=0*result=B;H8;HJ:P1;H3;H4:P2;H3;HJ:P3;H4;H4:P4;C8;D7:P5;HJ;C8*results=B;8:P1;7:P2;3:P3;8:P4;5:P5;8*winResult= 5003;5005;5015*ForcedCompleted=0*ReasonCode=0*enterType=100";

                default:
                    return "choice=1001*comms=101*site=spondemo*tableCode=B001*state=0*result=B;S6;H2:P;D10;D5*results=B;8:P;5*winResult=1001";
            }
        }

        public static string GetGTCasinoTransDesc(int betType)
        {
            switch (betType)
            {
                case 3204:
                    return "choice=2018*site=IBC*tableCode=27*result=27*results=*winResult=2040;2001;2003;2005;2009;2012*ForcedCompleted=0*ReasonCode=0";

                case 3203:
                    return "choice=3009*site=IBC*tableCode=25*result=1;1;2*results=*winResult=3002;3004;3005;3026;3027;3032;3038*ForcedCompleted=0*ReasonCode=0";

                case 3202:
                    return "choice=1002*site=IBCIndo*tableCode=37*result=D;S6:T;D10*results=D;6:T;10*winResult=2002;1009*ForcedCompleted=0*ReasonCode=0";

                case 3205:
                    return "choice=5010*site=IBC*tableCode=26*result=DQ:B;D3;HJ;S5;S10;H6:P1;H4;CA;SK;H9;DK:P2;D8;CJ;SA;C2;D9:P3;S4;C9;S9;HA;SJ*results=B;0:P1;4:P2;10:P3;3*winResult=5007;5010;5008;5011;5009;5012*ForcedCompleted=0*ReasonCode=0";

                default:
                    return "choice=1002*site=IBCIndo*tableCode=37*result=P;S7;D5;D7:B;C4;HJ;CQ*results=P;9:B;4*winResult=1002;1009*ForcedCompleted=0*ReasonCode=0";
            }
        }

        public static string GetSexyGamingTransDesc(int betType)
        {
            switch (betType)
            {
                case 3101:
                    return "choice=1002*site=IBC*tableCode=5*result=B;SQ;S9;:P;S4;D3;*results=*winResult=*ForcedCompleted=0*ReasonCode=0";

                case 3102:
                    return "choice=2001*site=IBC*tableCode=31*result=D;CQ:T;C7*results=*winResult=*ForcedCompleted=0*ReasonCode=0";

                case 3103:
                    return "choice=3040*site=IBC*tableCode=51*result=5;6;6*results=*winResult=*ForcedCompleted=0*ReasonCode=0";

                case 3104:
                    return "choice=4156*site=IBC*tableCode=71*result=5*results=*winResult=*ForcedCompleted=0*ReasonCode=0";

                case 3105:
                    return "choice=3001*site=spondemo*tableCode=B001*result=R;1:B;5*results=*winResult=";

                default:
                    return "choice=1002*site=IBC*tableCode=5*result=B;SQ;S9;:P;S4;D3;*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
            }
        }

        public static string GetBBinTransDesc(int betType)
        {
            switch (betType)
            {
                case 3001:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3001*result=B;C10;D9:P;DJ;C4*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3003:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3003*result=D;S7:T;H10*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3005:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3005*result=B;S7;S5;D2:P1;S4;C3;S10:P2;D3;C4;DQ:P3;H7;C6;H5*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3006:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3006*result=B;S6;H9:P1;D8;H8:P2;D5;S1:P3;C10;H5*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3007:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3007*result=26*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3008:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3008*result=2;3;5*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3011:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3011*result=W;W;R;R*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3012:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3012*result=B;D4;D7;H6;S4;HA:P1;CA;C10;H3;D8;D9:P2;DQ;DJ;H5;CK;C4:P3;S1;D10;CJ;D3;S8*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3015:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3015*result=4*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3016:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3016*result=2;3;5*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3017:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3017*result=B;C10;D9:P;DJ;C4*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3018:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3018*result=D;H6;S8;CJ:P;D2;S12;S7*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3020:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3020*result=B;C10;D9:P;DJ;C4*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3021:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3021*result=C10*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3023:
                    return "choice=1002*site=IBC*tableCode=5*gamename=3023*result=B;D4;D7;H6;S4;HA:P1;CA;C10;H3;D8;D9:P2;DQ;DJ;H5;CK;C4:P3;S1;D10;CJ;D3;S8*results=*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3025:
                    return "choice=1,1:0.95,40000.00,-40000.00*site=*tableCode=5*gamename=3025*result=B;D9;C3;H9:P;C9;D5;H8*results=B;1:P;2*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3026:
                    return "choice=1,1:1,40000.00,40000.00*site=*tableCode=1*gamename=3026*result=D;DA:T;D3*results=D;1:T;3*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3027:
                    return "choice=1,1:0.95,40000.00,-40000.00*site=*tableCode=1*gamename=3027*result=B;C10;SK;DJ:P;C7;H10*results=B;0:P;7*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3028:
                    return "choice=151,1:1,40000.00,-40000.00*site=*tableCode=1*gamename=3028*result=2*results=2*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3029:
                    return "choice=2,1:0.97,40000.00,-40000.00*site=*tableCode=2*gamename=3029*result=D;HJ;DA;D5:P;D4;DK;H10*results=HighCard-A,HighCard-K*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3030:
                    return "choice=6,1:1,40000.00,40000.00*site=*tableCode=1*gamename=3030*result=B;H2;D9;SJ:P1;SQ;C6;H3:P2;CK;S7;SA:P3;H7;C9;H8*results=B;P1:P1;P9:P2;P8:P3;4*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3031:
                    return "choice=4,1:0.95,40000.00,38000.00*site=*tableCode=2*gamename=3031*result=B;C6;SK;C8;S5;H3:P1;H6;SQ;CA;HA;H4:P2;DJ;CJ;H9;SA;H7:P3;CQ;D5;D6;H5;D10*results=B;No Bull:P1;Bull2:P2;Bull7:P3;Bull6*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3032:
                    return "choice=5,1:0.96,40000.00,-40000.00*site=*tableCode=2*gamename=3032*result=W;W;R;R*results=W;W;R;R*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3033:
                    return "choice=1,1:1,10000.00,10000.00@47,1:1,10000.00,-10000.00@49,1:1,10000.00,10000.00@51,1:1,10000.00,-10000.00*site=*tableCode=2*gamename=3033*result=4;1;5*results=4;1;5*winResult=*ForcedCompleted=0*ReasonCode=0";
                case 3034:
                    return "choice=1,1:1,10000.00,-10000.00@35,1:1,10000.00,-10000.00@37,1:1,10000.00,10000.00@40,1:1,10000.00,10000.00*site=*tableCode=2*gamename=3034*result=3;5;6*results=3;5;6*winResult=*ForcedCompleted=0*ReasonCode=0";
                
                default:
                    return "";
            }
        }
        public IEnumerable<AGCasino> GetResultFromExcel(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}