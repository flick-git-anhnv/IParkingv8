using System.IO;

namespace Kztek.Object
{
    public class PathManagement
    {
        public static string baseConfigBath = string.Empty;
        public static string baseImagePath = string.Empty;
        public static string configPath() => Path.Combine(baseConfigBath, "config.txt");
        public static string laneLoopDelayConfig(string laneId) => Path.Combine(configPath() + $"lane/{laneId}/loopDelay.txt");
        public static string preferTicketsConfig(string laneId) => Path.Combine(configPath() + $"lane/{laneId}/preferTickets.txt");
    }
}
