using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futech.DisplayLED
{
    public delegate void LedConnectStatusChangeEventHandler(object sender, LedConnectStatusChangeEventArgs e);
    public class LedConnectStatusChangeEventArgs
    {
        public string SenderId { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public bool IsConnect { get; set; } = false;
    }
}
