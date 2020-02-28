using System.Collections.Generic;

namespace Fanex.Result.Core.VirtualGames.Ultilities
{
    public static class VirtualGamesDeviceConstants
    {
        public readonly static string Desktop = "d";
        public readonly static string Mobile = "m";
        public readonly static string Tablet = "t";

        private readonly static List<string> Devices = new List<string> { Desktop, Mobile, Tablet };

        public static string GetSupportedDevice(string value)
            => Devices.Contains(value.ToLowerInvariant()) ? value.ToLowerInvariant() : Desktop;
    }
}
