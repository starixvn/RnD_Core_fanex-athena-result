namespace Fanex.Result.Core.SAGaming
{
    using System;
    using System.Collections.Generic;

    public class TransDesc
    {
        private const string ResultTag = "result";
        private const string ResultsTag = "results";
        private const string WinResultTag = "winresult";

        private readonly Dictionary<string, string> list = new Dictionary<string, string>();

        public TransDesc(string transDesc)
        {
            if (!string.IsNullOrEmpty(transDesc))
            {
                var splittedTransDescParts = transDesc.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in splittedTransDescParts)
                {
                    var keyValuePair = part.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    if (keyValuePair.Length == 2)
                    {
                        list.Add(keyValuePair[0].ToLowerInvariant(), keyValuePair[1]);
                    }
                }
            }
        }
        public string Result => list.ContainsKey(ResultTag) ? ReplaceHardcode(list[ResultTag]).Replace("0", "") : string.Empty;

        public string Results => list.ContainsKey(ResultsTag) ? ReplaceHardcode(list[ResultsTag]) : string.Empty;

        public string WinResult => list.ContainsKey(WinResultTag) ? list[WinResultTag] : string.Empty;

        private static string ReplaceHardcode(string result)
            => result.Replace("B;", "")
            .Replace("P;", "")            
            .Replace("M;", "")
            .Replace("S;", "")
            .Replace("D;", "")
            .Replace("T;", "")
            .Replace(";", ",");
    }
}
