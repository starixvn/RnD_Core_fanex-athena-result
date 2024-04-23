namespace Fanex.Result.Core.Sportsbook.Constants
{
    using System.Collections.Immutable;

    public static class SoccerConstants
    {
      	public const string LastSeriesChildMatchIdFormat = "{0}_{1}";
      	public static readonly ImmutableList<string> LastSeriesChildMatchMapper = ImmutableList.Create(
			"1_2300",
			"2_3100",
			"7_3700",
			"8_4100",
			"9_4600",
			"10_5400",
			"11_4900",
			"11_5000"
		);
    }
}
