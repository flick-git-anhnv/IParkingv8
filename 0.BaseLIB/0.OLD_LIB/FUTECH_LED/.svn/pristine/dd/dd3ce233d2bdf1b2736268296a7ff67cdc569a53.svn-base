using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Futech.DisplayLED.NetworkTools
{
    public class NetWorkTools
    {
        public static bool IsPingSuccess(string ipAddress, int timeOut)
        {
            Ping pingSender = new Ping();
            PingReply reply = null;
            reply = pingSender.Send(ipAddress, timeOut);
            if (reply != null && reply.Status == IPStatus.Success)
                return true;
            else
            {
                reply = pingSender.Send(ipAddress, timeOut);
                if (reply != null && reply.Status == IPStatus.Success)
                    return true;
            }
            return false;
        }

        public static IEnumerable<string> GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    yield return ip.ToString();
                }
            }
        }

    }
}
