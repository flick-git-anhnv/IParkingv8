using System;
using System.Collections.Generic;

namespace Kztek.Object
{
    [Flags]
    public enum EmIdentityType
    {
        Card = 1,
        Face = 2,
        Finger = 4,
        Password = 8,
        All = 15,
    }
    public class DeviceCapacity
    {
        public EmIdentityType IdentityType { get; set; }

        public Dictionary<EmIdentityType, int> MaxIdentityCount { get; set; } = new Dictionary<EmIdentityType, int>();
        public Dictionary<EmIdentityType, int> CurrentUserCount { get; set; } = new Dictionary<EmIdentityType, int>();

        public int MaxEventCount { get; set; }
        public int CurrentEventCount { get; set; }
    }
}
