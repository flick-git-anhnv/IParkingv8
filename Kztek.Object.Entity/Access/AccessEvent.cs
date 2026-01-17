using System;

namespace Kztek.Object
{
    public class AccessEvent
    {
        public string id { get; set; } = string.Empty;
        public string ControllerId { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string IdentityCode { get; set; } = string.Empty;
        public int IdentityMethod { get; set; }
        public DateTime EventTime { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public bool IsCheckOut { get; set; } = false;
    }
}
