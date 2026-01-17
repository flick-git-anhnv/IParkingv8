using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Futech.Socket_v2
{
    public delegate void DataReceivedInByteArrayEventHandler(byte[] dataReceivedByteArray, int dataReceivedLength, string remoteIP);
}
