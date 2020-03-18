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

        public IEnumerable<AGCasino> GetResultFromExcel(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}