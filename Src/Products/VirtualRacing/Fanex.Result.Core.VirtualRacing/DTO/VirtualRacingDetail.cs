namespace Fanex.Result.Core.VirtualRacing.DTO
{
    public sealed class VirtualRacingDetail
    {
        public VirtualRacingDetail()
        {
        }

        public int Orid { get; set; }

        public int EventId { get; set; }

        public int Placing { get; set; }

        public string Lane { get; set; }

        public int RacerId { get; set; }
    }
}