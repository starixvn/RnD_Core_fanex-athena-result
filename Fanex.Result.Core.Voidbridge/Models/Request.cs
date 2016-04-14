namespace Fanex.Result.Core.Voidbridge.Models
{
    internal class Request
    {
        public string TimeStamp { get; set; }

        public Auth Auth { get; set; }

        public Call Call { get; set; }
    }
}