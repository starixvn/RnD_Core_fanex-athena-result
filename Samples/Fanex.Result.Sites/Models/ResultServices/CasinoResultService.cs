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

                default:
                    return "choice=1001*comms=101*site=spondemo*tableCode=B001*state=0*result=B;S6;H2:P;D10;D5*results=B;8:P;5*winResult=1001";
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
                    return "choice=3001*site=spondemo*tableCode=B001*state=0*result=R;d1:B;b5*results=*winResult=";

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